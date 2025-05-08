using UnityEngine;

public class ZonaImpacto : MonoBehaviour
{
    public int puntos = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Forma"))
        {
            puntos += 100;
            Destroy(other.gameObject);
            Debug.Log("¡Puntos! Total: " + puntos);
        }
    }
}
