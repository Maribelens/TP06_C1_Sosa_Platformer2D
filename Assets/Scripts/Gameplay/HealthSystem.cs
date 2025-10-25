using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event Action<int, int> onLifeUpdated; // <currentLife, maxLife>
    public event Action onInvulnerableStart;
    public event Action onInvulnerableEnd;
    public event Action onDie;

    [SerializeField] private AudioClip damageSFX;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private int maxLife = 100;
    private int life = 100;
    public float invulnerabilityTimer = 0f;
    public bool isInvulnerable = false;


    private void Awake()
    {
        life = maxLife;
        if (sfxSource == null)
        {
            sfxSource = GetComponent<AudioSource>();
        }
    }

    private void Start()
    {
        onLifeUpdated?.Invoke(life, maxLife);
    }

    private void Update()
    {
        PowerUpTimer();
    }

    public void PowerUpTimer()
    {
        if (isInvulnerable)
        {
            invulnerabilityTimer -= Time.deltaTime;
            if (invulnerabilityTimer <= 0f)
            {
                isInvulnerable = false;
                onInvulnerableEnd?.Invoke();
                //Debug.Log("Protección terminada");
            }
        }
    }

    public void CollectInvulnerabilityPowerup(float duration)
    {
        isInvulnerable = true;
        //Debug.Log("Protección activada");
        invulnerabilityTimer = duration; // Por ejemplo, 5 segundos
        onInvulnerableStart?.Invoke();
        Debug.Log($"Invulnerabilidad activada por {duration} segundos");
    }

    public void DoDamage(int damage)
    {
        if (damage < 0)
        {
            Debug.Log("Se cura en la funcion de daño");
            return;
        }

        if (isInvulnerable) return;

        life -= damage;
        sfxSource.clip = damageSFX;
        sfxSource.Play();

        if (life <= 0)
        {
            //life = 0;
            onDie?.Invoke();
        }
        else
        {
            onLifeUpdated?.Invoke(life, maxLife);
        }

        Debug.Log("DoDamage", gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onDie?.Invoke();
        }
    }

    public void Heal(int plus)
    {
        if (plus < 0)
        {
            Debug.Log("Se daña en la funcion de cura");
            return;
        }

        life += plus;

        if (life > maxLife)
            life = maxLife;

        Debug.Log("Heal");
        onLifeUpdated?.Invoke(life, maxLife);
    }
}
