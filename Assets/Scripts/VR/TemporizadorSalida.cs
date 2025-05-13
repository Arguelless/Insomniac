using UnityEngine;
using UnityEngine.SceneManagement;

public class TemporizadorSalida : MonoBehaviour
{
    public float duracion = 60f; // 1 minuto

    void Start()
    {
        Invoke(nameof(CargarEscenaFin), duracion);
    }

    void CargarEscenaFin()
    {
        SceneManager.LoadScene("FinVR");
    }
}
