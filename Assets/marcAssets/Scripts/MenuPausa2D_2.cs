using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa2D_2 : MonoBehaviour
{
    public GameObject PanelPausa;
    public string MainMenu;
    public string Juego2D_3;

    public void ControlMenu()
    {
        bool isActive = PanelPausa.activeSelf;
        PanelPausa.SetActive(!isActive);  // Alternar la visibilidad del men�.

        // Si el men� est� abierto, pausamos el juego.
        if (!isActive)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void SalirAlMenu()
    {
        if (PanelPausa != null)
        {
            PanelPausa.SetActive(false); // Desactiva el panel del men� de pausa
        }

        string juegoActual = SceneManager.GetActiveScene().name; // Obtiene el nombre de la escena actual
        SceneManager.UnloadSceneAsync(juegoActual);
        SceneManager.LoadScene(MainMenu);
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void SaltarAlJuego2D_3()
    {
        if (PanelPausa != null)
        {
            PanelPausa.SetActive(false); // Desactiva el panel del men� de pausa
        }

        string juegoActual = SceneManager.GetActiveScene().name; // Obtiene el nombre de la escena actual
        SceneManager.UnloadSceneAsync(juegoActual);
        SceneManager.LoadScene(Juego2D_3);
    }

}
