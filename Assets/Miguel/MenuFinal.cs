using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFinal : MonoBehaviour
{
    public GameObject PanelFinal;
    public string MainMenu;
    public string Juego2D_1;
    public Temporizador temporizador;
    public GameObject botonReiniciar;

    private MenuPrincipal menuPrincipal;

    void Start()
    {

        menuPrincipal = FindObjectOfType<MenuPrincipal>();


    }

    void Update()
    {


    }



    public void Reiniciar()
    {
        if (PanelFinal != null && PanelFinal.activeSelf)
        {
            PanelFinal.SetActive(false);
        }

        // Reiniciar el temporizador si tienes una referencia a él
        if (temporizador != null)
        {
            temporizador.ReiniciarTemporizador(); // Asumiendo que tienes este método
        }

        SceneManager.LoadSceneAsync(Juego2D_1);

        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    public void cargarSiguienteJuego()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadSceneAsync("Juego2D_2");
        if (MenuPrincipal.instance.bucle == true)
        {
            MenuPrincipal.instance.currentGameIndex++;
        } 
    }

    public void SalirAlMenu()
    {
        if (PanelFinal != null)
        {
            PanelFinal.SetActive(false);
        }

        if (MenuPrincipal.instance != null)
        {
            if (MenuPrincipal.instance.bucle == true)
            {
                MenuPrincipal.instance.bucle = false;
                MenuPrincipal.instance.currentGameIndex = 0;
            }           
            Debug.Log("Estado de MenuPrincipal reseteado: Bucle desactivado, índice de juego a 0.");

            if (MenuPrincipal.instance.mainMenu != null)
            {
                MenuPrincipal.instance.mainMenu.SetActive(true);
                Debug.Log("UI del MenuPrincipal activada.");
            }
            else
            {
                Debug.LogError("Error: La referencia 'mainMenu' en MenuPrincipal no está asignada. Asigna el GameObject de la UI del menú en el Inspector.");
            }
        }
        else
        {
            Debug.LogError("Error: La instancia de MenuPrincipal (Singleton) no se encontró. ¿Ha sido destruida por error?");
            return;
        }

        Time.timeScale = 1f;
        string juegoActual = SceneManager.GetActiveScene().name;
        SceneManager.UnloadSceneAsync(juegoActual);
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
