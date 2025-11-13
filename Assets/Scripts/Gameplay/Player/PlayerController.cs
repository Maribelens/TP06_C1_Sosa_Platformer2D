using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerController : MonoBehaviour
{
    // --------------------- VARIABLES DE OTROS SCRIPTS ---------------------
    [Header("Scripts")]
    [SerializeField] private PlayerDataSo playerData;
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private GameManager gameManager;

    // --------------------- COMPONENTES ---------------------
    private Rigidbody2D rigidBody;
    private Animator animator;

    [Header("Jump")]
    [SerializeField] private LayerMask layerMaskGround;
    [SerializeField] private int maxJumps;
    private int currentJumps;

    [Header("Fire")]
    [SerializeField] private Transform firePoint;

    [Header("Damage")]
    [SerializeField] private GameObject deathEffectPrefab;
    [SerializeField] private int damage = 20;
    private float damageCooldown = 1f;
    private float lastDamageTime;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jumpClipSFX;
    [SerializeField] private AudioClip throwClipSFX;


    // --------------------- MÁQUINA DE ESTADOS ---------------------
    private List<State> states = new List<State>();
    [SerializeField] private State currentState;
    [SerializeField] private State previousState;

    // --------------------- MÉTODOS UNITY ---------------------
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthSystem = GetComponent<HealthSystem>();

        healthSystem.onDie += HealthSystem_onDie;
        healthSystem.onTakeDamage += OnTakeDamage;
    }

    private void Start()
    {
        states.Add(new StateIdle(this, playerData));
        states.Add(new StateWalk(this, playerData));
        states.Add(new StateJump(this, playerData));
        states.Add(new StateAttack(this));
        states.Add(new StateHurt(this));

        SwapStateTo(AnimationStates.Idle);
    }
    private void Update()
    {
        if (currentState != null)
            currentState.Update();
    }


    // --------------------- GESTIÓN DE ESTADOS ---------------------
    public void SwapStateTo(AnimationStates nextState)
    {
        foreach (State state in states)
        {
            if (state.state == nextState)
            {
                currentState?.OnExit();
                previousState = currentState;
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

    // --------------------- FUNCIONES DE MOVIMIENTO ---------------------

    public bool IsGroundCollision()
    {
        float radius = 0.3f;
        float distanceOffset = 0.25f;

        Vector3 offset = -transform.up * distanceOffset;
        Vector2 center = transform.position + offset;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(center, radius, Vector2.down, 0.2f, layerMaskGround);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform != transform && !hit.collider.isTrigger)
            {
                return true;
            }
        }
        Debug.DrawRay(center, Vector2.down * 0.1f, Color.red);
        return false;
    }

    /*Vector3 direction, float horizontalInput*/
    public void Movement(Vector3 direction, float horizontalInput)
    {
        if (!IsGroundCollision()) return;

        //Vector2 direction = new Vector2(horizontalInput, 0);
        rigidBody.AddForce(direction * playerData.speed * Time.deltaTime, ForceMode2D.Force);

        if ((rigidBody.velocity.x > 0 && !LookingRight()) || (rigidBody.velocity.x < 0 && LookingRight()))
        {
            TurnAround();
        }
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

    public float GetVelocityX()
    {
        return rigidBody.velocity.x;
    }

    public void Jump()
    {
        if (!IsGroundCollision() && currentJumps >= maxJumps) return;

        currentJumps++;
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
        rigidBody.AddForce(Vector2.up * playerData.jumpForce, ForceMode2D.Impulse);

        audioSource.clip = jumpClipSFX;
        audioSource.Play();
    }

    public void ResetJumps()
    {
        currentJumps = 0;
    }


    public void Fire()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Bullet bullet = Instantiate(playerData.bulletPrefab);
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = Quaternion.identity;
        bullet.gameObject.layer = LayerMask.NameToLayer("Player");

        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (targetPos - firePoint.position).normalized;

        bullet.SetBullet(20, 30, direction);

        audioSource.clip = throwClipSFX;
        audioSource.Play();
    }

    // --------------------- COLISIONES ---------------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") && Time.time > lastDamageTime + damageCooldown)
        {
            healthSystem.DoDamage(damage);
            lastDamageTime = Time.time;
        }
    }

    // --------------------- MUERTE Y DAÑO ---------------------
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

    private void OnTakeDamage()
    {
        SwapStateTo(AnimationStates.Hurt);
    }
}
