using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFinal : MonoBehaviour
{
    public GameObject PanelFinal;
    public string MainMenu;
    public string Juego2D_2;
    public Temporizador temporizador;

    // ... (Otras funciones) ...

    public void SalirAlMenu()
    {
        if (PanelFinal != null)
        {
            PanelFinal.SetActive(false);
        }

        // Encontrar la instancia persistente de MenuPrincipal
        MenuPrincipal menuPrincipal = FindObjectOfType<MenuPrincipal>();
        if (menuPrincipal != null)
        {
            menuPrincipal.bucle = false; // Asegurarse de que el bucle esté desactivado
            menuPrincipal.gameObject.SetActive(true); // Asegurarse de que esté activo en el menú
            Debug.Log("MenuPrincipal encontrado y activado al salir al menú.");
        }
        else
        {
            Debug.LogError("No se encontró la instancia de MenuPrincipal al salir al menú.");
        }

        string juegoActual = SceneManager.GetActiveScene().name;
        SceneManager.UnloadSceneAsync(juegoActual);
        SceneManager.LoadScene(MainMenu);
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
