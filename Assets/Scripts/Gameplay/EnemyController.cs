using UnityEngine;
using UnityEngine.Audio;

public class EnemyController : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Animator animator;

    [SerializeField] private GameConfigSo enemyData;
    private HealthSystem healthSystem;
    [SerializeField] private GameObject deathEffectPrefab;

    [Header("Movement")]
    [SerializeField] private float currentSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float distanceRay;
    [SerializeField] private Transform frontController;
    [SerializeField] private bool touchingGround;
    [SerializeField] LayerMask limitsLayers;

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
        healthSystem.onDie += HealthSystem_onDie;
        if (sfxSource == null)
        {
            sfxSource = GetComponent<AudioSource>();
        }
    }

    private void Start()
    {
        healthSystem.maxLife = enemyData.maxLife;
    }

    private void Update()
    {
        touchingGround = Physics2D.Raycast(frontController.position, transform.right, distanceRay, limitsLayers);  
    }

    private void FixedUpdate()
    {
        rb2D.velocity = new Vector2(currentSpeed, rb2D.velocity.y);
        if (touchingGround)
        {
            currentSpeed *= -1;
            //TurnAround();
        }
        LookByMovementDirection();
    }

    //void ControlAnimations()
    //{
    //    animator.SetInteger("State", Mathf.Abs(rb2D.velocity.x));
    //}

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
