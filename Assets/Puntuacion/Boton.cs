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
            menuPrincipal.bucle = false; // Asegurarse de que el bucle est� desactivado al volver al men�
            Debug.Log("Bucle desactivado al volver al men� principal.");
            // *** NO DESACTIVAR EL GAMEOBJECT DE MenuPrincipal AQU� ***
            Debug.Log("GameObject de MenuPrincipal se mantiene activo.");
        }
        else
        {
            Debug.LogError("No se encontr� el objeto MenuPrincipal al volver al men� principal.");
        }

        string juegoActual = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(MainMenu);
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
