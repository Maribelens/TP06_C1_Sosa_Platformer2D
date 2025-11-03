using UnityEngine;
[CreateAssetMenu(menuName = "Configs/GameConfig")]
public class GameConfigSo : ScriptableObject
{ 
    [Header("Gameplay")]
    public float invulnerabilityTime = 3f;
}
