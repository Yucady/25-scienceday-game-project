using UnityEngine;

public class GameManager_Start : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject startPanel;

    void Awake()
    {
        // 게임 시작 전 전체 게임을 정지시킵니다.
        Time.timeScale = 0f;
    }

    // 시작 버튼을 누르면 호출되는 함수
    public void StartGame()
    {
        // 게임 재개
        Time.timeScale = 1f;

        // 플레이어 상태 초기화
        if (playerController != null)
        {
            playerController.OnGameStart();
        }

        // 시작 패널 숨기기
        if (startPanel != null)
        {
            startPanel.SetActive(false);
        }
    }
}
