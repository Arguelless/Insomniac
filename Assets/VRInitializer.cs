using UnityEngine;
using UnityEngine.XR.Management; // Importa el sistema de gestión de XR (realidad extendida).
using UnityEngine.SceneManagement;

public class VRInitializer : MonoBehaviour
{
    private bool vrInitialized = false;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if ((scene.name == "JuegoVR" || scene.name == "PreparacionVR") && !vrInitialized)
        {
            StartVR();
        }
        else if (scene.name != "JuegoVR" && scene.name != "PreparacionVR" && vrInitialized)
        {
            StopVR(); // Opcional: detener VR al salir de las escenas VR
        }
    }

    // Método para iniciar la realidad virtual.
    public void StartVR()
    {
        var xrManager = XRGeneralSettings.Instance.Manager;

        // Inicializa el cargador de XR de forma sincrónica.
        xrManager.InitializeLoaderSync();

        // Verifica si no se pudo inicializar el cargador XR.
        if (xrManager.activeLoader == null)
        {
            Debug.LogError("No se pudo inicializar el cargador XR. Asegúrate de que el cargador esté habilitado correctamente en los ajustes de XR Plug-in Management.");
            return;
        }

        Debug.Log($"Cargador activo: {xrManager.activeLoader.name}"); // Mostrar el nombre del cargador activo.

        // Intenta iniciar los subsistemas XR.
        try
        {
            xrManager.StartSubsystems();
            Debug.Log("VR activado correctamente.");
            vrInitialized = true;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error al iniciar los subsistemas de XR: {ex.Message}");
        }
    }

    // Método para detener la realidad virtual.
    public void StopVR()
    {
        // Detiene los subsistemas de XR, deshabilitando el seguimiento y el renderizado.
        XRGeneralSettings.Instance.Manager.StopSubsystems();

        // Desinicializa el cargador de XR, liberando los recursos asociados.
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        Debug.Log("VR desactivado."); // Confirma en la consola que VR está desactivado.
        vrInitialized = false;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
