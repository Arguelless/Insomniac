using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MostrarPuntuacion : MonoBehaviour
{
    public TextMeshProUGUI textoPuntuacionTotal; // Asigna aqu� el TextMeshProUGUI en el Inspector

    void Start()
    {
        // Asegurarse de que el PuntuacionManager existe
        if (PuntuacionManager.Instance != null)
        {
            // Calcular la puntuaci�n total
            int puntuacionTotal = PuntuacionManager.Instance.ObtenerPuntuacionTotal();

            // Actualizar el TextMeshProUGUI si est� asignado
            if (textoPuntuacionTotal != null)
            {
                textoPuntuacionTotal.text = puntuacionTotal.ToString();
                Debug.Log("Puntuaci�n Total mostrada: " + puntuacionTotal);
            }
            else
            {
                Debug.LogError("TextMeshProUGUI 'textoPuntuacionTotal' no asignado en el Inspector del objeto MostrarPuntuacion.");
            }
        }
        else
        {
            Debug.LogError("PuntuacionManager no encontrado en la escena Puntuacion.");
        }
    }
}
