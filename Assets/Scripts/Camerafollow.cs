using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // El jugador a seguir
    public float smoothSpeed = 0.125f; // Qué tan suave es el movimiento
    public Vector3 offset; // Offset para la cámara (puedes ajustar la posición relativa de la cámara)

    void LateUpdate()
    {
        if (player != null)
        {
            // Nueva posición con la misma Z de la cámara, pero siguiendo el eje X y Y del jugador
            Vector3 targetPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);

            // Transición suave entre la posición actual de la cámara y la nueva posición
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        }
    }
}

