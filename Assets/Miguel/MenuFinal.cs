using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFinal : MonoBehaviour
{
    public GameObject PanelFinal;
    public string MainMenu;
    public string Juego2D_2;
    public Temporizador temporizador;

    public void ReiniciarJuego()
    {
        Time.timeScale = 1;
        temporizador.ReiniciarTemporizador();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Recargar la escena actual.
    }

    public void SaltarJuego()
    {
        if (PanelFinal != null)
        {
            PanelFinal.SetActive(false); // Desactiva el panel del menú de pausa
        }

        string juegoActual = SceneManager.GetActiveScene().name; // Obtiene el nombre de la escena actual
        SceneManager.UnloadSceneAsync(juegoActual);
        SceneManager.LoadScene(Juego2D_2);
    }

    public void SalirAlMenu()
    {
        if (PanelFinal != null)
        {
            PanelFinal.SetActive(false); // Desactiva el panel del menú de pausa
        }

        string juegoActual = SceneManager.GetActiveScene().name; // Obtiene el nombre de la escena actual
        SceneManager.UnloadSceneAsync(juegoActual);
        SceneManager.LoadScene(MainMenu);
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
