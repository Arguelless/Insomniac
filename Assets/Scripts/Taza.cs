using UnityEngine;

public class TazaSeguirPlayer : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        if (player != null)
        {
            // La taza solo sigue al jugador en X, y mantiene su Y
            transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        }
    }
}
