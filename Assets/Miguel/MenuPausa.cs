using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject PanelPausa;
    public string MainMenu;
    public Temporizador temporizador;
    public GameObject botonPausa; // Arrastra el GameObject del botón de pausa aquí
    private MenuPrincipal menuPrincipal;

    void Start()
    {
        // Buscar la instancia de MenuPrincipal al inicio
        menuPrincipal = FindObjectOfType<MenuPrincipal>();

        // Asegurarse de que el botón de pausa esté asignado
        if (botonPausa == null)
        {
            Debug.LogError("El GameObject del botón de Pausa no está asignado en el Inspector de MenuPausa.");
        }

        // Establecer la visibilidad inicial del botón de pausa
        ActualizarVisibilidadBotonPausa();
    }

    void Update()
    {
        // Actualizar la visibilidad del botón de pausa en cada frame (o cuando cambie el estado del bucle)
        ActualizarVisibilidadBotonPausa();
    }

    void ActualizarVisibilidadBotonPausa()
    {
        if (menuPrincipal != null && botonPausa != null)
        {
            botonPausa.SetActive(!menuPrincipal.bucle); // Ocultar si bucle es true
        }
        else if (botonPausa != null)
        {
            // Si no se encuentra MenuPrincipal, mostrar el botón de pausa (por precaución)
            botonPausa.SetActive(true);
        }
    }

    public void ControlMenu()
    {
        bool isActive = PanelPausa.activeSelf;
        PanelPausa.SetActive(!isActive);  // Alternar la visibilidad del menú.

        // Si el menú está abierto, pausamos el juego.
        if (!isActive)
        {
            Time.timeScale = 0;
            temporizador.PausarTemporizador();
        }
        else
        {
            Time.timeScale = 1;
            temporizador.ReanudarTemporizador();
        }
    }

    public void ReiniciarJuego()
    {
        Time.timeScale = 1;
        temporizador.ReiniciarTemporizador();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Recargar la escena actual.
    }

    public void SalirAlMenu()
    {
        if (PanelPausa != null)
        {
            PanelPausa.SetActive(false); // Desactiva el panel del menú de pausa
        }

        Time.timeScale = 1f;

        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene(MainMenu);
    }
}