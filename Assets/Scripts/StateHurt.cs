using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class StateHurt : State
{
    //HealthSystem healthSystem;
    private float hurtDuration = 0.5f;
    private float timer;
    Vector2 knockBackDirection;

    public StateHurt(PlayerController playerController)
    {
        this.playerController = playerController;
        state = AnimationStates.Hurt;
    }

    public override void OnEnter()
    {
        Debug.Log($"Entro de {state}");
        //Animacion
        playerController.ChangeAnimatorState((int)state);

        //knockBack
        Rigidbody2D rb = playerController.GetComponent<Rigidbody2D>();
        knockBackDirection = new Vector2(-playerController.transform.localScale.x * 3f, 2f);
        rb.velocity = Vector2.zero;
        rb.AddForce(knockBackDirection, ForceMode2D.Impulse);

        timer = hurtDuration;

    }

    public override void Update()
    {
        // Conexiones de Salida

        // ---------- UPDATE ----------
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            if (playerController.IsGroundCollision())
                playerController.SwapStateTo(AnimationStates.Idle);
            else
                playerController.SwapStateTo(AnimationStates.Jump);
        }
    }
}
