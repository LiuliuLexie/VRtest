using UnityEngine;

public class VRPlayer1 : MonoBehaviour
{
    public Transform mainCamera;
    public float lookAngleThreshold = 10.0f;
    public float speed = 2.0f; // max spped
    public bool moveForward;

    private CharacterController cc;

    private float currentSpeed = 0f; 
    private float speedVelocity = 0f; // for calculation SmoothDamp
    public float smoothTime = 0.3f; 

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        float pitch = mainCamera.eulerAngles.x;
        if (pitch > 180f) pitch -= 360f;

        if (pitch >= lookAngleThreshold || pitch <= -lookAngleThreshold)
        {
            moveForward = true;
        }
        else
        {
            moveForward = false;
        }

        float targetSpeed = moveForward ? speed : 0f;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedVelocity, smoothTime);

        if (currentSpeed > 0.01f)
        {
            Vector3 forward = mainCamera.forward;
            forward.Normalize();
            cc.Move(forward * currentSpeed * Time.deltaTime);
        }

    // Debug.Log("Current Speed: " + currentSpeed.ToString("F2"));
    }
}
