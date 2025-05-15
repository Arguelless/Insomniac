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
            Debug.LogError("El GameObject del bot�n Reiniciar no est� asignado en el Inspector de MenuFinal.");
        }

        // Actualizar la visibilidad inicial del bot�n
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
            botonReiniciar.SetActive(!menuPrincipal.bucle); // Mostrar el bot�n si bucle es falso
        }
        else if (botonReiniciar != null)
        {
            // Si no se encuentra MenuPrincipal, asumimos que no estamos en bucle y mostramos el bot�n
            botonReiniciar.SetActive(true);
        }
    }

    public void Reiniciar()
    {
        if (PanelFinal != null && PanelFinal.activeSelf)
        {
            PanelFinal.SetActive(false);
        }

        // Reiniciar el temporizador si tienes una referencia a �l
        if (temporizador != null)
        {
            temporizador.ReiniciarTemporizador(); // Asumiendo que tienes este m�todo
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
            menuPrincipal.bucle = false; // Asegurarse de que el bucle est� desactivado
            menuPrincipal.gameObject.SetActive(true); // Asegurarse de que est� activo en el men�
            Debug.Log("MenuPrincipal encontrado y activado al salir al men�.");
        }
        else
        {
            Debug.LogError("No se encontr� la instancia de MenuPrincipal al salir al men�.");
        }

        string juegoActual = SceneManager.GetActiveScene().name;
        SceneManager.UnloadSceneAsync(juegoActual);
        SceneManager.LoadScene(MainMenu);
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
