using TMPro;
using UnityEngine;

public class PickablesUI : MonoBehaviour
{
    [Header("Panel Coins")]
    [SerializeField] public CanvasGroup panelCoins;
    [SerializeField] private TextMeshProUGUI coinText;

    [Header("Panel Diamonds")]
    [SerializeField] public CanvasGroup panelDiamonds;
    [SerializeField] private TextMeshProUGUI diamondsText;

    [Header("Panel PowerUp")]
    [SerializeField] public CanvasGroup panelPowerUp; // icono en canvas
    [SerializeField] public TextMeshProUGUI timerText;
    [SerializeField] private HealthSystem health;

    private void Awake()
    {
        //if (!panelCoins || !panelDiamonds)
        //{
        //    Debug.LogError("Faltan referencias de paneles en UiElements.");
        //}
        SetStateCanvasGroup(panelCoins, true);
        SetStateCanvasGroup(panelDiamonds, true);
        SetStateCanvasGroup(panelPowerUp, false);
    }

    private void Start()
    {
        health.onInvulnerableStart += ShowProtectionScreen;
        health.onInvulnerableEnd += EndProtectionScreen;
        health.onInvulnerabilityTimerUpdate += UpdateTimer;
    }

    private void SetStateCanvasGroup(CanvasGroup canvasGroup, bool state)
    {
        canvasGroup.alpha = state ? 1 : 0;
        canvasGroup.interactable = state;
        canvasGroup.blocksRaycasts = state;
    }

    public void UpdateAmountCoins(int amount)
    {
        coinText.text = amount.ToString();
    }

    public void UpdateAmountDiamonds(int amount)
    {
        diamondsText.text = amount.ToString();
    }

    public void ShowProtectionScreen()
    {
        health.isInvulnerable = true;
        SetStateCanvasGroup(panelPowerUp, true);
    }

    public void EndProtectionScreen()
    {
        health.isInvulnerable = false;
        SetStateCanvasGroup(panelPowerUp, false);
    }

    public void UpdateTimer(float timeleft)
    {
        timerText.text = health.invulnerableTimeLeft.ToString("f1") + "s";
    }
}
