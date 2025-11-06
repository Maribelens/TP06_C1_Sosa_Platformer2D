using UnityEngine;

[CreateAssetMenu(menuName = "Configs/PlayerConfig")]
public class PlayerDataSo : ScriptableObject
{
    [Header("Inputs")]
    public KeyCode keyCodeJump = KeyCode.Space;
    public KeyCode keyCodeLeft = KeyCode.LeftArrow;
    public KeyCode keyCodeLeftAlt = KeyCode.A;
    public KeyCode keyCodeRight = KeyCode.RightArrow;
    public KeyCode keyCodeRightAlt = KeyCode.D;

    [Header("PlayerSettings")]
    public int maxLife = 100;
    public float speed = 1000f;
    public float jumpForce = 6f;

    [Header("Bullet")]
    public Bullet bulletPrefab;
}
