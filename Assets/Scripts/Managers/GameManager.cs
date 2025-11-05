using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    ////public static GameManager instance;
    public enum GameState { Playing, Paused, GameOver, Victory }
    [SerializeField] private GameState currentState;
    public GameState CurrentState => currentState;

    public event Action<GameState> OnStateChanged;
    public event Action OnGameOver;
    public event Action OnVictory;

    [Header("References")]
    [SerializeField] private PickablesUI pickablesUI;
    [SerializeField] private UIResultScreen resultsScreenUI;
    [SerializeField] private HealthSystem health;

    [Header("Pickables")]
    public int coins = 0;
    public int diamonds = 0;

    [Header("Audio")]
    [SerializeField] private AudioClip gameplayMusic;
    [SerializeField] private AudioClip gameOverMusic;
    [SerializeField] private AudioClip victoryMusic;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource musicUISource;

    private void Start()
    {
        Time.timeScale = 1;
        //SetGameState(GameState.Playing);
        if (pickablesUI == null) Debug.LogError("UiElements no asignado en GameManager");
        if (musicSource == null) Debug.LogError("AudioSource de gameplay no asignado");

        if (musicSource != null)
        {
            musicSource.clip = gameplayMusic;
            musicSource.Play();
        }

        if (health != null)
        {
            health.onInvulnerableStart += ActivateProtection;
        }
    }

    private void OnDestroy()
    {
        health.onInvulnerableStart -= ActivateProtection;
    }

    public void SetGameState(GameState newState)
    {
        if (currentState == newState) return;
        currentState = newState;
        Debug.Log("Nuevo estado: " + currentState);

        OnStateChanged?.Invoke(currentState);

        switch (currentState)
        {
            case GameState.Playing:
                Time.timeScale = 1;
                break;

            case GameState.Paused:
                Time.timeScale = 0;
                break;

            case GameState.GameOver:
                Time.timeScale = 0;
                PlayMusic(gameOverMusic);
                OnGameOver?.Invoke();
                break;

            case GameState.Victory:
                Time.timeScale = 0;
                PlayMusic(victoryMusic);
                OnVictory?.Invoke();
                break;
        }

    }
    //resetear variables

    private void PlayMusic(AudioClip clip)
    {
        if(musicSource != null && clip != null)
        {
            musicSource.Stop();
            musicUISource.clip = clip;
            musicUISource.Play();
        }
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
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        pickablesUI.UpdateAmountCoins(coins);
        Debug.Log("Monedas: " + coins);
    }
    public void AddDiamonds(int amount)
    {
        diamonds += amount;
        pickablesUI.UpdateAmountDiamonds(diamonds);
        Debug.Log("Diamantes: " + diamonds);
    }


    //public void PlayerDefeated()
    //{
    //    Time.timeScale = 0;

    //    if (musicSource != null && musicUISource != null)
    //    {
    //        musicSource.Stop();
    //        musicUISource.clip = gameOverMusic;
    //        musicUISource.Play();
    //    }
    //    uiEndGame.ShowGameOverScreen();
    //    //SetGameState(GameState.GameOver);
    //    Debug.Log("El alien fue derrotado");
    //}

    //public void PlayerVictory()
    //{
    //    Time.timeScale = 0;

    //    if(musicSource != null && musicUISource != null)
    //    {
    //        musicSource.Stop();
    //        musicUISource.clip = victoryMusic;
    //        musicUISource.Play();
    //    }
    //    uiEndGame.ShowVictoryScreen();
    //    //SetGameState(GameState.Victory);
    //    Debug.Log("El alien ha ganado");
    //}

}
