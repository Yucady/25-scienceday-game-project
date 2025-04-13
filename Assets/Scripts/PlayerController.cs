using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 dragStartPos;
    private Vector2 dragEndPos;

    private bool isDragging = false;
    private bool isGrounded = false;

    [Header("힘 조절")]
    public float forceMultiplier = 5f;

    [Header("궤적 예측")]
    public LineRenderer lineRenderer;
    public int pointCount = 30;
    public float timeStep = 0.1f;

    private Camera mainCamera;
    private GameOverManager gameOverManager;
    private CameraFollow cameraFollow;

    // 마지막으로 방문한 플랫폼의 Y 좌표
    private float lastPlatformY = float.NegativeInfinity;

    // 게임 오버 상태 변수
    private bool isGameOver = false;

    // PlayerController.cs

    [SerializeField]
    private Transform startPoint; // 시작 위치를 유니티에서 할당


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;

        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;

        if (mainCamera != null)
            cameraFollow = mainCamera.GetComponent<CameraFollow>();

        // 새로운 API 사용: FindFirstObjectByType<T>()
        gameOverManager = Object.FindFirstObjectByType<GameOverManager>();

        if (gameOverManager == null)
        {
            Debug.LogError("GameOverManager를 찾을 수 없습니다.");
        }
    }

    void Update()
    {
        if (isGameOver || Time.timeScale == 0f) return;

        HandleDragInput();
        CheckIfOutOfBounds();
    }

    void HandleDragInput()
    {
        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            dragStartPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;

            // 드래그 시작 시 속도 초기화 (새 API가 아니라 velocity는 여전히 사용 가능)
            rb.linearVelocity = Vector2.zero;
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 currentPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 dragVector = dragStartPos - (Vector2)currentPoint;
            DrawTrajectory(rb.position, dragVector * forceMultiplier);
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            dragEndPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dragVector = dragStartPos - dragEndPos;

            // 힘을 적용하여 드래그한 방향으로 플레이어를 이동
            rb.AddForce(dragVector * forceMultiplier, ForceMode2D.Impulse);

            isDragging = false;
            isGrounded = false;
            lineRenderer.enabled = false;
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

    void CheckIfOutOfBounds()
    {
        if (mainCamera == null || gameOverManager == null) return;

        float camBottom = mainCamera.transform.position.y - mainCamera.orthographicSize;
        float camLeft = mainCamera.transform.position.x - mainCamera.orthographicSize * mainCamera.aspect;
        float camRight = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect;
        Vector2 pos = transform.position;

        if (pos.y < camBottom || pos.x < camLeft || pos.x > camRight)
        {
            gameOverManager.TriggerGameOver();
            isGameOver = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.GetContact(0);

        // 땅이나 플랫폼에 착지하면 드래그 가능 상태로 전환
        if ((collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform")) &&
            contact.normal.y > 0.5f)
        {
            isGrounded = true;
        }

        // 플랫폼에 착지했을 때, 점수 추가 및 카메라 이동 처리
        if (collision.gameObject.CompareTag("Platform") && contact.normal.y > 0.5f)
        {
            float platformY = collision.transform.position.y;
            if (platformY > lastPlatformY)
            {
                lastPlatformY = platformY;
                ScoreManager.Instance?.AddScore(1);
                cameraFollow?.MoveCameraToPlatform(platformY);
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }

    public void OnGameStart()
    {
        isGameOver = false;
        rb.linearVelocity = Vector2.zero;

        // 설정된 시작 위치로 이동
        if (startPoint != null)
            transform.position = startPoint.position;

        Debug.Log("게임 시작! 플레이어 상태 초기화 완료");
    }


    public void OnGameOver()
    {
        isGameOver = true;
    }
}
