using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;
using System.Collections;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject menuSeleccionJuego;
    public string Juego2D_1;
    public string Juego2D_2;
    public string Juego2D_3;
    public string JuegoAR;
    public string JuegoVR;

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void MenuSeleccionJuego()
    {
        bool isActive = menuSeleccionJuego.activeSelf;
        menuSeleccionJuego.SetActive(!isActive);
    }

    public void IniciarBucle()
    {
        StartCoroutine(CargarEscena2D(Juego2D_1));
    }

    public void CargarJuego2D_1()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadScene(Juego2D_1);
    }

    IEnumerator CargarEscena2D(string escena)
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene(escena);
    }

    public void CargarJuego2D_2()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadScene(Juego2D_2);
    }

    public void CargarJuego2D_3()
    {
        SceneManager.LoadScene(Juego2D_3);
    }

    public void CargarJuegoAR()
    {
        SceneManager.LoadScene(JuegoAR);
    }

    public void IniciarCambioAEscenaVR()
    {
        SceneManager.LoadScene(JuegoVR);
    }
}
