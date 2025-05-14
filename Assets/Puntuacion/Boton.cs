using UnityEngine;
using UnityEngine.SceneManagement;

public class Boton : MonoBehaviour
{
    public string MainMenu;
    private MenuPrincipal menuPrincipal;

    void Start()
    {
        menuPrincipal = FindObjectOfType<MenuPrincipal>();
    }

    public void SalirAlMenu()
    {
        if (menuPrincipal != null)
        {
            menuPrincipal.bucle = false; // Asegurarse de que el bucle esté desactivado al volver al menú
            Debug.Log("Bucle desactivado al volver al menú principal.");
            // *** NO DESACTIVAR EL GAMEOBJECT DE MenuPrincipal AQUÍ ***
            Debug.Log("GameObject de MenuPrincipal se mantiene activo.");
        }
        else
        {
            Debug.LogError("No se encontró el objeto MenuPrincipal al volver al menú principal.");
        }

        string juegoActual = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(MainMenu);
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
