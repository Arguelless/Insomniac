using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFinal : MonoBehaviour
{
    public GameObject PanelFinal;
    public string MainMenu;
    public string Juego2D_1;
    public Temporizador temporizador;
    public GameObject botonReiniciar;

    private MenuPrincipal menuPrincipal;

    void Start()
    {

        menuPrincipal = FindObjectOfType<MenuPrincipal>();


        if (botonReiniciar == null)
        {
            Debug.LogError("El GameObject del botón Reiniciar no está asignado en el Inspector de MenuFinal.");
        }

        // Actualizar la visibilidad inicial del botón
        ActualizarVisibilidadBotonReiniciar();
    }

    void Update()
    {
        // Verificar el estado del bucle en cada frame (o cuando sea necesario)
        ActualizarVisibilidadBotonReiniciar();
    }

    void ActualizarVisibilidadBotonReiniciar()
    {
        if (menuPrincipal != null && botonReiniciar != null)
        {
            botonReiniciar.SetActive(!menuPrincipal.bucle); // Mostrar el botón si bucle es falso
        }
        else if (botonReiniciar != null)
        {
            // Si no se encuentra MenuPrincipal, asumimos que no estamos en bucle y mostramos el botón
            botonReiniciar.SetActive(true);
        }
    }

    public void Reiniciar()
    {
        if (PanelFinal != null && PanelFinal.activeSelf)
        {
            PanelFinal.SetActive(false);
        }

        // Reiniciar el temporizador si tienes una referencia a él
        if (temporizador != null)
        {
            temporizador.ReiniciarTemporizador(); // Asumiendo que tienes este método
        }

        SceneManager.LoadScene(Juego2D_1);

        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    public void SalirAlMenu()
    {
        if (PanelFinal != null)
        {
            PanelFinal.SetActive(false);
        }

        // Encontrar la instancia persistente de MenuPrincipal
        MenuPrincipal menuPrincipal = FindObjectOfType<MenuPrincipal>();
        if (menuPrincipal != null)
        {
            menuPrincipal.bucle = false; // Asegurarse de que el bucle esté desactivado
            menuPrincipal.gameObject.SetActive(true); // Asegurarse de que esté activo en el menú
            Debug.Log("MenuPrincipal encontrado y activado al salir al menú.");
        }
        else
        {
            Debug.LogError("No se encontró la instancia de MenuPrincipal al salir al menú.");
        }

        string juegoActual = SceneManager.GetActiveScene().name;
        SceneManager.UnloadSceneAsync(juegoActual);
        SceneManager.LoadScene(MainMenu);
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
