using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ARGhostGameManager : MonoBehaviour
{
    public GameObject panelInicio;
    public GameObject panelFin;
    public TMP_Text textoFin;
    public TMP_Text textoCuentaAtras;

    public ARGhostSpawner spawner;

    private bool enBucle = false;

    void Start()
    {
        // Comprobar si estamos en el modo bucle
        MenuPrincipal menuPrincipal = FindObjectOfType<MenuPrincipal>();
        if (menuPrincipal != null && menuPrincipal.bucle)
        {
            enBucle = true;
            // Si estamos en bucle, iniciar la cuenta atrás automáticamente
            IniciarCuentaAtrasAutomatico();
        }
        else
        {
            enBucle = false;
            // Si no estamos en bucle, mostrar el panel de inicio
            panelInicio.SetActive(true);
            panelFin.SetActive(false);
            spawner.enabled = false; // No empieza hasta que pulsemos OK
        }
    }

    public void BotonIniciarJuego()
    {
        IniciarCuentaAtrasAutomatico();
    }

    public void IniciarCuentaAtrasAutomatico()
    {
        if (panelInicio != null)
        {
            panelInicio.SetActive(false);
        }
        panelFin.SetActive(false);
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

        textoCuentaAtras.text = "¡GO!";
        yield return new WaitForSeconds(1f);
        textoCuentaAtras.gameObject.SetActive(false);

        spawner.enabled = true;
        spawner.IniciarJuego();
    }

    public void MostrarPanelFin(int puntuacionFinal)
    {
        panelFin.SetActive(true);
        textoFin.text = "Juego terminado\nPuntos: " + puntuacionFinal;

        if (PuntuacionManager.Instance != null)
        {
            PuntuacionManager.Instance.AsignarPuntos(3, puntuacionFinal);
        }
        else
        {
            Debug.LogError("La instancia de PuntuacionManager no está disponible.");
        }

        // Parar la música
        FindFirstObjectByType<MusicManager>()?.PararMusica();

        // Si estamos en bucle, podrías considerar iniciar el siguiente juego automáticamente aquí
        if (enBucle)
        {
            // Encontrar y llamar a la función para avanzar al siguiente juego en MenuPrincipal
            MenuPrincipal menuPrincipal = FindObjectOfType<MenuPrincipal>();
            if (menuPrincipal != null)
            {
                menuPrincipal.JuegoTerminado(); // Simula que el juego terminó (manualmente o por tiempo)
            }
            else
            {
                Debug.LogError("No se encontró MenuPrincipal para avanzar al siguiente juego en bucle.");
            }
        }
    }
}
