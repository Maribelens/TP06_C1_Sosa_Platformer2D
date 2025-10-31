using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { Playing, Paused, GameOver, Victory }
    public GameState CurrentState { get; private set; }


    [Header("References")]
    //[SerializeField] private GameOverUI gameOverUI;
    //[SerializeField] private VictoryUI victoryUI;
    //[SerializeField] private HUDControllerUI hudUI;
    [SerializeField] private UiManager ui;
    [SerializeField] private HealthSystem health;

    [Header("Pickables")]
    public int coins = 0;
    public int diamonds = 0;
    public int healtPlus = 20;
    public int healtMinus = 15;

    [Header("Audio")]
    [SerializeField] private AudioClip gameplayMusic;

    [SerializeField] private AudioClip gameOverMusic;
    [SerializeField] private AudioClip victoryMusic;
    //[SerializeField] private AudioClip applauseSfx;
    [SerializeField] private AudioSource musicGameplay;
    [SerializeField] private AudioSource musicUI;

    private void Awake()
    {
        //musicGameplay = GameObject.Find("MusicGameplayLoop").GetComponent<AudioSource>();
        //uiElements = GameObject.Find("UI").GetComponent<UiElements>();
        health = GameObject.Find("PLAYER").GetComponent<HealthSystem>();
        musicGameplay.clip = gameplayMusic;
        musicGameplay.Play();
    }

    private void Start()
    {
        Time.timeScale = 1;
        //health.onInvulnerableStart += ActivateProtection;
        //health.onInvulnerableEnd += DeactivateProtection;
    }

    public void ActivateProtection()
    {
        if (health == null)
        {
            Debug.LogWarning("ActivateProtection: health es null");
            return;
        }
        Debug.Log("Proteccion activada");
        health.StartInvulnerability(7f);
        //uiElements.ShowProtectionScreen();
        //health.CollectInvulnerabilityPowerup(duration);
        // Acá podés cambiar color del jugador, activar un efecto, etc.
    }

    ////public void DeactivateProtection()
    ////{
    ////    if(health.invulnerableTimeLeft == 0)
    ////    {
    ////        Debug.Log("Proteccion terminada");
    ////        //uiElements.EndProtectionScreen();
    ////    }
    ////    // Acá revertís el color, desactivás el efecto, etc.
    ////}

    public void AddCoins(int amount)
    {
        coins += amount;
        ui.UpdatedCoins(coins);
        Debug.Log("Monedas: " + coins);
    }
    public void AddGems(int amount)
    {
        diamonds += amount;
        ui.UpdatedDiamonds(diamonds);
        Debug.Log("Diamantes: " + diamonds);
    }

    //public void AddHealth()
    //{
    //    health.Heal(healtPlus);
    //}

    //public void ReduceHealth()
    //{
    //    health.DoDamage(healtMinus);
    //}

    public void PlayerDefeated()
    {
        //CurrentState = GameState.GameOver;
        Time.timeScale = 0;
        musicGameplay.Stop();
        musicUI.clip = gameOverMusic;
        musicUI.Play();

        ui.ShowGameOverScreen();
        Debug.Log("El alien fue derrotado");
    }

    public void PlayerVictory()
    {
        Time.timeScale = 0;
        musicGameplay.Stop();
        musicUI.clip = victoryMusic;
        //musicUI.clip = applauseSfx;
        musicUI.Play();

        ui.ShowVictoryScreen();
        Debug.Log("El alien ha ganado");
    }
}
