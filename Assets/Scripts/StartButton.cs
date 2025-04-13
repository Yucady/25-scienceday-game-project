using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] private GameManager_Start gameManager;

    private Button startButton;

    private void Awake()
    {
        startButton = GetComponent<Button>();
        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonClicked);
        }
    }

    private void OnStartButtonClicked()
    {
        if (gameManager != null)
        {
            gameManager.StartGame();
        }
    }
}
