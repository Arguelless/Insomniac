using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Management;
using UnityEngine.SceneManagement;

public class ARInitializer : MonoBehaviour
{
    private ARSession arSession;
    private XROrigin xrOrigin;
    public string arSceneName = "JuegoAR"; // Nombre de tu escena AR

    void Start()
    {
        // Comprobar si estamos en la escena AR al inicio
        if (SceneManager.GetActiveScene().name == arSceneName)
        {
            FindAndStartAR();
        }
        SceneManager.sceneLoaded += OnSceneLoaded; // Suscribirse al evento de carga de escena
    }
    void FindAndStartAR()
    {
        arSession = FindObjectOfType<ARSession>();
        xrOrigin = FindObjectOfType<XROrigin>();
        if (arSession != null && xrOrigin != null)
        {
            StartAR();
        }
        else
        {
            Debug.LogError("No se encontraron ARSession o XROrigin en la escena " + arSceneName);
        }
    }

    public void StartAR()
    {
        XRGeneralSettings.Instance.Manager.InitializeLoaderSync();

        if (XRGeneralSettings.Instance.Manager.activeLoader == null ||
            !(XRGeneralSettings.Instance.Manager.activeLoader is XRLoader))
        {
            Debug.LogError("No se pudo iniciar AR.");
            return;
        }

        XRGeneralSettings.Instance.Manager.StartSubsystems();
        arSession.enabled = true;
        xrOrigin.enabled = true;
        Debug.Log("AR activado correctamente.");
    }

    public void StopAR()
    {
        if (arSession != null) arSession.enabled = false;
        if (xrOrigin != null) xrOrigin.enabled = false;
        if (XRGeneralSettings.Instance.Manager != null && XRGeneralSettings.Instance.Manager.activeLoader != null)
        {
            XRGeneralSettings.Instance.Manager.StopSubsystems();
            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        }
        Debug.Log("AR desactivado.");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Activar AR si se carga la escena AR
        if (scene.name == arSceneName)
        {
            FindAndStartAR();
        }
        // Desactivar AR si se carga cualquier otra escena y AR está activo
        else if (scene.name != arSceneName && XRGeneralSettings.Instance.Manager != null && XRGeneralSettings.Instance.Manager.activeLoader != null)
        {
            StopAR();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Asegurarse de desuscribirse al destruir el objeto
    }
}
