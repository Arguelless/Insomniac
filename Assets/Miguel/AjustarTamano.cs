using UnityEngine;

public class AjustarTamano : MonoBehaviour
{
    public GameObject ficha; // Asigna la ficha (con el SpriteRenderer)
    public GameObject pelota; // Asigna la pelota

    void Start()
    {
        AjustarTamanoAProporcion();
    }

    void AjustarTamanoAProporcion()
    {
        // Obtener el radio de la pelota (en realidad su escala)
        float radioPelota = pelota.transform.localScale.x;

        // Ajustar la escala de la ficha para que se ajuste al tamaño de la pelota
        ficha.transform.localScale = new Vector3(radioPelota, radioPelota, 1);
    }
}
