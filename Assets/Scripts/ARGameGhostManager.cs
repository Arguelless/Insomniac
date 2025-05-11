using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ARGhostGameManager : MonoBehaviour
{
    public GameObject panelInicio;
    public GameObject panelFin;
    public TMP_Text textoFin;
    public TMP_Text textoCuentaAtras;

    public ARGhostSpawner spawner;

    void Start()
    {
        panelInicio.SetActive(true);
        panelFin.SetActive(false);
        spawner.enabled = false; // No empieza hasta que pulsemos OK
    }

    public void BotonIniciarJuego()
    {
        panelInicio.SetActive(false);
        StartCoroutine(CuentaAtras());
    }

    IEnumerator CuentaAtras()
    {
        textoCuentaAtras.gameObject.SetActive(true);
        int segundos = 3;

        while (segundos > 0)
        {
            textoCuentaAtras.text = segundos.ToString();
            yield return new WaitForSeconds(1f);
            segundos--;
        }

        textoCuentaAtras.text = "0";
        yield return new WaitForSeconds(1f);
        textoCuentaAtras.gameObject.SetActive(false);

        spawner.enabled = true;
        spawner.IniciarJuego();
    }

    public void MostrarPanelFin(int puntuacionFinal)
    {
        panelFin.SetActive(true);
        textoFin.text = "Juego terminado\nPuntos: " + puntuacionFinal;

        // Parar la música
        FindFirstObjectByType<MusicManager>()?.PararMusica();
    }
}

