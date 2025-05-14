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

    public void SalirAlMenu()
    {
        // Encontrar la instancia persistente de MenuPrincipal
        MenuPrincipal menuPrincipal = FindObjectOfType<MenuPrincipal>();
        if (menuPrincipal != null)
        {
            menuPrincipal.bucle = false; // Asegurarse de que el bucle est� desactivado
            menuPrincipal.gameObject.SetActive(true); // Asegurarse de que est� activo en el men�
            Debug.Log("MenuPrincipal encontrado y activado al salir al men�.");
        }
        else
        {
            Debug.LogError("No se encontr� la instancia de MenuPrincipal al salir al men�.");
        }

        string juegoActual = SceneManager.GetActiveScene().name;
        SceneManager.UnloadSceneAsync(juegoActual);
        SceneManager.LoadScene(MainMenu);
        Screen.orientation = ScreenOrientation.Portrait;

        if (PanelPausa != null)
        {
            PanelPausa.SetActive(false); // Desactivar al final
        }
    }

    void DesactivarPanelPausa()
    {
        if (PanelPausa != null)
        {
            PanelPausa.SetActive(false);
        }
    }
}
