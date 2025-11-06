using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private PlayerDataSo playerData;
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private GameManager gameManager;

    private Rigidbody2D rigidBody;
    private Animator animator;

    //private bool isJumping = false;
    //private bool isAttacking = false;

    [SerializeField] private LayerMask layerMaskGround;

    private List<State> states = new List<State>();
    [SerializeField] private State currentState;
    [SerializeField] private State previousState;

    [Header("Jump")]
    [SerializeField] private Transform groundController;
    [SerializeField] private Vector2 boxDimensions;
    [SerializeField] private LayerMask jumpLayers;
    [HideInInspector] public bool isGrounded;
    [SerializeField] private bool wasGrounded;
    [SerializeField] private bool canMoveDuringJump;
    //[SerializeField] private int maxJumps = 2;
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
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //if (sfxSource == null)
        //{
        //    sfxSource = GetComponent<AudioSource>();
        //}
        //healthSystem = GetComponent<HealthSystem>();
        //healthSystem.onDie += HealthSystem_onDie;
    }
    private void Start()
    {
        states.Add(new StateIdle(this));
        states.Add(new StateWalk(this));
        states.Add(new StateJump(this));

        SwapStateTo(AnimationStates.Idle);
        //jumpsRemaining = maxJumps;
        //Debug.Log($"Velocidad del jugador: {playerData.speed}");     
    }
    private void Update()
    {
        currentState.Update();
    }
        //if (Input.GetKeyDown(playerData.keyCodeJump))
        //{
        //    jumpInput = true;
        //}
        //bool currentlyGrounded = Physics2D.OverlapBox(groundController.position, boxDimensions, 0f, jumpLayers);

        //if (!wasGrounded && currentlyGrounded)
        //{
        //    OnLand();
        //}
        //isGrounded = currentlyGrounded;
        //wasGrounded = currentlyGrounded;

        //if (isGrounded)
        //{
        //    jumpsRemaining = maxJumps - 1;
        //}

        //Debug.Log($"Grounded: {isGrounded}, Jumps: {jumpsRemaining}");



        //if (Input.GetMouseButtonDown(0))
        //{
        //    Fire();
        //}

    public void SwapStateTo(AnimationStates nextState)
    {
        foreach (State state in states)
        {
            if (state.state == nextState)
            {               
                    currentState?.OnExit();

                    currentState = state;
                    currentState.OnEnter();
                    break;
                
            }
        }
    }

    public void ChangeAnimatorState(int state)
    {
        animator.SetInteger("State", state);
    }

    public bool IsGroundCollision()
    {
        float radius = 0.35f;
        float distanceOffset = 0.35f;

        Vector3 offset = -transform.up * distanceOffset;
        Vector3 center = transform.position + offset;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(center, radius, Vector2.down, 0.0f, layerMaskGround);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform != transform)
            {
                //Debug.Log($"Hit: {hit.transform.gameObject.name}");
                return true;
            }    
        }

        return false;
    }

    //private void OnLand()
    //{
    //    if(landSFX != null && sfxSource != null)
    //    {
    //        sfxSource.clip = landSFX;
    //        sfxSource.Play();
    //    }
    //}
    //private void FixedUpdate()
    //{
    //    Movement();
    //    TryJump();
    //    jumpInput = false;
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") && Time.time > lastDamageTime + damageCooldown) // asegúrate de que el enemigo tenga el tag "Enemy"
        {
            healthSystem.DoDamage(damage);
            lastDamageTime = Time.time;// le quita vida al jugador
        }
    }

    public void Movement(Vector3 direction, float moveinput)
    {
        if (!IsGroundCollision() && !canMoveDuringJump) { return; }
        rigidBody.AddForce(direction * playerData.speed * Time.deltaTime, ForceMode2D.Force);

        if ((GetVelocityX() > 0 && !LookingRight()) || (GetVelocityX() < 0 && LookingRight()))
        {
            TurnAround();
        }
    }

    //float moveInput = 0f;


    //if (Input.GetKey(playerData.keyCodeLeft) || Input.GetKey(playerData.keyCodeLeftAlt))
    //{
    //    moveInput = -1f;
    //}
    //else if (Input.GetKey(playerData.keyCodeRight) || Input.GetKey(playerData.keyCodeRightAlt))
    //{
    //    moveInput = 1f;
    //}
    //if ((moveInput > 0 && !LookingRight()) || (moveInput < 0 && LookingRight()))
    //{
    //    TurnAround();
    //}
    //rigidBody.velocity = new Vector2(moveInput * playerData.speed, rigidBody.velocity.y);


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

    public float GetVelocityX()
    {
        return rigidBody.velocityX;
    }

    //if (Input.GetKey(playerData.keyCodeLeft))
    //{
    //    moveInput = -1f;
    //}
    //else if (Input.GetKey(playerData.keyCodeRight))
    //{
    //    moveInput = 1f;
    //}
    //private void TryJump()
    //{
    //    if (!jumpInput) { return; }
    //    //if (!isGrounded) { return; }
    //    if(jumpsRemaining <= 0) { return; }
    //    Jump();
    //    //jumpsRemaining = Mathf.Max(0, jumpsRemaining - 1);

    //}

    public void Jump()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
        rigidBody.AddForce(Vector2.up * playerData.jumpForce, ForceMode2D.Impulse);
    }

        //jumpInput = false;
        //rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
        //rigidBody.AddForce(Vector2.up * playerData.jumpForce, ForceMode2D.Impulse);

        //jumpsRemaining--;

        //if(jumpSFX != null && sfxSource != null)
        //{
        //    sfxSource.clip = jumpSFX;
        //    sfxSource.Play();
        //}

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
