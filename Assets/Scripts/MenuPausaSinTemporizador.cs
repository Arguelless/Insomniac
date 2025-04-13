using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausaSinTemporizador : MonoBehaviour
{
    public GameObject PanelPausa;
    public string MainMenu;
    public string JuegoAR;

    public void ControlMenu()
    {
        bool isActive = PanelPausa.activeSelf;
        PanelPausa.SetActive(!isActive);

        if (!isActive)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void ReiniciarJuego()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SaltarJuego()
    {
        if (PanelPausa != null)
        {
            PanelPausa.SetActive(false);
        }

        string juegoActual = SceneManager.GetActiveScene().name;
        SceneManager.UnloadSceneAsync(juegoActual);
        SceneManager.LoadScene(JuegoAR);
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void SalirAlMenu()
    {
        if (PanelPausa != null)
        {
            PanelPausa.SetActive(false);
        }

        string juegoActual = SceneManager.GetActiveScene().name;
        SceneManager.UnloadSceneAsync(juegoActual);
        SceneManager.LoadScene(MainMenu);
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
