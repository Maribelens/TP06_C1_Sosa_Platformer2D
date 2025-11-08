using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.LowLevel;

public class PlayerController : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private PlayerDataSo playerData;
    //[SerializeField] private PlayerAudio playerAudio;
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
    //[SerializeField] private Transform groundController;
    //[SerializeField] private Vector2 boxDimensions;
    //[SerializeField] private LayerMask jumpLayers;
    //[HideInInspector] public bool isGrounded;
    //[SerializeField] private bool wasGrounded;
    [SerializeField] private bool canMoveDuringJump;
    //[SerializeField] private int maxJumps = 2;
    //private int jumpsRemaining;
    //private bool jumpInput;

    [Header("Fire")]
    [SerializeField] private Transform firePoint;

    [Header("Damage")]
    [SerializeField] private GameObject deathEffectPrefab;
    [SerializeField] private int damage = 20;
    private float damageCooldown = 1f;
    private float lastDamageTime;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip walkClipSFX;
    [SerializeField] private AudioClip jumpClipSFX;
    [SerializeField] private AudioClip throwClipSFX;
    [SerializeField] private AudioClip landClipSFX;

    private bool isWalking;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //audioSource.GetComponent<AudioSource>();

        //playerAudio = GetComponent<PlayerAudio>();
        healthSystem = GetComponent<HealthSystem>();

        //healthSystem.onLifeUpdated += OnLifeUpdated;
        healthSystem.onDie += HealthSystem_onDie;
        healthSystem.onTakeDamage += OnTakeDamage;
        //healthSystem.onTakeDamage += OnTakeDamage;
        //healthSystem.onLifeUpdated += CheckIfHurt;
    }

    private void OnTakeDamage()
    {
        //audioSource.playOnAwake = false;

        SwapStateTo(AnimationStates.Hurt);
    }

    private void Start()
    {
        states.Add(new StateIdle(this));
        states.Add(new StateWalk(this));
        states.Add(new StateJump(this));
        states.Add(new StateAttack(this));
        states.Add(new StateHurt(this));

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
        float distanceOffset = 0.5f;

        Vector3 offset = -transform.up * distanceOffset;
        Vector2 center = transform.position + offset;

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
        //playerAudio.PlayWalk();

        if ((GetVelocityX() > 0 && !LookingRight()) || (GetVelocityX() < 0 && LookingRight()))
        {
            TurnAround();
        }
        //{
        //    PlayLoop(walkClipSFX, true);
        //    isWalking = true;
        //}
        //else
        //{
        //    StopLoop();
        //    isWalking = false;
        //}
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
        return rigidBody.velocity.x;
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
    //    Jump();
    //    //jumpsRemaining = Mathf.Max(0, jumpsRemaining - 1);

    //}

    public void Jump()
    {
        //if(jumpsRemaining <= 0) { return; }
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
        rigidBody.AddForce(Vector2.up * playerData.jumpForce, ForceMode2D.Impulse);
        audioSource.clip = jumpClipSFX;
        audioSource.Play();
        //jumpsRemaining = maxJumps - 1;
        //if (isGrounded)
        //{
        //    jumpsRemaining = maxJumps - 1;
        //}
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

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireCube(groundController.position, boxDimensions);
    //}

    public void Fire()
    {
        if (EventSystem.current.IsPointerOverGameObject()) // Para saber si le pego a un objeto de UI
            return;

        Bullet bullet = Instantiate(playerData.bulletPrefab);
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = Quaternion.identity;
        bullet.gameObject.layer = LayerMask.NameToLayer("Player");

        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //targetPos.z = 0;
        //bullet.transform.LookAt(targetPos);
        Vector3 direction = (targetPos - firePoint.position).normalized;

        bullet.SetBullet(20, 30, direction);

        audioSource.clip = throwClipSFX;
        audioSource.Play();
        //playerAudio.PlayThrow();
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
