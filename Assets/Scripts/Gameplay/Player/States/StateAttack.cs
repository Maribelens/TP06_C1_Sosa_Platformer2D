using UnityEngine;

public class StateAttack : State
{
    private float attackDuration = 0.4f;
    private float timer;
    private bool hasFired = false;

    public StateAttack(PlayerController playerController)
    {
        this.playerController = playerController;
        state = AnimationStates.Attack;
    }

    public override void OnEnter()
    {
        Debug.Log($"Entro en {state}");
        playerController.ChangeAnimatorState((int)state);
        timer = attackDuration;
        hasFired = false;

        playerController.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    public override void Update()
    {
        timer -= Time.deltaTime;

        if (!hasFired)
        {
            playerController.Fire();
            hasFired = true;
        }
        if (timer <= 0)
        {
            playerController.SwapStateTo(AnimationStates.Idle);
        }
    }

    public override void OnExit()
    {
        //Debug.Log($"Salgo de {state}");
    }
}
