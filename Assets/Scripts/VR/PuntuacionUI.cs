using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PuntuacionUI : MonoBehaviour
{
    public TextMeshProUGUI textoPuntuacion;
    public PuntuacionVRManager puntuacionVRManager;
    public TextMeshProUGUI textoMultiplicador;
    public TextMeshProUGUI textoSubidaMultiplicador;
    private int multiplicadorPrevio = 1;
    private float tiempoVisual = 0f;


    void Start()
    {
        puntuacionVRManager = FindObjectOfType<PuntuacionVRManager>();
    }

    void Update()
    {

        textoPuntuacion.text = "Puntos: " + puntuacionVRManager.puntuacionVR.ToString();
        textoMultiplicador.text = "Multiplicador x" + puntuacionVRManager.multiplicador;
        if (puntuacionVRManager.multiplicador > multiplicadorPrevio)
        {
            textoSubidaMultiplicador.text = "x" + puntuacionVRManager.multiplicador.ToString() + "!";
            textoSubidaMultiplicador.gameObject.SetActive(true);
            textoSubidaMultiplicador.alpha = 1f;
            textoSubidaMultiplicador.fontSize = 60;
            tiempoVisual = 1f; // dura 1 segundo
            multiplicadorPrevio = puntuacionVRManager.multiplicador;
        }

        // Desaparecer gradualmente
        if (tiempoVisual > 0)
        {
            tiempoVisual -= Time.deltaTime;
            textoSubidaMultiplicador.alpha = Mathf.Clamp01(tiempoVisual);
            textoSubidaMultiplicador.fontSize = Mathf.Lerp(60, 30, 1f - tiempoVisual);
        }
        else
        {
            textoSubidaMultiplicador.gameObject.SetActive(false);
        }
    }
}
