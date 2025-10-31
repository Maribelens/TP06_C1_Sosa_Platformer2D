using UnityEngine;

[CreateAssetMenu(menuName = "Configs/PlayerConfig")]
public class PlayerDataSo : ScriptableObject
{
    [Header("Inputs")]
    public KeyCode keyCodeJump = KeyCode.Space;
    public KeyCode keyCodeLeft = KeyCode.LeftArrow;
    public KeyCode keyCodeRight = KeyCode.RightArrow;

    [Header("PlayerSettings")]
    public int maxLife;
    public float speed = 5f;
    public float jumpForce = 6f;

    [Header("Bullet")]
    public Bullet bulletPrefab;
}
