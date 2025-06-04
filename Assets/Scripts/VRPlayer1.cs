using UnityEngine;

public class VRPlayer1 : MonoBehaviour
{
    public Transform mainCamera;
    public float lookAngleThreshold = 10.0f; // 10 might be the best according to tests
    public float maxSpeed = 2.0f; // 1.5?
    public float acceleration = 1.5f;
    public float decelerationSmoothTime = 0.5f;

    public float yawThreshold = 145f;
    public float yawTurnRate = 10f;

    private CharacterController cc;
    private float currentSpeed = 0f;
    private float speedVelocity = 0f;
    private float overRotationTimer = 0f;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        float pitch = mainCamera.eulerAngles.x;
        if (pitch > 180f) pitch -= 360f;

        float yaw = mainCamera.eulerAngles.y;
        if (yaw > 180f) yaw -= 360f;

        // ============ fly control ============
        bool wantMove = Mathf.Abs(pitch) >= lookAngleThreshold;
        float targetSpeed = wantMove ? maxSpeed : 0f;

        // lerp
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedVelocity, decelerationSmoothTime);

        if (currentSpeed > 0.01f)
        {
            Vector3 moveDir = mainCamera.forward;
            moveDir.Normalize();
            cc.Move(moveDir * currentSpeed * Time.deltaTime);
        }

        // ============ turn ============
        if (Mathf.Abs(yaw) > yawThreshold)
        {
            overRotationTimer += Time.deltaTime;

            float extraRotation = yawTurnRate * overRotationTimer * Time.deltaTime;
            transform.Rotate(0f, Mathf.Sign(yaw) * extraRotation, 0f);
        }
        else
        {
            overRotationTimer = 0f;
        }

        if (Mathf.Abs(pitch) > 5f && overRotationTimer > 0f)
        {
            overRotationTimer = 0f;
        }
    }
}
