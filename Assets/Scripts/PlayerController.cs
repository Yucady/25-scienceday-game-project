using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 dragStartPos;
    private Vector2 dragEndPos;
    private bool isDragging = false;
    private bool isGameOver = false;

    public float forceMultiplier = 5f;

    private Camera mainCamera;
    private GameOverManager gameOverManager;
    private CameraFollow cameraFollow;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;

        if (mainCamera != null)
            cameraFollow = mainCamera.GetComponent<CameraFollow>();

        gameOverManager = FindFirstObjectByType<GameOverManager>();
    }

    void Update()
    {
        if (isGameOver) return;

        HandleDragInput();
        CheckIfOutOfBounds();
    }

    void HandleDragInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            dragEndPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dragVector = dragStartPos - dragEndPos;

            rb.linearVelocity = Vector2.zero;
            rb.AddForce(dragVector * forceMultiplier, ForceMode2D.Impulse);

            isDragging = false;
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
        if (collision.gameObject.CompareTag("Platform"))
        {
            ContactPoint2D contact = collision.GetContact(0);

            if (contact.normal.y > 0.5f)
            {
                float platformY = collision.transform.position.y;
                cameraFollow?.MoveCameraToPlatform(platformY);
                ScoreManager.Instance?.AddScore(1);
            }
        }
    }

    public void OnGameOver()
    {
        isGameOver = true;
    }
}
