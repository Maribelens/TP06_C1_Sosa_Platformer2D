using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private PlayerDataSo playerData;
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private GameManager gameManager;

    [Header("Refernces")]
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;

    [Header("Jump")]
    [SerializeField] private Transform groundController;
    [SerializeField] private Vector2 boxDimensions;
    [SerializeField] private LayerMask jumpLayers;
    [HideInInspector] public bool isGrounded;
    [SerializeField] private bool wasGrounded;
    [SerializeField] private bool canMoveDuringJump;
    [SerializeField] private int maxJumps = 2;
    private int jumpsRemaining;
    private bool jumpInput;

    [Header("Fire")]
    [SerializeField] private Transform firePoint;

    [Header("Damage")]
    [SerializeField] private GameObject deathEffectPrefab;
    [SerializeField] private int damage = 20;
    private float damageCooldown = 1f;
    private float lastDamageTime;

    [Header("Audio")]
    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioClip throwSFX;
    [SerializeField] private AudioClip landSFX;
    [SerializeField] private AudioSource sfxSource;

    private void Awake()
    {
        if (rigidBody == null)
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (sfxSource == null)
        {
            sfxSource = GetComponent<AudioSource>();
        }
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.onDie += HealthSystem_onDie;
    }
    private void Start()
    {
        jumpsRemaining = maxJumps;
        Debug.Log($"Velocidad del jugador: {playerData.speed}");     
    }
    private void Update()
    {
        if (Input.GetKeyDown(playerData.keyCodeJump))
        {
            jumpInput = true;
        }
        bool currentlyGrounded = Physics2D.OverlapBox(groundController.position, boxDimensions, 0f, jumpLayers);

        if(!wasGrounded && currentlyGrounded)
        {
            OnLand();
        }
        isGrounded = currentlyGrounded;
        wasGrounded = currentlyGrounded;

        if (isGrounded)
        {
            jumpsRemaining = maxJumps - 1;
        }

        Debug.Log($"Grounded: {isGrounded}, Jumps: {jumpsRemaining}");


        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }
    private void OnLand()
    {
        if(landSFX != null && sfxSource != null)
        {
            sfxSource.clip = landSFX;
            sfxSource.Play();
        }
    }
    private void FixedUpdate()
    {
        Movement();
        TryJump();
        jumpInput = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") && Time.time > lastDamageTime + damageCooldown) // asegúrate de que el enemigo tenga el tag "Enemy"
        {
            healthSystem.DoDamage(damage);
            lastDamageTime = Time.time;// le quita vida al jugador
        }
    }

    private void Movement()
    {
        float moveInput = 0f;
        if (!isGrounded && !canMoveDuringJump) { return; }

        if (Input.GetKey(playerData.keyCodeLeft))
        {
            moveInput = -1f;
        }
        else if (Input.GetKey(playerData.keyCodeRight))
        {
            moveInput = 1f;
        }
        if ((moveInput > 0 && !LookingRight()) || (moveInput < 0 && LookingRight()))
        {
            TurnAround();
        }
        rigidBody.velocity = new Vector2(moveInput * playerData.speed, rigidBody.velocity.y);
    }

    private void TurnAround()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private bool LookingRight()
    {
        return transform.localScale.x == 1;
    }

    private void TryJump()
    {
        if (!jumpInput) { return; }
        //if (!isGrounded) { return; }
        if(jumpsRemaining <= 0) { return; }
        Jump();
        //jumpsRemaining = Mathf.Max(0, jumpsRemaining - 1);

    }

    private void Jump()
    {
        jumpInput = false;
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
        rigidBody.AddForce(Vector2.up * playerData.jumpForce, ForceMode2D.Impulse);

        jumpsRemaining--;

        if(jumpSFX != null && sfxSource != null)
        {
            sfxSource.clip = jumpSFX;
            sfxSource.Play();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundController.position, boxDimensions);
    }

    private void Fire()
    {
        if (EventSystem.current.IsPointerOverGameObject()) // Para saber si le pego a un objeto de UI
            return;

        Bullet bullet = Instantiate(playerData.bulletPrefab);
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = Quaternion.identity;
        bullet.gameObject.layer = LayerMask.NameToLayer("Player");

        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //bullet.transform.LookAt(targetPos);
        Vector3 direction = targetPos - firePoint.position;

        bullet.SetBullet(20, 30, direction);

        sfxSource.clip = throwSFX;
        sfxSource.Play();
        //Vector3 direction = targetPos - firePoint.position;
        //angulos a calcular: jugador, mouse
    }

    private void HealthSystem_onDie()
    {
        if (deathEffectPrefab != null)
        {
            GameObject effect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }
        Invoke(nameof(HandleDeath), 0.5f);
    }

    private void HandleDeath()
    {
        gameManager.SetGameState(GameManager.GameState.GameOver);
        Destroy(gameObject);
        Debug.Log("Murió el player");
    }

}
