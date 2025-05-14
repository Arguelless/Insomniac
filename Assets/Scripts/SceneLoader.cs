using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void CargarSiguienteEscena()
    {
        Debug.Log("Boton 'Siguiente Juego' presionado.");
        // Obtener una referencia al MenuPrincipal buscando un objeto del tipo MenuPrincipal en la escena
        MenuPrincipal menuPrincipal = FindObjectOfType<MenuPrincipal>();

        if (menuPrincipal != null)
        {
            Debug.Log("MenuPrincipal encontrado. Llamando a JuegoTerminado().");
            menuPrincipal.JuegoTerminado(); // Indicar que el juego terminó manualmente
        }
        else
        {
            Debug.LogError("No se encontró el objeto MenuPrincipal en la escena.");
        }
    }

    // Alternativamente, puedes usar por nombre:
    public void CargarEscenaPorNombre(string nombre)
    {
        SceneManager.LoadScene(nombre);
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
