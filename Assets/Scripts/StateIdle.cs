using UnityEngine;

public class StateIdle : State
{
    public StateIdle (PlayerController playerController)
    {
        this.playerController = playerController;
        state = AnimationStates.Idle;
    }

    public override void OnEnter()
    {
        Debug.Log($"Entro de {state}");
        playerController.ChangeAnimatorState((int)state);
    }

    public override void Update()
    {
        // Conexiones de Salida
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            playerController.SwapStateTo(AnimationStates.Walk);
        else if (Input.GetKeyDown(KeyCode.LeftControl))
            playerController.SwapStateTo(AnimationStates.Attack);
        else if (Input.GetKeyDown(KeyCode.Space))
            playerController.SwapStateTo(AnimationStates.Jump);
    }
}
