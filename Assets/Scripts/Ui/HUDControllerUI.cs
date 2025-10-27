using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDControllerUI : MonoBehaviour
{
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

    [Header("Scripts")]
    [SerializeField] private HealthSystem health;

    private void Awake()
    {
        if (!panelCoins || !panelDiamonds)
        {
            Debug.LogError("Faltan referencias de paneles en UiElements.");
        }
        sfxSourceUI = GetComponent<AudioSource>();
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
        //health.invulnerableTimeLeft = duration;
        //if (uiCoroutine != null) StopCoroutine(uiCoroutine);
        //uiCoroutine = StartCoroutine(ShowTimer(duration));
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
}
