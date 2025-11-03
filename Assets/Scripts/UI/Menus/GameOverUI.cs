using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private CanvasGroup canvasGroup;

    private void Awake()
    {
        if (!panelGameOver)
        {
            Debug.LogError("Faltan referencias de paneles en GameOverUI.");
        }
        //gameoverPanel.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        playAgainButton.onClick.AddListener(OnPlayAgainClicked);
        mainMenuButton.onClick.AddListener(OnExitGameClicked);
    }

    public void ShowGameOverScreen()
    {
        //show elements
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        //panelGameOver.SetActive(true);
    }

    private void OnPlayAgainClicked()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    private void OnExitGameClicked()
    {
        SceneManager.LoadScene(0);
    }

    private void OnDestroy()
    {
        playAgainButton.onClick.RemoveAllListeners();
        mainMenuButton.onClick.RemoveAllListeners();
    }
}
