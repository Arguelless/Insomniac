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
    public TextMeshProUGUI Puntuacion;

    void Start()
    {
        Debug.Log("Temporizador.cs: Start() llamado."); // <--------------------- DEBUG
        tiempoRestante = tiempoLimite;
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
        Puntuacion.gameObject.SetActive(true);
        textoFinal.gameObject.SetActive(true);

        if (tiempoRestante == 0)
        {
            int puntos = 0;

            if (scriptGol.contador1 == scriptGol.contador2)
            {
                textoFinal.text = "¡Has empatado la partida!";
                puntos = 500;
            }
            else if (scriptGol.contador1 < scriptGol.contador2)
            {
                textoFinal.text = "¡Has perdido la partida!";
                puntos = 0;
            }
            else if (scriptGol.contador1 > scriptGol.contador2)
            {
                textoFinal.text = "¡Enhorabuena, has ganado la partida!";
                puntos = 1000;
            }

            if (PuntuacionManager.Instance != null)
            {
                PuntuacionManager.Instance.AsignarPuntos(0, puntos);
            }
            else
            {
                Debug.LogError("La instancia de PuntuacionManager no está disponible.");
            }

            Puntuacion.text = puntos.ToString();

        }
    }
}
