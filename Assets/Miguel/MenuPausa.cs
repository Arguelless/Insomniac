using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject PanelPausa;
    public string MainMenu;
    public Temporizador temporizador;
    public GameObject botonPausa; // Arrastra el GameObject del bot�n de pausa aqu�
    private MenuPrincipal menuPrincipal;

    void Start()
    {
        // Buscar la instancia de MenuPrincipal al inicio
        menuPrincipal = FindObjectOfType<MenuPrincipal>();

        // Asegurarse de que el bot�n de pausa est� asignado
        if (botonPausa == null)
        {
            Debug.LogError("El GameObject del bot�n de Pausa no est� asignado en el Inspector de MenuPausa.");
        }

        // Establecer la visibilidad inicial del bot�n de pausa
        ActualizarVisibilidadBotonPausa();
    }

    void Update()
    {
        // Actualizar la visibilidad del bot�n de pausa en cada frame (o cuando cambie el estado del bucle)
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
            // Si no se encuentra MenuPrincipal, mostrar el bot�n de pausa (por precauci�n)
            botonPausa.SetActive(true);
        }
    }

    public void ControlMenu()
    {
        bool isActive = PanelPausa.activeSelf;
        PanelPausa.SetActive(!isActive);  // Alternar la visibilidad del men�.

        // Si el men� est� abierto, pausamos el juego.
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
            PanelPausa.SetActive(false); // Desactiva el panel del men� de pausa
        }

        Time.timeScale = 1f;

        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene(MainMenu);
    }
}