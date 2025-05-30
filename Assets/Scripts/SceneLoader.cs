using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void CargarSiguienteEscena()
    {
        Debug.Log("Boton 'Siguiente Juego' presionado.");
        MenuPrincipal menuPrincipal = FindObjectOfType<MenuPrincipal>();

        if (menuPrincipal != null)
        {
            Debug.Log("MenuPrincipal encontrado. Llamando a JuegoTerminado().");
            menuPrincipal.JuegoTerminado();
        }
        else
        {
            Debug.LogError("No se encontró el objeto MenuPrincipal en la escena.");
        }
    }

    // Alternativamente, puedes usar por nombre:
    public void CargarEscenaPorNombre(string nombre)
    {
        SceneManager.LoadSceneAsync(nombre);
    }

    void Start()
    {
        Debug.Log("SceneLoader Start en escena: " + SceneManager.GetActiveScene().name);
    }

    void OnEnable()
    {
        Debug.Log("SceneLoader OnEnable en escena: " + SceneManager.GetActiveScene().name);
    }
}
