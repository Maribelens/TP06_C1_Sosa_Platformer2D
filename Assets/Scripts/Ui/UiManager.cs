using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [Header("HUD panele")]

    [Header("Panel Coins")]
    [SerializeField] private GameObject panelCoins;
    [SerializeField] private TextMeshProUGUI coinText;

    [Header("Panel Diamonds")]
    [SerializeField] private GameObject panelDiamonds;
    [SerializeField] private TextMeshProUGUI diamondsText;

    [Header("Panel PowerUp")]
    [SerializeField] private GameObject panelPowerUp;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Audio")]
    [SerializeField] private AudioClip gameOverSFX;
    [SerializeField] private AudioSource sfxSourceUI;


    [Header("Game Over panel")]
    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private Button gameOverPlayAgainButton;
    [SerializeField] private Button gameOverMainMenuButton;
    [SerializeField] private CanvasGroup gameOverCanvasGroup;

    [Header("Victory panel")]
    [SerializeField] private GameObject panelVictory;
    [SerializeField] private Button victoryPlayAgainButton;
    [SerializeField] private Button victoryMainMenuButton;
    [SerializeField] private CanvasGroup victoryCanvasGroup;

    [Header("Scripts")]
    [SerializeField] private HealthSystem health;

    private void Awake()
    {
        if (!panelGameOver || !panelCoins || !panelDiamonds)
        {
            Debug.LogError("Faltan referencias de paneles en UiElements.");
        }
        sfxSourceUI = GetComponent<AudioSource>();
        //gameOverCanvasGroup.GetComponent<CanvasGroup>();
        gameOverCanvasGroup.alpha = 0;
        gameOverCanvasGroup.interactable = false;
        gameOverCanvasGroup.blocksRaycasts = false;

        victoryCanvasGroup.alpha = 0;
        victoryCanvasGroup.interactable = false;
        victoryCanvasGroup.blocksRaycasts = false;

        gameOverPlayAgainButton.onClick.AddListener(OnPlayAgainClicked);
        gameOverMainMenuButton.onClick.AddListener(OnExitGameClicked);
        victoryPlayAgainButton.onClick.AddListener(OnPlayAgainClicked);
        victoryMainMenuButton.onClick.AddListener(OnExitGameClicked);
    }

    private void Start()
    {
        health.onInvulnerableStart += ShowProtectionScreen;
        health.onInvulnerableEnd += EndProtectionScreen;
        health.onInvulnerabilityTimerUpdate += UpdateTimer;
        sfxSourceUI.loop = false;
    }

    public void UpdatedCoins(int amount)
    {
        coinText.text = amount.ToString();
    }
    public void UpdatedDiamonds(int amount)
    {
        diamondsText.text = amount.ToString();
    }

    public void ShowProtectionScreen()
    {
        health.isInvulnerable = true;
        panelPowerUp.SetActive(true);
    }

    public void EndProtectionScreen()
    {
        health.isInvulnerable = false;
        panelPowerUp.SetActive(false);
    }

    private void UpdateTimer(float timeLeft)
    {
        timerText.text = health.invulnerableTimeLeft.ToString("F1") + "s";
    }

    public void ShowGameOverScreen()
    {
        //show elements
        gameOverCanvasGroup.alpha = 1;
        gameOverCanvasGroup.interactable = true;
        gameOverCanvasGroup.blocksRaycasts = true;
        //panelGameOver.SetActive(true);
    }

    public void ShowVictoryScreen()
    {
        //show elements
        victoryCanvasGroup.alpha = 1;
        victoryCanvasGroup.interactable = true;
        victoryCanvasGroup.blocksRaycasts = true;

        //panelVictory.SetActive(true);
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
        gameOverPlayAgainButton.onClick.RemoveAllListeners();
        gameOverMainMenuButton.onClick.RemoveAllListeners();
        victoryPlayAgainButton.onClick.RemoveAllListeners();
        victoryMainMenuButton.onClick.RemoveAllListeners();
    }
}
