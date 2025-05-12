using UnityEngine;
using Google.XR.Cardboard;
using UnityEngine.XR.Management;

public class CardboardInputHandler : MonoBehaviour
{
    void Update()
    {
        // Solo continuar si XR está correctamente inicializado
        if (!XRGeneralSettings.Instance.Manager.isInitializationComplete)
            return;

        // Botón "Cerrar" (X)
        if (Api.IsCloseButtonPressed)
        {
            Debug.Log("Botón X presionado");
            Application.Quit(); // Salir de la app
        }

        // Botón de configuración (engranaje)
        if (Api.IsGearButtonPressed)
        {
            Debug.Log("Botón de configuración presionado");
            Api.ScanDeviceParams(); // Escanea un nuevo QR
        }

        // Solo se debe llamar si XR está inicializado
        Api.UpdateScreenParams();
    }
}