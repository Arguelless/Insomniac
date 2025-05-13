using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void CargarSiguienteEscena()
    {
        // Obtener una referencia al MenuPrincipal (asumiendo que tienes una)
        MenuPrincipal menuPrincipal = FindObjectOfType<MenuPrincipal>();

        if (menuPrincipal != null)
        {
            menuPrincipal.JuegoTerminado(); // Indicar que el juego termin� manualmente
            menuPrincipal.CargarJuegoActual(); // Cargar la siguiente escena
        }
        else
        {
            Debug.LogError("No se encontr� el objeto MenuPrincipal en la escena.");
        }
    }

    // Alternativamente, puedes usar por nombre:
    public void CargarEscenaPorNombre(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }
}
