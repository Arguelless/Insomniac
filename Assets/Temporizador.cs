using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Temporizador : MonoBehaviour
{
    public float tiempoLimite = 60f; // 1 minuto
    private float tiempoRestante;
    private bool juegoPausado = false;

    public TextMeshProUGUI textoTemporizador; // Asegúrate de asignar un Text en la UI para mostrar el temporizador

    void Start()
    {
        tiempoRestante = tiempoLimite;
    }

    void Update()
    {
        // Solo actualizamos el temporizador si el juego no está pausado
        if (!juegoPausado)
        {
            tiempoRestante -= Time.deltaTime;

            // Asegúrate de que el temporizador no pase de 0
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

    // Método para pausar el temporizador
    public void PausarTemporizador()
    {
        juegoPausado = true;
    }

    // Método para reanudar el temporizador
    public void ReanudarTemporizador()
    {
        juegoPausado = false;
    }

    // Método para reiniciar el temporizador
    public void ReiniciarTemporizador()
    {
        tiempoRestante = tiempoLimite;
    }
}
