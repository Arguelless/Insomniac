using UnityEngine;
using UnityEngine.SceneManagement;

public class SalirFinVR : MonoBehaviour
{
    public float tiempoHastaTransicion = 10f;

    void Start()
    {
        Invoke(nameof(CargarEscenaPuntuacion), tiempoHastaTransicion);
    }

    void CargarEscenaPuntuacion()
    {
        SceneManager.LoadScene("Puntuacion");
    }
}
