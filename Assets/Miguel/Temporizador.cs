using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Temporizador : MonoBehaviour
{
    public float tiempoLimite = 60f; // 1 minuto
    private float tiempoRestante;
    private bool juegoPausado = false;

    public TextMeshProUGUI textoTemporizador; // Aseg�rate de asignar un Text en la UI para mostrar el temporizador

    void Start()
    {
        tiempoRestante = tiempoLimite;
    }

    void Update()
    {
        // Solo actualizamos el temporizador si el juego no est� pausado
        if (!juegoPausado)
        {
            tiempoRestante -= Time.deltaTime;

            // Aseg�rate de que el temporizador no pase de 0
            if (tiempoRestante < 0)
            {
                tiempoRestante = 0;
            }

            // Actualizamos la UI con el tiempo restante
            if (textoTemporizador != null)
            {
                textoTemporizador.text = Mathf.FloorToInt(tiempoRestante).ToString();
            }
        }
    }

    // M�todo para pausar el temporizador
    public void PausarTemporizador()
    {
        juegoPausado = true;
    }

    // M�todo para reanudar el temporizador
    public void ReanudarTemporizador()
    {
        juegoPausado = false;
    }

    // M�todo para reiniciar el temporizador
    public void ReiniciarTemporizador()
    {
        tiempoRestante = tiempoLimite;
    }
}
