using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;
using System.Collections;

public class SaltarJuego : MonoBehaviour
{
    public string JuegoVR;
    public string MainMenu;

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

    public void SalirAlMenu()
    {
        string juegoActual = SceneManager.GetActiveScene().name; // Obtiene el nombre de la escena actual
        SceneManager.UnloadSceneAsync(juegoActual);
        SceneManager.LoadScene(MainMenu);
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
