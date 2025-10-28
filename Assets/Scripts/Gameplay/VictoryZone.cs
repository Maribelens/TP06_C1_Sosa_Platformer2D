using UnityEngine;

public class VictoryZone : MonoBehaviour
{

    [SerializeField] private GameManager gmManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gmManager.PlayerVictory();
        }
    }
}
