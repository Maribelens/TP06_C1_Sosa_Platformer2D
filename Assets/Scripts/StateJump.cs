using System;
using UnityEngine;

public class StateJump : State
{
    private float timeOnAir = 0;

    public StateJump(PlayerController playerController)
    {
        this.playerController = playerController;
        state = AnimationStates.Jump;
    }

    public override void OnEnter()
    {
        Debug.Log($"Entro de {state}");
        playerController.ChangeAnimatorState((int)state);

        timeOnAir = 1f;
        playerController.Jump();
    }

    public override void Update()
    {
        timeOnAir -= Time.deltaTime;
        // Con raycast saber si toco el piso -> Me voy del estado Jump
        if (timeOnAir < 0 && playerController.IsGroundCollision())
        {
            // Con la velocidad del RB.x Sabemos si ir a Move o Idle

            if (Math.Abs(playerController.GetVelocityX()) > 0.1f)
                playerController.SwapStateTo(AnimationStates.Walk);
            else
                playerController.SwapStateTo(AnimationStates.Idle);
        }
        else if (Input.GetKey(KeyCode.A))
            playerController.Movement(Vector3.left * 0.7f, -1);
        else if (Input.GetKey(KeyCode.D))
            playerController.Movement(Vector3.right * 0.7f, 1);
    }

    public override void OnExit()
    {
        // Disparo particulas de polvito
    }
}
