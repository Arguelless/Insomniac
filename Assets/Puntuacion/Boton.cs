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
                Debug.Log("Bucle desactivado al volver al men� principal.");
                Debug.Log("GameObject de MenuPrincipal se mantiene activo.");
            }
        }
        else
        {
            Debug.LogError("No se encontr� el objeto MenuPrincipal al volver al men� principal.");
        }
        PuntuacionManager.Instance.ResetPuntuaciones(); // Asegurarse de que la puntuaci�n se guarda antes de salir
        string juegoActual = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(MainMenu);
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
