using UnityEngine;

public class Pickables : MonoBehaviour
{
    public enum PickableType { Coin, Diamond, Protection, Health, Damage }
    [SerializeField] private HealthSystem health;

    [SerializeField] private PickableType type;
    [SerializeField] private AudioClip pickSound;
    [SerializeField] private GameObject EffectPrefab;
    [SerializeField] private GameManager gameManager;

    //private float protectionDuration = 5f;
    //private bool refreshIfActive = true;

    //private void Awake()
    //{
    //    if (gameManager == null)
    //        gameManager = GetComponent<GameManager>();

    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HealthSystem health = collision.GetComponent<HealthSystem>();

            if (EffectPrefab != null)
            {
                GameObject effect = Instantiate(EffectPrefab, transform.position, Quaternion.identity);
                Destroy(effect, 2f);
            }
            Destroy(gameObject);

            if (pickSound != null)
            {
                AudioSource.PlayClipAtPoint(pickSound, transform.position);
            }

            switch (type)
            {
                case PickableType.Coin:
                    gameManager.AddCoins(1);
                    break;

                case PickableType.Diamond:
                    gameManager.AddGems(1);
                    break;
                case PickableType.Health:
                    //gameManager.AddHealth();
                    health.Heal(20);
                    break;
                case PickableType.Damage:
                    //gameManager.ReduceHealth();
                    health.DoDamage(20);
                    break;
                case PickableType.Protection:
                    gameManager.ActivateProtection();
                    //health.StartInvulnerability(7f);
                    break;
            }
            Debug.Log("Player recogio un objeto");
        }
    }
}
