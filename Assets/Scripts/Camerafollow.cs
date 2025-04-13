using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // El jugador a seguir
    public float smoothSpeed = 0.125f; // Qu� tan suave es el movimiento
    public Vector3 offset; // Offset para la c�mara (puedes ajustar la posici�n relativa de la c�mara)

    void LateUpdate()
    {
        if (player != null)
        {
            // Nueva posici�n con la misma Z de la c�mara, pero siguiendo el eje X y Y del jugador
            Vector3 targetPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);

            // Transici�n suave entre la posici�n actual de la c�mara y la nueva posici�n
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        }
    }
}

