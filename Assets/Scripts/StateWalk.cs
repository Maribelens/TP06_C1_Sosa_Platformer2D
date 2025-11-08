using UnityEngine;
using UnityEngine.LowLevel;

public class StateWalk : State
{
    PlayerAudio playerAudio;
    HealthSystem healthSystem;
    public StateWalk(PlayerController playerController)
    {
        this.playerController = playerController;
        state = AnimationStates.Walk;
    }

    public override void OnEnter()
    {
        Debug.Log($"Entro de {state}");
        playerController.ChangeAnimatorState((int)state);
    }

    public override void Update()
    {
        // Conexiones de Salida
        if (Input.GetMouseButton(0))
            playerController.SwapStateTo(AnimationStates.Attack);
        else if (Input.GetKeyDown(KeyCode.Space))
            playerController.SwapStateTo(AnimationStates.Jump);

        // ---------- UPDATE ----------
            else if (Input.GetKey(KeyCode.A))
            {
                playerController.Movement(Vector3.left, -1);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                playerController.Movement(Vector3.right, 1);
            }    
        else
            playerController.SwapStateTo(AnimationStates.Idle);
    }
}
