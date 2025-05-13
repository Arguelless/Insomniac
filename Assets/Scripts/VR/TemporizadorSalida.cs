using UnityEngine.SceneManagement;
using UnityEngine;

public class TemporizadorSalida : MonoBehaviour
{
    public float duracion = 60f;
    public PuntuacionVRManager puntuacionVRManager;

    void Start()
    {
        Invoke(nameof(CargarEscenaFin), duracion);
    }

    void CargarEscenaFin()
    {
        if (puntuacionVRManager != null)
        {
            Debug.Log("Guardando puntuación: " + puntuacionVRManager.puntuacionVR); // DEBUG
            PlayerPrefs.SetInt("PuntuacionFinVR", puntuacionVRManager.puntuacionVR);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogWarning("puntuacionVRManager es NULL"); // DEBUG
        }

        SceneManager.LoadScene("FinVR");
    }
}
