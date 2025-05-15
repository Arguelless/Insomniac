using UnityEngine;

public class PlataformaSpawner : MonoBehaviour
{
    public GameObject[] plataformas;
    public float tiempoEntreSpawns = 2f;
    public float alturaInicial = 0f;
    public float incrementoAltura = 2f;

    private float tiempoSiguienteSpawn;
    private float alturaActual;

    // control de inicio
    private bool juegoActivo = false;

    void Start()
    {
        alturaActual = alturaInicial;
        // No iniciamos el tiempo hasta que el juego empiece
    }

    void Update()
    {
        if (!juegoActivo) return;

        if (Time.time >= tiempoSiguienteSpawn)
        {
            SpawnPlataforma();
            tiempoSiguienteSpawn = Time.time + tiempoEntreSpawns;
        }
    }

    //este método llamará el GameManager cuando empiece el juego
    public void ActivarSpawner()
    {
        juegoActivo = true;
        tiempoSiguienteSpawn = Time.time + tiempoEntreSpawns;
    }

    void SpawnPlataforma() //randomizamos la aparición lateral de la plataforma
    {
        int index = Random.Range(0, plataformas.Length);
        GameObject plataformaElegida = plataformas[index];

        float xPos = Random.value > 0.5f ? -6f : 6f; //des de donde empieza a aparecer

        GameObject nueva = Instantiate(plataformaElegida, new Vector3(xPos, alturaActual, 0f), Quaternion.identity);

        float direccion = xPos < 0 ? 1f : -1f; // hacia donde se dirige (izq o derecha)
        nueva.GetComponent<PlataformaMovimiento>().SetVelocidad(2f * direccion);

        alturaActual += incrementoAltura; //incremento de altura de las plataformas verticalmente
    }

    public void DetenerSpawner() //al aprar el juego que ya no se generen más
    {
        juegoActivo = false;
    }
}
