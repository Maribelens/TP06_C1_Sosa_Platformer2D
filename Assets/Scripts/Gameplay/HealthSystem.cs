using System;
using System.Collections;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    // Eventos de estado de vida e invulnerabilidad
    public event Action<int, int> onLifeUpdated;    // (vida actual, vida máxima)
    public event Action onInvulnerableStart;
    public event Action onInvulnerableEnd;
    public event Action<float> onInvulnerabilityTimerUpdate;
    public event Action onTakeDamage;
    public event Action onDie;

    [SerializeField] private AudioClip damageSFX;
    [SerializeField] private AudioSource sfxSource;

    public int maxLife = 100;
    private int life;
    public float invulnerableTimeLeft = 0f;
    public bool isInvulnerable = false;


    private void Awake()
    {
        life = maxLife;
        sfxSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        onLifeUpdated?.Invoke(life, maxLife);
    }

    public void StartInvulnerability(float duration)
    {
        if( isInvulnerable) return;
        StartCoroutine(InvulnerabilityRoutine(duration));
        Debug.Log("Protección activada");
        Debug.Log($"Invulnerabilidad activada por {duration} segundos");
    }

    private IEnumerator InvulnerabilityRoutine(float duration)
    {
        isInvulnerable = true;
        invulnerableTimeLeft = duration;
        onInvulnerableStart?.Invoke();

        while (invulnerableTimeLeft > 0)
        {
            invulnerableTimeLeft -= Time.deltaTime;
            onInvulnerabilityTimerUpdate?.Invoke(invulnerableTimeLeft);
            yield return null;
        }

        isInvulnerable = false;
        onInvulnerableEnd?.Invoke();

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
            onDie?.Invoke();
        }
        else
        {
            onTakeDamage?.Invoke();
            onLifeUpdated?.Invoke(life, maxLife);
        }

        Debug.Log("DoDamage", gameObject);
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
        Debug.Log("Curación aplicada");
    }
}
