using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartManager : MonoBehaviour
{
    // ���� ���� ��ư�� ������ �� ȣ��� �Լ�
    public void OnStartButtonClicked()
    {
        // ��: "GameScene"�̶�� �̸��� ������ ��ȯ
        SceneManager.LoadScene("25-scienceday-project");
    }
}
