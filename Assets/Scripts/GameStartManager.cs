using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartManager : MonoBehaviour
{
    // 게임 시작 버튼이 눌렸을 때 호출될 함수
    public void OnStartButtonClicked()
    {
        // 예: "GameScene"이라는 이름의 씬으로 전환
        SceneManager.LoadScene("25-scienceday-project");
    }
}
