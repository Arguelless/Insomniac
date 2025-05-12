using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;
using System.Collections;

public class SaltarJuego : MonoBehaviour
{
    public string JuegoVR;
    public string MainMenu;

    public void IniciarCambioAEscenaVR()
    {
        string juegoActual = SceneManager.GetActiveScene().name; // Obtiene el nombre de la escena actual
        SceneManager.UnloadSceneAsync(juegoActual);
        SceneManager.LoadScene(JuegoVR);
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void SalirAlMenu()
    {
        string juegoActual = SceneManager.GetActiveScene().name; // Obtiene el nombre de la escena actual
        SceneManager.UnloadSceneAsync(juegoActual);
        SceneManager.LoadScene(MainMenu);
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
