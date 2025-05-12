using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonEmpezarVR : MonoBehaviour, IInteractuable
{
    [SerializeField] string escenaVR = "Escena_VR";

    Vector3 posicionInicial;
    float profundidad = 0.1f; // cuánto se hunde

    void Start()
    {
        posicionInicial = transform.localPosition;
    }

    public void Mirando(float progreso)
    {
        Debug.Log("Mirando progreso: " + progreso.ToString("F2"));
        transform.localPosition = posicionInicial + Vector3.back * progreso * profundidad;
    }

    public void Accion()
    {
        Debug.Log("¡Botón activado!");
        SceneManager.LoadScene(escenaVR);
    }
}