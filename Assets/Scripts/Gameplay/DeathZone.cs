using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.SetGameState(GameManager.GameState.GameOver);
            //gmManager.OnStateChanged += HandleChange;
        }
    }

}

