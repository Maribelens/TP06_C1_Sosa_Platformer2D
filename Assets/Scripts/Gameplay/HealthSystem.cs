using System;
using System.Collections;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event Action<int, int> onLifeUpdated; // <currentLife, maxLife>
    public event Action onInvulnerableStart;
    public event Action onInvulnerableEnd;
    public event Action<float> onInvulnerabilityTimerUpdate;
    public event Action onDie;

    [SerializeField] private AudioClip damageSFX;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private int maxLife = 100;
    private int life = 100;
    public float invulnerableTimeLeft = 0f;
    public bool isInvulnerable = false;

    [SerializeField] private GameManager gameManager;


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

    //private void Update()
    //{
    //    PowerUpTimer();
    //}

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
        invulnerableTimeLeft = duration; // Por ejemplo, 5 segundos
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

    //public void PowerUpTimer()
    //{
    //    if (isInvulnerable)
    //    {
    //        invulnerableTimeLeft -= Time.deltaTime;
    //        if (invulnerableTimeLeft <= 0f)
    //        {
    //            isInvulnerable = false;
    //            onInvulnerableEnd?.Invoke();
    //            //Debug.Log("Protección terminada");
    //        }
    //    }
    //}

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
            gameManager.ActivateProtection();
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
