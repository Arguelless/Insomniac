using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject PanelPausa;

    public void ControlMenu()
    {
        bool isActive = PanelPausa.activeSelf;
        PanelPausa.SetActive(!isActive);  // Alternar la visibilidad del men�.

        // Si el men� est� abierto, pausamos el juego.
        if (!isActive)
        {
            Time.timeScale = 0;  // Pausar el juego.
        }
        else
        {
            Time.timeScale = 1;  // Reanudar el juego.
        }
    }

    public void ReiniciarJuego()
    {
        Time.timeScale = 1;  // Asegurarnos de que el tiempo est� normalizado al reiniciar.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Recargar la escena actual.
    }

    public void SaltarJuego()
    {
        // Este c�digo depende de c�mo quieras "saltar", por ejemplo cargando otra escena:
        // SceneManager.LoadScene("SiguienteNivel"); // Aqu� "SiguienteNivel" es el nombre de la escena.
        // O si es solo saltar una secuencia del juego, tienes que hacer el c�digo correspondiente.
        Debug.Log("Saltar juego (funcionalidad a�n no implementada).");
    }
}