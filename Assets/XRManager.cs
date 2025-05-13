using UnityEngine;
using UnityEngine.XR.Management; // Importa la biblioteca de gestión de XR para habilitar/deshabilitar la realidad extendida.

public class XRManager : MonoBehaviour
{
    // Método general para habilitar o deshabilitar XR
    public void EnableXR(bool enable)
    {
        var xrManager = XRGeneralSettings.Instance.Manager; // Obtiene la configuración general de XR.

        if (enable) // Si se desea habilitar XR:
        {
            xrManager.InitializeLoaderSync(); // Inicializa el cargador XR de forma sincrónica.
            if (xrManager.activeLoader == null) // Verifica si no se pudo inicializar un cargador XR.
            {
                Debug.LogError("No XR loader found to enable XR."); // Muestra un error si no hay cargador disponible.
                return; // Finaliza el método.
            }
            xrManager.StartSubsystems(); // Inicia los subsistemas XR, como seguimiento y renderizado.
            Debug.Log("XR Enabled"); // Muestra un mensaje de confirmación.
        }
        else // Si se desea deshabilitar XR:
        {
            xrManager.StopSubsystems(); // Detiene los subsistemas XR.
            xrManager.DeinitializeLoader(); // Desinicializa el cargador XR y libera recursos.
            Debug.Log("XR Disabled"); // Muestra un mensaje de confirmación.
        }
    }

    // Método para habilitar o deshabilitar un proveedor XR específico
    public void EnableSpecificXR(string providerName, bool enable)
    {
        var xrManager = XRGeneralSettings.Instance.Manager; // Obtiene la configuración general de XR.

        if (enable) // Si se desea habilitar un proveedor específico:
        {
            xrManager.InitializeLoaderSync(); // Inicializa el cargador XR de forma sincrónica.

            // Comprueba si no hay cargador activo o si el cargador no coincide con el nombre del proveedor deseado.
            if (xrManager.activeLoader == null || xrManager.activeLoader.name != providerName)
            {
                Debug.LogError($"The XR provider '{providerName}' is not available."); // Muestra un error si el proveedor no está disponible.
                return; // Finaliza el método.
            }

            xrManager.StartSubsystems(); // Inicia los subsistemas XR.
            Debug.Log($"{providerName} Enabled"); // Muestra un mensaje indicando que el proveedor específico está activo.
        }
        else // Si se desea deshabilitar un proveedor específico:
        {
            // Comprueba si el cargador activo coincide con el proveedor deseado.
            if (xrManager.activeLoader != null && xrManager.activeLoader.name == providerName)
            {
                xrManager.StopSubsystems(); // Detiene los subsistemas XR.
                xrManager.DeinitializeLoader(); // Desinicializa el cargador XR.
                Debug.Log($"{providerName} Disabled"); // Muestra un mensaje indicando que el proveedor se desactivó.
            }
        }
    }

    // Método para cambiar entre proveedores XR según el tipo de escena
    public void SwitchXRProvider(string sceneName)
    {
        string providerName = "";

        // Si la escena es VR, usa el cargador de VR (Cardboard)
        if (sceneName.Contains("VR"))
        {
            providerName = "Cardboard XR Plugin"; // Cargador de VR
            Debug.Log("Activating Cardboard XR for VR scene...");
        }
        // Si la escena es AR, usa el cargador de AR (Google ARCore)
        else if (sceneName.Contains("AR"))
        {
            providerName = "Google ARCore"; // Cargador de AR
            Debug.Log("Activating Google ARCore for AR scene...");
        }

        // Si se ha determinado el proveedor adecuado
        if (!string.IsNullOrEmpty(providerName))
        {
            SwitchXRProvider(providerName); // Llama al método para cambiar al proveedor adecuado
        }
        else
        {
            Debug.LogError("Unknown XR provider for the given scene.");
        }
    }

    // Método que se ejecuta al iniciar el script
    void Start()
    {
        // Este es solo un ejemplo, puede ser llamado dependiendo de la escena
        SwitchXRProvider("JuegoVR"); // Cambia a Cardboard XR para la escena VR
        // O bien:
        // SwitchXRProvider("JuegoAR"); // Cambia a Google ARCore para la escena AR
    }

    // Método para desactivar XR manualmente (opcional)
    public void DeactivateXR()
    {
        var xrManager = XRGeneralSettings.Instance.Manager; // Obtiene la configuración general de XR.

        if (xrManager.activeLoader != null) // Verifica si hay un cargador XR activo.
        {
            xrManager.StopSubsystems(); // Detiene los subsistemas XR.
            xrManager.DeinitializeLoader(); // Desinicializa el cargador XR.
            Debug.Log("XR Disabled"); // Muestra un mensaje indicando que XR se desactivó.
        }
    }
}
