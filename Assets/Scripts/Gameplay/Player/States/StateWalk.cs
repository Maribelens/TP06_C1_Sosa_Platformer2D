using UnityEngine;

public class StateWalk : State
{
    private float horizontalInput;
    public StateWalk(PlayerController playerController, PlayerDataSo playerData)
    {
        this.playerController = playerController;
        this.playerData = playerData;
        state = AnimationStates.Walk;
    }

    public override void OnEnter()
    {
        Debug.Log($"Entro de {state}");
        playerController.ChangeAnimatorState((int)state);
        horizontalInput = 0f;
    }

    public override void Update()
    {
        // Conexiones de Salida
        if (Input.GetMouseButton(playerData.fireMouseButton))
            playerController.SwapStateTo(AnimationStates.Attack);
        else if (Input.GetKeyDown(playerData.keyCodeJump))
        {
            //playerController.ConsumeJump();
            playerController.SwapStateTo(AnimationStates.Jump);
        }
        // ---------- UPDATE ----------
        else if (Input.GetKey(playerData.keyCodeLeft))
        {
            playerController.Movement(Vector3.left, -1);
        }
        else if (Input.GetKey(playerData.keyCodeRight))
        {
            playerController.Movement(Vector3.right, 1);
        }
        else
            playerController.SwapStateTo(AnimationStates.Idle);
    }
}
