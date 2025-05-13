using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject menuSeleccionJuego;
    public string Juego2D_1;
    public string Juego2D_2;
    public string Juego2D_3;
    public string JuegoAR;
    public string PreparacionVR;

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait; // Mantener la orientacion en Portrait para el menu principal
    }

    public void MenuSeleccionJuego()
    {
        bool isActive = menuSeleccionJuego.activeSelf;
        menuSeleccionJuego.SetActive(!isActive);
    }

    // M�todo para iniciar el bucle
    public void IniciarBucle()
    {
        StartCoroutine(CargarEscena2D(Juego2D_1));
    }

    // Cargar el juego 2D_1
    public void CargarJuego2D_1()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft; // Cambiar orientacion antes de cargar la escena
        SceneManager.LoadScene(Juego2D_1);
    }

    // Coroutine para cargar la escena 2D_1 de manera as�ncrona
    IEnumerator CargarEscena2D(string escena)
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft; // Cambiar orientacion antes de cargar
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(escena); // Usar LoadSceneAsync para mejorar la carga

        // Esperar hasta que la escena se cargue completamente
        while (!asyncLoad.isDone)
        {
            // Puedes agregar una barra de carga o animacion aqui si lo deseas
            yield return null;
        }
    }

    // Cargar el segundo juego 2D
    public void CargarJuego2D_2()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft; // Cambiar orientaci�n antes de cargar
        SceneManager.LoadScene(Juego2D_2);
    }

    // Cargar el tercer juego 2D
    public void CargarJuego2D_3()
    {
        SceneManager.LoadScene(Juego2D_3);
    }

    // Cargar el juego AR
    public void CargarJuegoAR()
    {
        SceneManager.LoadScene(JuegoAR);
    }

    // Iniciar el cambio a la escena VR
    public void IniciarCambioAEscenaVR()
    {
        SceneManager.LoadScene(PreparacionVR);
    }
}
