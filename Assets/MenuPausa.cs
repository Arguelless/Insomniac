using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject PanelPausa;

    public void ControlMenu()
    {
        bool isActive = PanelPausa.activeSelf;
        PanelPausa.SetActive(!isActive);  // Alternar la visibilidad del menú.

        // Si el menú está abierto, pausamos el juego.
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
        Time.timeScale = 1;  // Asegurarnos de que el tiempo está normalizado al reiniciar.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Recargar la escena actual.
    }

    public void SaltarJuego()
    {
        // Este código depende de cómo quieras "saltar", por ejemplo cargando otra escena:
        // SceneManager.LoadScene("SiguienteNivel"); // Aquí "SiguienteNivel" es el nombre de la escena.
        // O si es solo saltar una secuencia del juego, tienes que hacer el código correspondiente.
        Debug.Log("Saltar juego (funcionalidad aún no implementada).");
    }
}