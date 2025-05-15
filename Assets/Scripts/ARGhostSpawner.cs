using UnityEngine;
using System.Collections;
using TMPro;

public class ARGhostSpawner : MonoBehaviour
{
    public GameObject[] fantasmaPrefabs;
    public int totalFantasmas = 20;  // 20 fantasma, a cada fantasma 50 puntos = maxima puntuación 1000 puntos
    public float distanciaMin = 1f;  // se proyectan entre 1 y 2 metros ***despues podemos variarlo
    public float distanciaMax = 2f;

    public TMP_Text textoPuntos;
    public AudioClip sonidoCaptura;
    private AudioSource audioSource;

    private GameObject fantasmaActual;
    private int fantasmasGenerados = 0;
    private int puntuacion = 0;
    private float tiempoUltimoSonido = -10f;
    public float intervaloSonido = 1.5f; //para que no se solapen los sonidos al capturar

    public void IniciarJuego()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(SpawnFantasmaPeriodicamente());
    }

    IEnumerator SpawnFantasmaPeriodicamente()  //randomizamos el tiempo de aparicion entre 1-3 segundos
    {
        while (fantasmasGenerados < totalFantasmas)
        {
            float espera = Random.Range(1f, 3f);
            yield return new WaitForSeconds(espera);

            if (fantasmaActual == null)
            {
                SpawnFantasma();
                fantasmasGenerados++;
            }
        }

        yield return new WaitForSeconds(2f);
        FindFirstObjectByType<ARGhostGameManager>()?.MostrarPanelFin(puntuacion); //mostramos panel final
    }

    void SpawnFantasma() //el fantasma aparece en zonas aleatorias siempre miranto al jugador
    {
        Vector3 dir = Camera.main.transform.forward;
        Vector3 pos = Camera.main.transform.position + dir * Random.Range(distanciaMin, distanciaMax);
        pos += Camera.main.transform.right * Random.Range(-0.5f, 0.5f);
        pos += Camera.main.transform.up * Random.Range(-0.2f, 0.5f);

        Quaternion rot = Quaternion.LookRotation(Camera.main.transform.position - pos);
        int index = Random.Range(0, fantasmaPrefabs.Length);
        fantasmaActual = Instantiate(fantasmaPrefabs[index], pos, rot);

        float vida = CalcularVidaFantasma(); //más adelante calcularemos el tiempo de aparición
        StartCoroutine(DestruirFantasmaDespuesDeTiempo(fantasmaActual, vida)); //vive unos segundos y desaparece
    }

    float CalcularVidaFantasma() //para disminuir el tiempo de aparicion de los fantasmas para incrementar la dificultad
    {
        if (fantasmasGenerados < 5) return 2.0f;
        else if (fantasmasGenerados < 10) return 1.5f;
        else if (fantasmasGenerados < 15) return 1.0f;
        else return 0.7f;
    }

    IEnumerator DestruirFantasmaDespuesDeTiempo(GameObject fantasma, float segundos) //tag fantasma para poder cazarlos
    {
        yield return new WaitForSeconds(segundos);

        if (fantasma != null)
        {
            Destroy(fantasma);
            if (fantasma == fantasmaActual)
            {
                fantasmaActual = null;
            }
        }
    }

    public void FantasmaAtrapado(GameObject fantasma) //cada fantasma atrapado son 50 puntos
    {
        if (fantasma == fantasmaActual)
        {
            puntuacion += 50;
            textoPuntos.text = "Puntos: " + puntuacion;

            if (sonidoCaptura != null && Time.time - tiempoUltimoSonido >= intervaloSonido)
            {
                audioSource.PlayOneShot(sonidoCaptura); //sonido al capturar
                tiempoUltimoSonido = Time.time;
            }

            fantasmaActual = null;
        }
    }
}

