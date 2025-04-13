using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Temporizador : MonoBehaviour
{
    public float tiempoLimite = 60f; // 1 minuto
    private float tiempoRestante;
    private bool juegoPausado = false;
    public Gol scriptGol;
    public TextMeshProUGUI textoTemporizador;
    public GameObject panelFinal;
    public TextMeshProUGUI textoFinal;

    void Start()
    {
        tiempoRestante = tiempoLimite;
        tiempoRestante -= Time.deltaTime;
        textoFinal.gameObject.SetActive(false);
        panelFinal.SetActive(false);
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
                FinalTemporizador();
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

    public void FinalTemporizador()
    {
        bool isActive = panelFinal.activeSelf;
        panelFinal.SetActive(!isActive);
        panelFinal.gameObject.SetActive(true);

        if (tiempoRestante == 0)
        {
            if (scriptGol.contador1 == scriptGol.contador2)
            {
                textoFinal.text = "¡Has empatado la partida!";
            }
            else if(scriptGol.contador1 < scriptGol.contador2)
            {
                textoFinal.text = "¡Has perdido la partida!";
            }
            else if(scriptGol.contador1 > scriptGol.contador2) 
            {
                textoFinal.text = "¡Enhorabuena, has ganado la partida!";
            }
        }
    }
}
