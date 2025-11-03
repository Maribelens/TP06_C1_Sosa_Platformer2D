using UnityEngine;

public class VictoryZone : MonoBehaviour
{

    [SerializeField] private GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.SetGameState(GameManager.GameState.Victory);
            //gmManager.PlayerVictory();
        }
    }
}
