using UnityEngine;

public class gamestartscenemove : MonoBehaviour
{
    public GameObject gameStartPanel; // Inspector���� �Ҵ��� �г�

    // Start is called before the first frame update
    void Start()
    {
        // �ʱ�ȭ �ڵ尡 �ʿ��ϸ� ���⿡ �ۼ�
    }

    // Update is called once per frame
    void Update()
    {
        // �� ������ ������ �ڵ尡 �ʿ��ϸ� ���⿡ �ۼ�
    }

    // ��ư Ŭ�� �� ȣ��� �޼���
    public void OnGameStartButtonClicked()
    {
        if (gameStartPanel != null)
        {
            gameStartPanel.SetActive(false); // �г� ��Ȱ��ȭ
        }
    }
}