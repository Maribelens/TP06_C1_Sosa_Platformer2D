using System;
using UnityEngine;

public class StateJump : State
{
    //private float timeOnAir = 0;
    private bool hasJumped = false;

    public StateJump(PlayerController playerController, PlayerDataSo playerData)
    {
        this.playerController = playerController;
        this.playerData = playerData;
        state = AnimationStates.Jump;
    }

    public override void OnEnter()
    {
        Debug.Log($"Entro de {state}");
        playerController.ChangeAnimatorState((int)state);

        //timeOnAir = 1f;
        playerController.Jump();
        hasJumped = true;
    }

    public override void Update()
    {
        //timeOnAir -= Time.deltaTime;

        //if(hasJumped && playerController.CanJump())
        //{
        //    playerController.ConsumeJump();
        //}

        // Con raycast saber si toco el piso -> Me voy del estado Jump
        if (hasJumped && playerController.IsGroundCollision())
        {
            // Con la velocidad del RB.x Sabemos si ir a Move o Idle
            hasJumped = false;
            //playerController.ResetJumps();

            if (Math.Abs(playerController.GetVelocityX()) > 0.1f)
                playerController.SwapStateTo(AnimationStates.Walk);
            else
                playerController.SwapStateTo(AnimationStates.Idle);
        }
        else if (Input.GetKey(playerData.keyCodeLeft))
            playerController.Movement(Vector3.left * 0.7f, -1);
        else if (Input.GetKey(playerData.keyCodeRight))
            playerController.Movement(Vector3.right * 0.7f, 1);
    }

    public override void OnExit()
    {
        // Disparo particulas de polvito
    }
}
