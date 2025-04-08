using UnityEngine;

public class AppleShooter : MonoBehaviour
{
    public Rigidbody2D appleRb;
    public LineRenderer lineRenderer;
    public float launchForce = 10f;
    public int pointCount = 30;
    public float timeStep = 0.1f;

    private Vector3 startPoint;
    private Camera cam;
    private bool isDragging = false;

    void Start()
    {
        cam = Camera.main;
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            startPoint.z = 0;
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            currentPoint.z = 0;

            Vector3 dir = startPoint - currentPoint;
            Vector3 velocity = dir * launchForce;

            DrawTrajectory(appleRb.position, velocity);
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            lineRenderer.enabled = false;

            Vector3 endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            endPoint.z = 0;

            Vector2 force = (startPoint - endPoint) * launchForce;
            appleRb.linearVelocity = force; // ���� ����� ������ � ����!
        }
    }

    void DrawTrajectory(Vector3 startPos, Vector3 startVelocity)
    {
        lineRenderer.enabled = true;
        lineRenderer.positionCount = pointCount;

        Vector3 gravity = Physics2D.gravity;
        for (int i = 0; i < pointCount; i++)
        {
            float t = i * timeStep;
            Vector3 point = startPos + startVelocity * t + 0.5f * gravity * t * t;
            lineRenderer.SetPosition(i, point);
        }
    }
}
