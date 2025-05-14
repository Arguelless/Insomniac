using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Management;

public class ARInitializer : MonoBehaviour
{
    public ARSession arSession;
    public XROrigin xrOrigin;

    void Start()
    {
        StartAR();
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
        arSession.enabled = false;
        xrOrigin.enabled = false;
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        Debug.Log("AR desactivado.");
    }
}
