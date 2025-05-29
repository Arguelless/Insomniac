using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa2D_2 : MonoBehaviour
{
    public GameObject PanelPausa;
    public string MainMenu;
    public string Juego2D_3;
    public TimerControllerm timerController; // Referencia al TimerControllerm

    public void ControlMenu()
    {
        bool isActive = PanelPausa.activeSelf;
        PanelPausa.SetActive(!isActive);  // Alternar la visibilidad del men�.

        // Si el men� est� abierto, pausamos el juego.
        if (!isActive)
        {
            if (timerController != null)
            {
                timerController.PauseTimer(); // Pausa el temporizador si existe
            }
            Time.timeScale = 0;
        }
        else
        {
            if (timerController != null)
            {
                timerController.ResumeTimer(); // Reanuda el temporizador si existe
            }
            Time.timeScale = 1;
        }
    }

    public void SalirAlMenu()
    {
        if (PanelPausa != null)
        {
            PanelPausa.SetActive(false); // Desactiva el panel del men� de pausa
        }
        if (MenuPrincipal.instance.bucle == true)
        {
            MenuPrincipal.instance.currentGameIndex = 0;
        }
        string juegoActual = SceneManager.GetActiveScene().name; // Obtiene el nombre de la escena actual
        SceneManager.UnloadSceneAsync(juegoActual);
        SceneManager.LoadScene(MainMenu);
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void ReiniciarJuego()
    {
        if (PanelPausa != null)
        {
            PanelPausa.SetActive(false); // Desactiva el panel del men� de pausa
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Recargar la escena actual.
    }

    public void SaltarAlJuego2D_3()
    {
        if (PanelPausa != null)
        {
            PanelPausa.SetActive(false); // Desactiva el panel del men� de pausa
        }

        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadSceneAsync(Juego2D_3);
        if (MenuPrincipal.instance.bucle == true)
        {
            MenuPrincipal.instance.currentGameIndex++;
        }
    }

}
