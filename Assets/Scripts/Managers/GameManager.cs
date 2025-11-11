using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { Playing, Paused, GameOver, Victory }
    [SerializeField] private GameState currentState;
    public GameState CurrentState => currentState;

    public event Action<GameState> OnStateChanged;
    public event Action OnGameOver;
    public event Action OnVictory;

    [Header("References")]
    [SerializeField] private UIPickables uiPickables;
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
        if (uiPickables == null) Debug.LogError("UiElements no asignado en GameManager");
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

    private void PlayMusic(AudioClip clip)
    {
        // Cambia la música según el estado actual del juego
        if (musicSource != null && clip != null)
        {
            musicSource.Stop();
            musicUISource.clip = clip;
            musicUISource.Play();
        }
    }

    public void ActivateProtection()
    {
        // Activa la invulnerabilidad temporal del jugador
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
        uiPickables.UpdateAmountCoins(coins);
        Debug.Log("Monedas: " + coins);
    }

    public void AddDiamonds(int amount)
    {
        diamonds += amount;
        uiPickables.UpdateAmountDiamonds(diamonds);
        Debug.Log("Diamantes: " + diamonds);
    }
}
