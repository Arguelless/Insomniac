using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Management;

public class ARInitializer : MonoBehaviour
{
    private ARSession arSession;
    private XROrigin xrOrigin;

    void Start() //al iniciar la escena buscamos los componentes AR y XR
    {
        arSession = FindObjectOfType<ARSession>();
        xrOrigin = FindObjectOfType<XROrigin>();

        if (arSession != null && xrOrigin != null)
        {
            StartCoroutine(IniciarAR()); //si  LOS ENCUENTRA NOS VAMOS A INICIAR AR
        }
        else
        {
            Debug.LogError("Faltan componentes ARSession o XROrigin.");
        }
    }

    private System.Collections.IEnumerator IniciarAR()
    {
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.LogError("No se pudo inicializar el loader AR.");
            yield break;
        }

        XRGeneralSettings.Instance.Manager.StartSubsystems(); //SE INICIAN LOS SUBsistemas de AR

        arSession.enabled = true;
        xrOrigin.enabled = true;

        Debug.Log("AR inicializado correctamente.");
    }
}

