using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 dragStartPos;
    private Vector2 dragEndPos;
    private bool isDragging = false;
    private bool isGameOver = false;
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

    private HashSet<GameObject> visitedPlatforms = new HashSet<GameObject>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;

        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.enabled = false;

        if (mainCamera != null)
            cameraFollow = mainCamera.GetComponent<CameraFollow>();

        gameOverManager = FindFirstObjectByType<GameOverManager>();
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

            rb.linearVelocity = Vector2.zero;
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
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.GetContact(0);

        // ✅ Ground 또는 Platform이면 드래그 가능
        if ((collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform")) && contact.normal.y > 0.5f)
        {
            isGrounded = true;
        }

        // ✅ Platform은 점수/카메라 이동용
        if (collision.gameObject.CompareTag("Platform") && contact.normal.y > 0.5f)
        {
            if (!visitedPlatforms.Contains(collision.gameObject))
            {
                visitedPlatforms.Add(collision.gameObject);

                float platformY = collision.transform.position.y;
                cameraFollow?.MoveCameraToPlatform(platformY);
                ScoreManager.Instance?.AddScore(1);
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

    public void OnGameOver()
    {
        isGameOver = true;
    }
}
