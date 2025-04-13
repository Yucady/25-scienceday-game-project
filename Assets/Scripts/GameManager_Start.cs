using UnityEngine;

public class GameManager_Start : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject startPanel;

    void Awake()
    {
        // ���� ���� �� ��ü ������ ������ŵ�ϴ�.
        Time.timeScale = 0f;
    }

    // ���� ��ư�� ������ ȣ��Ǵ� �Լ�
    public void StartGame()
    {
        // ���� �簳
        Time.timeScale = 1f;

        // �÷��̾� ���� �ʱ�ȭ
        if (playerController != null)
        {
            playerController.OnGameStart();
        }

        // ���� �г� �����
        if (startPanel != null)
        {
            startPanel.SetActive(false);
        }
    }
}
