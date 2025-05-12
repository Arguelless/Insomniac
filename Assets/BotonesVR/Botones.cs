using UnityEngine;
using UnityEngine.SceneManagement;

public class Botones : MonoBehaviour
{
    public string MainMenu;
    public string Puntuacion;

    public void SalirAlMenu()
    {
        string juegoActual = SceneManager.GetActiveScene().name; // Obtiene el nombre de la escena actual
        SceneManager.UnloadSceneAsync(juegoActual);
        SceneManager.LoadScene(MainMenu);
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void SaltarJuego()
    {
        string juegoActual = SceneManager.GetActiveScene().name; // Obtiene el nombre de la escena actual
        SceneManager.UnloadSceneAsync(juegoActual);
        SceneManager.LoadScene(Puntuacion);
    }
}
