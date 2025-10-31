using UnityEngine;
[CreateAssetMenu(menuName = "Configs/GameConfig")]
public class GameConfigSo : ScriptableObject
{
    [Header("Enemy")]
    public int maxLife = 60;
    public float enemyDamage = 10f;

    [Header("Gameplay")]
    public float invulnerabilityTime = 3f;
}
