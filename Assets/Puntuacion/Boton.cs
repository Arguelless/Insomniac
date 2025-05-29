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
            if (MenuPrincipal.instance.bucle == true)
            {
                MenuPrincipal.instance.currentGameIndex = 0;
                menuPrincipal.bucle = false;
                Debug.Log("Bucle desactivado al volver al menú principal.");
                Debug.Log("GameObject de MenuPrincipal se mantiene activo.");
            }
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
