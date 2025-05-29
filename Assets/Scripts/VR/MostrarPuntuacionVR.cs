using UnityEngine;
using TMPro;

public class MostrarPuntuacionFin : MonoBehaviour
{
    public TextMeshProUGUI textoFinal;

    void Start()
    {
        int puntuacion = PlayerPrefs.GetInt("PuntuacionFinVR", -1);
        Debug.Log("Puntuación guardada: " + puntuacion);
        textoFinal.text = "Tu puntuación en la DjExperience: " + puntuacion;

        if (PuntuacionManager.Instance != null && MenuPrincipal.instance.bucle == true)
        {
            PuntuacionManager.Instance.AsignarPuntos(4, puntuacion);
        }
        else
        {
            Debug.LogError("La instancia de PuntuacionManager no está disponible.");
        }
    }
}
