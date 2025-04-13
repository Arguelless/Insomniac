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
        SceneManager.LoadScene(Juego2D_1);
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    public void CargarJuego2D_1()
    {
        SceneManager.LoadScene(Juego2D_1);
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    public void CargarJuego2D_2()
    {
        SceneManager.LoadScene(Juego2D_2);
        Screen.orientation = ScreenOrientation.LandscapeLeft;
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
        StartCoroutine(CargarEscenaConXR());
    }

    IEnumerator CargarEscenaConXR()
    {
        Debug.Log("Iniciando XR...");
        yield return StartCoroutine(IniciarXR());

        if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
        {
            Debug.Log("XR iniciado. Cargando escena VR...");
            SceneManager.LoadScene(JuegoVR);
        }
        else
        {
            Debug.LogError("Fallo al iniciar XR");
        }
    }

    IEnumerator IniciarXR()
    {
        XRGeneralSettings.Instance.Manager.InitializeLoader();
        while (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            yield return null;
        }
        XRGeneralSettings.Instance.Manager.StartSubsystems();
    }

}
