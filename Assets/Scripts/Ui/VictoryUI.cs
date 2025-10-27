using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryUI : MonoBehaviour
{
    [SerializeField] private GameObject panelVictory;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        if (!panelVictory)
        {
            Debug.LogError("Faltan referencias de paneles en GameOverUI.");
        }
        playAgainButton.onClick.AddListener(OnPlayAgainClicked);
        mainMenuButton.onClick.AddListener(OnExitGameClicked);
    }

    public void ShowVictoryScreen()
    {
        //show elements
        panelVictory.SetActive(true);
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
