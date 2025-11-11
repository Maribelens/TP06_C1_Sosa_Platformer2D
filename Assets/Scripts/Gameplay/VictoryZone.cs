using UnityEngine;

public class VictoryZone : MonoBehaviour
{

    [SerializeField] private GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Activa la victoria si el jugador entra en la zona
        if (collision.CompareTag("Player"))
        {
            gameManager.SetGameState(GameManager.GameState.Victory);
        }
    }
}
