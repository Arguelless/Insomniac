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
    }
}
