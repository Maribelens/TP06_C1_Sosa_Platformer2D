using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerController : MonoBehaviour
{
    // --------------------- VARIABLES DE OTROS SCRIPTS ---------------------
    [Header("Scripts")]
    [SerializeField] private PlayerDataSo playerData;       //Contiene velocidad, fuerza de salto y prefab de bullet
    //public PlayerDataSo PlayerData => playerData;           // getter público opcional
    [SerializeField] private HealthSystem healthSystem;     //sistema de vida, daño y muerte
    [SerializeField] private GameManager gameManager;       //Administra estados del juego


    // --------------------- COMPONENTES ---------------------
    private Rigidbody2D rigidBody;
    private Animator animator;     

    [Header("Jump")]
    private bool canMoveDuringJump;     //control de movimiento de salto
    [SerializeField] private LayerMask layerMaskGround;     // Para detección de colisiones con el suelo
    [SerializeField] private int maxJumps;
    private int currentJumps;

    [Header("Fire")]
    [SerializeField] private Transform firePoint;   //punto de disparo

    [Header("Damage")]
    [SerializeField] private GameObject deathEffectPrefab;      //instancia de efecto de muerte
    [SerializeField] private int damage = 20;       //cantidad de daño recibido
    private float damageCooldown = 1f;      //tiempo de no ser dañado
    private float lastDamageTime;       //tiempo desde la ultima vez que recibio daño

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;       //emisor sonoro del jugador        
    [SerializeField] private AudioClip jumpClipSFX;         //asset de sonido de salto
    [SerializeField] private AudioClip throwClipSFX;        //asset de sonido de lanzar


    // --------------------- MÁQUINA DE ESTADOS ---------------------
    private List<State> states = new List<State>(); // Lista de todos los estados posibles
    [SerializeField] private State currentState;    //Estado actual
    [SerializeField] private State previousState;   //Estado previo



    // --------------------- MÉTODOS UNITY ---------------------
    private void Awake()
    {
        // Obtener referencias a componentes
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthSystem = GetComponent<HealthSystem>();

        healthSystem.onDie += HealthSystem_onDie;
        healthSystem.onTakeDamage += OnTakeDamage;
    }

    private void Start()
    {
        // Inicializar y agregar los estados a la lista
        states.Add(new StateIdle(this, playerData));
        states.Add(new StateWalk(this, playerData));
        states.Add(new StateJump(this, playerData));
        states.Add(new StateAttack(this));
        states.Add(new StateHurt(this));

        // Configurar el estado inicial
        SwapStateTo(AnimationStates.Idle);    
    }
    private void Update()
    {
        // Ejecutar la lógica del estado actual cada frame
        if (currentState != null)
        {
            //currentState.ReadInput();
            currentState.Update();
        }
           
    }

    private void FixedUpdate()
    {
        //currentState.FixedUpdate();
    }

    // --------------------- GESTIÓN DE ESTADOS ---------------------
    public void SwapStateTo(AnimationStates nextState)
    {
        // Cambiar el estado actual por otro
        foreach (State state in states)
        {
            if (state.state == nextState)
            {
                currentState?.OnExit();     // Salida del estado actual

                currentState = state;
                currentState.OnEnter();     //Entrada del nuevo estado
                break;

            }
        }
    }

    // Cambiar la animación mediante el Animator
    public void ChangeAnimatorState(int state)
    {
        animator.SetInteger("State", state);
    }

    // --------------------- FUNCIONES DE MOVIMIENTO ---------------------

    // Retorna true si el jugador está tocando el suelo, false si no
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
                Debug.Log($"Hit: {hit.transform.gameObject.name}");
                return true;
            }
        }
        Debug.DrawRay(center, Vector2.down * 0.1f, Color.red);
        return false;
    }


    // Aplica fuerza horizontal al jugador según input
    // También invierte la escala si el jugador cambia de dirección
    public void Movement(Vector3 direction, float horizontalInput)
    {
        if (!IsGroundCollision() && !canMoveDuringJump) { return; }
        rigidBody.AddForce(direction * playerData.speed * Time.deltaTime, ForceMode2D.Force);

        if ((GetVelocityX() > 0 && !LookingRight()) || (GetVelocityX() < 0 && LookingRight()))
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

    // Aplica impulso vertical al jugador y reproduce el sonido de salto
    public void Jump()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
        rigidBody.AddForce(Vector2.up * playerData.jumpForce, ForceMode2D.Impulse);
        audioSource.clip = jumpClipSFX;
        audioSource.Play();
    }

    //public void ResetJumps()
    //{
    //    currentJumps = maxJumps;
    //}
    //public void ConsumeJump()
    //{
    //    currentJumps--;
    //}
    //public bool CanJump()
    //{
    //    return currentJumps > 0;
    //}




    // Instancia y dispara un bullet hacia la posición del mouse
    // Evita disparar si el cursor está sobre un objeto UI
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
    // Aplica daño al jugador al tocar un enemigo si el cooldown ya pasó
    private void OnCollisionEnter2D(Collision2D collision)
    { 
        if (collision.collider.CompareTag("Enemy") && Time.time > lastDamageTime + damageCooldown)
        {
            healthSystem.DoDamage(damage);
            lastDamageTime = Time.time;
        }
    }

    // --------------------- MUERTE Y DAÑO ---------------------
    // Se llama al morir: instancia efecto de muerte y notifica al GameManager
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
        // Cambia el estado del jugador a "Hurt" al recibir daño
        SwapStateTo(AnimationStates.Hurt);
    }
}
