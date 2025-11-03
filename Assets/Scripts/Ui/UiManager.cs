using UnityEngine;

public class UiManager : MonoBehaviour
{
    ////[Header("Audio")]
    ////[SerializeField] private AudioSource sfxSourceUI;

    ////[Header("Game Over panel")]
    ////[SerializeField] private CanvasGroup gameoverPanel;
    ////[SerializeField] private Button gameOverPlayAgainButton;
    ////[SerializeField] private Button gameOverMainMenuButton;

    ////[Header("Victory panel")]
    ////[SerializeField] private CanvasGroup victoryPanel;
    ////[SerializeField] private Button victoryPlayAgainButton;
    ////[SerializeField] private Button victoryMainMenuButton;

    ////[Header("Scripts")]
    ////[SerializeField] private HealthSystem health;
    ////[SerializeField] private GameManager gameManager;

    ////private void Awake()
    ////{
    ////    sfxSourceUI = GetComponent<AudioSource>();
    ////    //gameoverPanel.GetComponent<CanvasGroup>();
    ////    SetCanvasGroup(gameoverPanel, false);
    ////    SetCanvasGroup(victoryPanel, false);
    ////    AddButtonsListeners();
    ////}

    ////private void Start()
    ////{
    ////    sfxSourceUI.loop = false;
    ////}

    ////private void OnEnable()
    ////{
    ////    if (gameManager != null)
    ////    {
    ////        gameManager.OnGameOver += ShowGameOverScreen;
    ////        gameManager.OnVictory += ShowVictoryScreen;
    ////        //gameManager.OnStateChanged += HandleStateChange;
    ////    }
    ////}

    ////private void OnDisable()
    ////{
    ////    gameManager.OnGameOver -= ShowGameOverScreen;
    ////    gameManager.OnVictory -= ShowVictoryScreen;
    ////}

    ////private void OnDestroy()
    ////{
    ////    RemoveButtonsListeners();
    ////}

    ////private void AddButtonsListeners()
    ////{
    ////    gameOverPlayAgainButton.onClick.AddListener(OnPlayAgainClicked);
    ////    gameOverMainMenuButton.onClick.AddListener(OnExitGameClicked);
    ////    victoryPlayAgainButton.onClick.AddListener(OnPlayAgainClicked);
    ////    victoryMainMenuButton.onClick.AddListener(OnExitGameClicked);
    ////}
    ////private void RemoveButtonsListeners()
    ////{
    ////    gameOverPlayAgainButton.onClick.RemoveAllListeners();
    ////    gameOverMainMenuButton.onClick.RemoveAllListeners();
    ////    victoryPlayAgainButton.onClick.RemoveAllListeners();
    ////    victoryMainMenuButton.onClick.RemoveAllListeners();
    ////}
    ////private void SetCanvasGroup(CanvasGroup canvasGroup, bool state)
    ////{
    ////    canvasGroup.alpha = state ? 1 : 0;
    ////    canvasGroup.interactable = state;
    ////    canvasGroup.blocksRaycasts = state;
    ////}
    //////private void HandleStateChange(GameManager.GameState state)
    //////{
    //////    switch (state)
    //////    {
    //////        case GameManager.GameState.GameOver:
    //////            ShowGameOverScreen();
    //////            break;
    //////        case GameManager.GameState.Victory:
    //////            ShowVictoryScreen();
    //////            break;
    //////    }
    //////}
    ////public void ShowGameOverScreen()
    ////{
    ////    //show elements
    ////    gameoverPanel.alpha = 1;
    ////    gameoverPanel.interactable = true;
    ////    gameoverPanel.blocksRaycasts = true;
    ////    //panelGameOver.SetActive(true);
    ////}
    ////public void ShowVictoryScreen()
    ////{
    ////    //show elements
    ////    victoryPanel.alpha = 1;
    ////    victoryPanel.interactable = true;
    ////    victoryPanel.blocksRaycasts = true;

    ////    //panelVictory.SetActive(true);
    ////}
    ////private void OnPlayAgainClicked()
    ////{
    ////    SceneManager.LoadScene(1);
    ////    Time.timeScale = 1;
    ////}
    ////private void OnExitGameClicked()
    ////{
    ////    SceneManager.LoadScene(0);
    ////}
}
