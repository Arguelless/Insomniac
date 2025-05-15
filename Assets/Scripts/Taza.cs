using UnityEngine;

public class TazaSeguirPlayer : MonoBehaviour
{
    public Transform player; //tag player a la taza

    void Update()
    {
        if (player != null)
        {
            // La taza solo sigue al jugador en X, y mantiene su Y. Siempre va a caer dentro
            transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        }
    }
}
