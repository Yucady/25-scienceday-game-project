using UnityEngine;

public class gamestartscenemove : MonoBehaviour
{
    public GameObject gameStartPanel; // Inspector에서 할당할 패널

    // Start is called before the first frame update
    void Start()
    {
        // 초기화 코드가 필요하면 여기에 작성
    }

    // Update is called once per frame
    void Update()
    {
        // 매 프레임 실행할 코드가 필요하면 여기에 작성
    }

    // 버튼 클릭 시 호출될 메서드
    public void OnGameStartButtonClicked()
    {
        if (gameStartPanel != null)
        {
            gameStartPanel.SetActive(false); // 패널 비활성화
        }
    }
}