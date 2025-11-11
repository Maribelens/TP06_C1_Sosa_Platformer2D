using Unity.VisualScripting;
using UnityEngine;

public class StateIdle : State
{
    public StateIdle(PlayerController playerController, PlayerDataSo playerDate)
    {
        this.playerController = playerController;
        this.playerData = playerDate;
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
        if (Input.GetKeyDown(playerData.keyCodeLeft) || Input.GetKeyDown(KeyCode.D))
            playerController.SwapStateTo(AnimationStates.Walk);
        else if (Input.GetMouseButtonDown(playerData.fireMouseButton))
            playerController.SwapStateTo(AnimationStates.Attack);
        else if (Input.GetKeyDown(playerData.keyCodeJump))
            //playerController.ConsumeJump();
            playerController.SwapStateTo(AnimationStates.Jump);
    }
}
