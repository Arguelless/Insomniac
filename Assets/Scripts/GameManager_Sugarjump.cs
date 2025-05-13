using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float tiempoTotal = 60f;
    private float tiempoRestante;
    private bool juegoActivo = false;
    private bool juegoFinalizado = false;

    public TMP_Text textoTiempo;
    public GameObject panelFin;
    public TMP_Text textoFin;
    public PlataformaSpawner plataformaSpawner;
    public PlataformaAutodestruible plataformaInicial;
    public PlayerJump playerJump;
    public GameObject player;



    void Start()
    {
        tiempoRestante = tiempoTotal;
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        panelFin.SetActive(false);
        playerJump.enabled = false; // Desactivamos la l�gica de salto
    }


    void Update()
    {
        if (juegoActivo && !juegoFinalizado)
        {
            tiempoRestante -= Time.deltaTime;
            textoTiempo.text = Mathf.CeilToInt(tiempoRestante).ToString();

            if (tiempoRestante <= 0)
            {
                tiempoRestante = 0;
                FinalizarJuego(1000); // Gan� por sobrevivir
            }
        }
    }

    //vamos a probar porque la plataforma inicial no desaparecepublic void Activar()

    public void EmpezarJuego()
    {
        juegoActivo = true;

        plataformaSpawner.ActivarSpawner();

        if (plataformaInicial != null)
            plataformaInicial.Activar();

        playerJump.enabled = true;
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

    }



    public void ElJugadorCayoEnLaTaza()
    {
        if (!juegoFinalizado)
        {
            int puntuacion = Mathf.RoundToInt(((tiempoTotal - tiempoRestante) / tiempoTotal) * 1000);
            FinalizarJuego(puntuacion);
        }
    }

    private void FinalizarJuego(int puntuacion)
    {
        juegoFinalizado = true;
        juegoActivo = false;
        panelFin.SetActive(true);
        textoFin.text = "Juego terminado\nPuntuaci�n: " + puntuacion;
        plataformaSpawner.DetenerSpawner();

        if (puntuacion == 1000)
        {
            // Solo desactivamos si gan� por tiempo
            player.SetActive(false);
        }
        FindFirstObjectByType<MusicManager>()?.PararMusica();

    }

    public bool JuegoFinalizado()
    {
        return juegoFinalizado;
    }

}

