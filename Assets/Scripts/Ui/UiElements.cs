//using TMPro;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;

//public class UiElements : MonoBehaviour
//{
//    [Header("Panels")]
//    [SerializeField] private GameObject panelGameOver;
//    [SerializeField] private GameObject panelVictory;
//    [SerializeField] private GameObject panelCoins;
//    [SerializeField] private GameObject panelGems;

//    [Header("PowerUp Panel")]
//    [SerializeField] private HealthSystem health;
//    [SerializeField] private GameObject panelPowerUp; // icono en canvas
//    [SerializeField] private TextMeshProUGUI timerText;
//    //private float protectionTimeRemaining;
//    //private bool isProtectionActive = false;

//    [Header("Game Over Buttons")]
//    [SerializeField] private Button gameOverPlayAgainButton;
//    [SerializeField] private Button gameOverMainMenuButton;

//    [Header("Victory Buttons")]
//    [SerializeField] private Button winPlayAgainButton;
//    [SerializeField] private Button winMainMenuButton;

//    [Header("References")]
//    [SerializeField] private TextMeshProUGUI coinText;
//    [SerializeField] private TextMeshProUGUI gemText;
//    [SerializeField] private AudioClip gameOverSFX;
//    [SerializeField] private AudioSource sfxSourceUI;

//    private void Awake()
//    {
//        if (!panelGameOver || !panelCoins || !panelGems)
//        {
//            Debug.LogError("Faltan referencias de paneles en UiElements.");
//        }

//        gameOverPlayAgainButton.onClick.AddListener(OnPlayAgainClicked);
//        gameOverMainMenuButton.onClick.AddListener(OnExitGameClicked);

//        winPlayAgainButton.onClick.AddListener(OnPlayAgainClicked);
//        winMainMenuButton.onClick.AddListener(OnExitGameClicked);

//        sfxSourceUI = GetComponent<AudioSource>();

//    }

//    private void Start()
//    {
//        health.onInvulnerableStart += ShowProtectionScreen;
//        health.onInvulnerableEnd += EndProtectionScreen;
//        health.onInvulnerabilityTimerUpdate += UpdateTimer;
//        sfxSourceUI.loop = false;
//    }

//    private void UpdateTimer(float timeLeft)
//    {
//        timerText.text = health.invulnerableTimeLeft.ToString("F1") + "s";
//    }

//    //private void Update()
//    //{
//    //    if (health.isInvulnerable)
//    //    {
//    //        health.invulnerableTimeLeft -= Time.deltaTime;
//    //        if (health.invulnerableTimeLeft < 0f)
//    //        {
//    //            health.invulnerableTimeLeft = 0f;
//    //        }
//    //        timerText.text = health.invulnerableTimeLeft.ToString("F1") + "s";
//    //    }
//    //}

//    public void UpdatedCoins(int amount)
//    {
//        coinText.text = amount.ToString();
//    }
//    public void UpdatedDiamonds(int amount)
//    {
//        gemText.text = amount.ToString();
//    }

//    public void ShowProtectionScreen()
//    {
//        health.isInvulnerable = true;
//        panelPowerUp.SetActive(true);
//        //health.invulnerableTimeLeft = duration;
//        //if (uiCoroutine != null) StopCoroutine(uiCoroutine);
//        //uiCoroutine = StartCoroutine(ShowTimer(duration));
//    }

//    public void EndProtectionScreen()
//    {
//        health.isInvulnerable = false;
//        panelPowerUp.SetActive(false);
//    }

//    public void ShowGameOverScreen()
//    {
//        //show elements
//        panelGameOver.SetActive(true);
//        //panelCoins.SetActive(false);
//        //panelDiamonds.SetActive(false);
//    }

//    public void ShowVictoryScreen()
//    {
//        //show elements
//        panelVictory.SetActive(true);
//        //panelCoins.SetActive(false);
//        //panelDiamonds.SetActive(false);
//    }

//    private void OnDestro()
//    {
//        gameOverPlayAgainButton.onClick.RemoveAllListeners();
//        gameOverMainMenuButton.onClick.RemoveAllListeners();

//        winPlayAgainButton.onClick.RemoveAllListeners();
//        winMainMenuButton.onClick.RemoveAllListeners();
//    }

//    private void OnPlayAgainClicked()
//    {
//        SceneManager.LoadScene(1);
//        Time.timeScale = 1;
//    }
//    private void OnExitGameClicked()
//    {
//        SceneManager.LoadScene(0);
//    }
//}
