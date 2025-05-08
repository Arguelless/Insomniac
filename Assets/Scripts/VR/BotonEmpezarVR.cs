using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonEmpezarVR : MonoBehaviour, IInteractuable
{
    [SerializeField] private string JuegoVR = "JuegoVR";
    public void Accion()
    {
        SceneManager.LoadScene(JuegoVR);
    }
}
