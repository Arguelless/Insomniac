using UnityEngine;
using UnityEngine.SceneManagement;

public class Boton : MonoBehaviour
{
    public string MainMenu;

    public void SalirAlMenu()
    {
        string juegoActual = SceneManager.GetActiveScene().name; // Obtiene el nombre de la escena actual
        SceneManager.UnloadSceneAsync(juegoActual);
        SceneManager.LoadScene(MainMenu);
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
