using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/PlayerConfig")]
public class PlayerDataSo : ScriptableObject
{
    [Header("Inputs")]
    public KeyCode keyCodeLeft = KeyCode.A;
    public KeyCode keyCodeRight = KeyCode.D;
    public KeyCode keyCodeJump = KeyCode.Space;
    public int fireMouseButton = 0; // 0 = click izquierdo


    [Header("PlayerSettings")]
    public int maxLife = 100;
    public float speed = 1000f;
    public float jumpForce = 6f;

    [Header("Bullet")]
    public Bullet bulletPrefab;
}
