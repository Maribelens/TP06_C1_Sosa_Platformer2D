using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // --------------------- REFERENCIAS ---------------------
    [Header("References")]
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Animator animator;
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private GameObject deathEffectPrefab;

    // --------------------- MOVIMIENTO ---------------------
    [Header("Movement")]
    [SerializeField] private float currentSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float distanceRay;
    [SerializeField] private Transform frontController;
    [SerializeField] private bool touchingGround;
    [SerializeField] LayerMask limitsLayers;

    // --------------------- AUDIO ---------------------
    [Header("Audio")]
    [SerializeField] private AudioClip slimeSFX;
    [SerializeField] private AudioClip boomSFX;
    [SerializeField] private AudioClip explotionSFX;
    public AudioSource sfxSource;

    private void Awake()
    {
        if (rb2D == null)
        {
            rb2D = GetComponent<Rigidbody2D>();
            rb2D.bodyType = RigidbodyType2D.Dynamic;
        }
        healthSystem = GetComponent<HealthSystem>();
        //enemyData.maxLife = healthSystem.maxLife;
        healthSystem.onDie += HealthSystem_onDie;
        if (sfxSource == null)
        {
            sfxSource = GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        // Detecta si hay suelo o límite frente al enemigo
        touchingGround = Physics2D.Raycast(frontController.position, transform.right, distanceRay, limitsLayers);  
    }

    private void FixedUpdate()
    {
        rb2D.velocity = new Vector2(currentSpeed, rb2D.velocity.y);

        // Invierte dirección al llegar al borde o chocar con límite
        if (touchingGround)
        {
            currentSpeed *= -1;
            //TurnAround();
        }
        LookByMovementDirection();
    }

    private void LookByMovementDirection()
    {
        if (currentSpeed > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        else if (currentSpeed < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(frontController.position, frontController.position + distanceRay * transform.right);
    }

    // Se ejecuta cuando el enemigo muere: genera efecto y sonido
    private void HealthSystem_onDie()
    {
        if (deathEffectPrefab != null)
        {
            GameObject effect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }

        GameObject tempAudio = new GameObject("TempAudio");
        AudioSource audioSource = tempAudio.AddComponent<AudioSource>();
        audioSource.clip = explotionSFX;
        audioSource.Play();

        Destroy(tempAudio, explotionSFX.length);
        Destroy(gameObject);
    }
}
