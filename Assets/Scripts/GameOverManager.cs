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

        // �Ʒ�, ����, ���������� ����� ���� ���� (���� ���)
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

        // ���� ���� (�÷��̾� ���� ���� ����)
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        // ���� �ð� �ٽ� ���
        Time.timeScale = 1f;

        // ���� �� �ٽ� �ε�
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
