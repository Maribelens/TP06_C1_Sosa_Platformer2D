using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField] private Transform target;  // Referencia al objeto que la cámara del minimapa seguirá
    private Vector3 cameraPosition;

    private void Start()
    {
        cameraPosition = transform.position;
    }

    private void Update()
    {
        // Actualiza la posición del minimapa para seguir al objetivo en el eje X
        cameraPosition.x = target.position.x;
        transform.position = cameraPosition;
    }
}
