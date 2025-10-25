using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { Playing, Paused, GameOver, Victory }
    public GameState CurrentState { get; private set; }


    [Header("References")]
    //[SerializeField] private UiElements uiElements;
    [SerializeField] private HealthSystem health;

    [Header("Pickables")]
    public int coins = 0;
    public int diamonds = 0;
    public int healtPlus = 20;

    [Header("Audio")]
    [SerializeField] private AudioClip gameplayMusic;
    [SerializeField] private AudioClip gameOverMusic;
    [SerializeField] private AudioClip victoryMusic;
    //[SerializeField] private AudioClip applauseSfx;
    [SerializeField] private AudioSource musicGameplay;
    [SerializeField] private AudioSource musicGameLoop;

    private void Awake()
    {
        //uiElements = GameObject.Find("UI").GetComponent<UiElements>();
        health = GameObject.Find("PLAYER").GetComponent<HealthSystem>();

        musicGameplay.clip = gameplayMusic;
        musicGameplay.Play();
    }

    private void Start()
    {
        Time.timeScale = 1;
        health.onInvulnerableStart += ActivateProtection;
        health.onInvulnerableEnd += DeactivateProtection;
    }

    public void ActivateProtection()
    {
        if (health == null)
        {
            Debug.LogWarning("ActivateProtection: health es null");
            return;
        }
        Debug.Log("Proteccion activada");
        //uiElements.StartProtectionScreen(7);
        //health.CollectInvulnerabilityPowerup(duration);
        // Acá podés cambiar color del jugador, activar un efecto, etc.
    }

    public void DeactivateProtection()
    {
        Debug.Log("Proteccion terminada");
        //uiElements.EndProtectionScreen();
        // Acá revertís el color, desactivás el efecto, etc.
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        //uiElements.UpdatedCoins(coins);
        Debug.Log("Monedas: " + coins);
    }
    public void AddGems(int amount)
    {
        diamonds += amount;
        //uiElements.UpdatedDiamonds(diamonds);
        Debug.Log("Diamantes: " + diamonds);
    }

    public void AddHealth()
    {
        health.Heal(healtPlus);
    }

    public void PlayerDefeated()
    {
        //CurrentState = GameState.GameOver;
        Time.timeScale = 0;
        musicGameplay.Stop();
        musicGameLoop.clip = gameOverMusic;
        musicGameLoop.Play();

        //uiElements.ShowGameOverScreen();
        Debug.Log("El alien fue derrotado");
    }

    public void PlayerVictory()
    {
        Time.timeScale = 0;
        musicGameplay.Stop();
        musicGameLoop.clip = victoryMusic;
        //musicGameLoop.clip = applauseSfx;
        musicGameLoop.Play();

        //uiElements.ShowVictoryScreen();
        Debug.Log("El alien ha ganado");
    }
}
