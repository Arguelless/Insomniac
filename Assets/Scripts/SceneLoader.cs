using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void CargarSiguienteEscena()
    {
        // Carga la siguiente escena en el Build Index
        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(escenaActual + 1);
    }

    // Alternativamente, puedes usar por nombre:
    public void CargarEscenaPorNombre(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }
}
