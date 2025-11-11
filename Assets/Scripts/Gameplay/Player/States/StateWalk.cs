using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.LowLevel;

public class StateWalk : State
{
    //HealthSystem healthSystem;
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
    }

    //public override void ReadInput()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //        playerController.SwapStateTo(AnimationStates.Jump);
    //    else if (Input.GetKeyUp(KeyCode.A) && Input.GetKeyUp(KeyCode.D))
    //        playerController.SwapStateTo(AnimationStates.Idle);

    //    // Capturás input, pero no movés todavía
    //    if (Input.GetKey(KeyCode.A))
    //        horizontalInput = -1;
    //    else if (Input.GetKey(KeyCode.D))
    //        horizontalInput = 1;
    //    else
    //        horizontalInput = 0;
    //}
    
    //public override void FixedUpdate()
    //{
    //    if (horizontalInput != 0)
    //        playerController.Movement(Vector3.right * horizontalInput);
    //}
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
