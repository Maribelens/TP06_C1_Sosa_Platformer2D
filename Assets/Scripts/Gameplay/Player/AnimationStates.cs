using UnityEngine;

public class AnimationStates : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerDataSo playerData;

    //private PlayerController player;
    //private EnemyController enemy;

    private static readonly int State = Animator.StringToHash("State");
    enum PlayerState
    {
        Idle = 1,
        Walk = 2,
        Jump = 3,
        Hurt = 4,
    };

    [SerializeField] private PlayerState currentState = PlayerState.Idle;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        UpdateAnimatorState(currentState);

    }
    private void Update()
    {
        bool isMoving = Input.GetKey(playerData.keyCodeRight) || Input.GetKey(playerData.keyCodeLeft);
        bool isJumping = Input.GetKey(playerData.keyCodeJump);

        PlayerState newState = currentState;

        if (isJumping)
        {
            newState = PlayerState.Jump;
        }
        else if (isMoving)
        {
            newState = PlayerState.Walk;
        }
        else
        {
            newState = PlayerState.Idle;
        }

        if(newState != currentState)
        {
            currentState = newState;
            UpdateAnimatorState(currentState);
        }

        //CancelInvoke(nameof(ResetToIdle));
    }

    public void ResetToIdle()
    {
        currentState = PlayerState.Idle;
        UpdateAnimatorState(currentState);
    }

    private void UpdateAnimatorState(PlayerState state)
    {
        animator.SetInteger(State, (int)state);
    }
}
