using UnityEngine;

public class VRPlayer : MonoBehaviour
{
    public Transform mainCamera;
    // public float lookDownAngle = 15.0f;
    // public float lookUpAngle = 15.0f;
    public float lookAngleThreshold = 15.0f;
    public float speed = 2.0f;
    public bool moveForward;

    private CharacterController cc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cc = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // 将角度从 0~360 映射为 -180~180
        float pitch = mainCamera.eulerAngles.x;
        if (pitch > 180f) pitch -= 360f;

        // 如果抬头或低头超过阈值就触发飞行
        if (pitch >= lookAngleThreshold || pitch <= -lookAngleThreshold)
        {
            moveForward = true;
        }
        else
        {
            moveForward = false;
        }

        if (moveForward)
        {
            Vector3 forward = mainCamera.forward;
            forward.Normalize();
            // add a exceleration variable to make a lerp movement
            cc.Move(forward * speed * Time.deltaTime);
        }
    }
}
