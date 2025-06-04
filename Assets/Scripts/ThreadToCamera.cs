using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ThreadToCamera : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    [Header("Thread Settings")]
    public int pointsCount = 20;
    public float threadWidth = 0.01f;

    [Header("Wave Animation")]
    public float waveAmplitude = 0.02f;
    public float waveFrequency = 2f;
    public float waveSpeed = 2f;

    private LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = pointsCount;
        line.useWorldSpace = true;
        line.startWidth = threadWidth;
        line.endWidth = threadWidth;
    }

    void Update()
    {
        if (startPoint == null || endPoint == null) return;

        Vector3 startPos = startPoint.position;
        Vector3 endPos = endPoint.position;
        Vector3 direction = endPos - startPos;
        Vector3 perpendicular = Vector3.Cross(direction.normalized, Vector3.up).normalized;

        for (int i = 0; i < pointsCount; i++)
        {
            float t = i / (float)(pointsCount - 1);
            Vector3 basePos = Vector3.Lerp(startPos, endPos, t);

            float wave = Mathf.Sin(Time.time * waveSpeed + t * waveFrequency * Mathf.PI * 2);
            float falloff = Mathf.Sin(t * Mathf.PI); // затухание по краям
            basePos += perpendicular * wave * waveAmplitude * falloff;

            line.SetPosition(i, basePos);
        }
    }
}