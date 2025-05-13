using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject PanelPausa;
    public string MainMenu;
    public string Juego2D_2;
    public Temporizador temporizador;
    private MenuPrincipal menuPrincipal;

    public void ControlMenu()
    {
        bool isActive = PanelPausa.activeSelf;
        PanelPausa.SetActive(!isActive);  // Alternar la visibilidad del men�.

        // Si el men� est� abierto, pausamos el juego.
        if (!isActive)
        {
            Time.timeScale = 0;
            temporizador.PausarTemporizador();
        }
        else
        {
            Time.timeScale = 1;
            temporizador.ReanudarTemporizador();
        }
    }

    public void ReiniciarJuego()
    {
        Time.timeScale = 1;
        temporizador.ReiniciarTemporizador();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Recargar la escena actual.
    }

    public void SalirAlMenu()
    {
        if (PanelPausa != null)
        {
            PanelPausa.SetActive(false); // Desactiva el panel del men� de pausa
        }

        Time.timeScale = 1f;

        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene(MainMenu);
    }
}