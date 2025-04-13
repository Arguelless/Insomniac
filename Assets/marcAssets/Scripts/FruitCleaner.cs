using UnityEngine;

public class FruitCleaner : MonoBehaviour
{
    void Update()
    {
        // Si la fruta cae demasiado abajo, destrúyela
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
}
