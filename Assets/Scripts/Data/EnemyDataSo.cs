using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EnemyConfig")]

public class EnemyDataSo :  ScriptableObject
{
    public int maxLife = 60;
    public float enemyDamage = 10f;
}
