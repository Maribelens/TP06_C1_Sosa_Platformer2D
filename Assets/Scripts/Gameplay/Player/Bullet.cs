using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 20;
    private Rigidbody2D rigidBody;
    [SerializeField] private GameObject effectPrefab;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector3 direction = rigidBody.velocity;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Debug.Log($"Angulo: {angle}");
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.DoDamage(damage);
        }
        if (other.CompareTag("Enemy") && other.CompareTag("Ground"))
        {
            GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f);
            Destroy(gameObject);
        }
    }

    public void SetBullet(int speed, int damage, Vector3 direction)
    {
        rigidBody.bodyType = RigidbodyType2D.Dynamic;
        rigidBody.velocity = direction.normalized * speed;
    }
}
