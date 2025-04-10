//using UnityEngine;

//public class TrajectoryRenderer : MonoBehaviour
//{
//    public LineRenderer lineRenderer;
//    public int pointCount = 30;
//    public float timeStep = 0.1f;
//    public float forceMultiplier = 4f;
//    public Transform launchPoint; // 사과의 시작 위치

//    private Camera mainCamera;
//    private bool isDragging = false;

//    void Start()
//    {
//        mainCamera = Camera.main;
//        lineRenderer.positionCount = pointCount;
//        lineRenderer.enabled = false;
//    }

//    void Update()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            isDragging = true;
//            lineRenderer.enabled = true;
//        }

//        if (isDragging)
//        {
//            Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
//            mouseWorld.z = 0;
//            Vector3 start = launchPoint.position;
//            Vector3 direction = (start - mouseWorld) * forceMultiplier;

//            DrawTrajectory(start, direction);
//        }

//        if (Input.GetMouseButtonUp(0))
//        {
//            isDragging = false;
//            lineRenderer.enabled = false;

//            // 여기서 사과 발사할 수 있음!
//        }
//    }

//    void DrawTrajectory(Vector3 startPos, Vector3 startVelocity)
//    {
//        Vector3 gravity = new Vector3(Physics2D.gravity.x, Physics2D.gravity.y, 0);

//        // 필수! 포인트 개수 설정
//        lineRenderer.positionCount = pointCount;

//        for (int i = 0; i < pointCount; i++)
//        {
//            float t = i * timeStep;
//            Vector3 point = startPos + startVelocity * t + 0.5f * gravity * t * t;
//            lineRenderer.SetPosition(i, point);
//        }
//    }
//}
