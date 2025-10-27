using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        if (!panelGameOver)
        {
            Debug.LogError("Faltan referencias de paneles en GameOverUI.");
        }
        playAgainButton.onClick.AddListener(OnPlayAgainClicked);
        mainMenuButton.onClick.AddListener(OnExitGameClicked);
    }

    public void ShowGameOverScreen()
    {
        //show elements
        panelGameOver.SetActive(true);
        //panelCoins.SetActive(false);
        //panelDiamonds.SetActive(false);
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
