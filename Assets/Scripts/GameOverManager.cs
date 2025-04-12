using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Transform player;

    private bool isGameOver = false;

    void Update()
    {
        if (isGameOver || player == null) return;

        Vector3 viewportPos = Camera.main.WorldToViewportPoint(player.position);

        if (viewportPos.y < 0 || viewportPos.x < 0 || viewportPos.x > 1)
        {
            TriggerGameOver();
        }
    }

    public void TriggerGameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // 플레이어 조작 막기
        if (player != null)
        {
            PlayerController controller = player.GetComponent<PlayerController>();
            if (controller != null)
                controller.OnGameOver();
        }

        //// AppleShooter도 조작 막기
        //AppleShooter shooter = FindObjectOfType<AppleShooter>();
        //if (shooter != null)
        //    shooter.OnGameOver();

        //// 게임 정지
        //Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
