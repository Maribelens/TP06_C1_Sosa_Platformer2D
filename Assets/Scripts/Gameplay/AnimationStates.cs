using UnityEngine;

public class AnimationStates : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private PlayerController player;
    public PlayerDataSo playerData;
    private static readonly int State = Animator.StringToHash("State");
    enum PlayerState
    {
        Idle = 1,
        Walk = 2,
        Jump = 3,
        Hurt = 4,
    };
    [SerializeField] private PlayerState playerState = PlayerState.Idle;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        animator.SetInteger(State, (int)playerState);

    }
    private void Update()
    {
        if (Input.GetKey(playerData.keyCodeRight) || Input.GetKey(playerData.keyCodeLeft))
        {
            playerState = PlayerState.Walk;
            animator.SetInteger(State, (int)playerState);
        }
        else
        {
            Invoke(nameof(ResetAnim), 1);
        }

        if (Input.GetKey(playerData.keyCodeJump))
        {
            playerState = PlayerState.Jump;
            animator.SetInteger(State, (int)playerState);
        }
        else
        {
            Invoke(nameof(ResetAnim), 1);
        }
    }

    private void ResetAnim()
    {
        playerState = PlayerState.Idle;
        animator.SetInteger(State, (int)playerState);
    }
}
