using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Transform player;

    private bool isGameOver = false;

    void Update()
    {
        if (isGameOver) return;

        Vector3 viewportPos = Camera.main.WorldToViewportPoint(player.position);

        // 아래, 왼쪽, 오른쪽으로 벗어나면 게임 오버 (위는 허용)
        if (viewportPos.y < 0 || viewportPos.x < 0 || viewportPos.x > 1)
        {
            TriggerGameOver();
        }
    }

    public void TriggerGameOver()
    {
        isGameOver = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // 게임 정지 (플레이어 조작 포함 멈춤)
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        // 게임 시간 다시 재생
        Time.timeScale = 1f;

        // 현재 씬 다시 로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
