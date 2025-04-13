using UnityEngine;

public class PlataformaSpawner : MonoBehaviour
{
    public GameObject[] plataformas; // Ahora acepta varios prefabs
    public float tiempoEntreSpawns = 2f;
    public float alturaInicial = 0f;
    public float incrementoAltura = 2f;

    private float tiempoSiguienteSpawn;
    private float alturaActual;

    void Start()
    {
        alturaActual = alturaInicial;
        tiempoSiguienteSpawn = Time.time + tiempoEntreSpawns;
    }

    void Update()
    {
        if (Time.time >= tiempoSiguienteSpawn)
        {
            SpawnPlataforma();
            tiempoSiguienteSpawn = Time.time + tiempoEntreSpawns;
        }
    }

    void SpawnPlataforma()
    {
        // Elegir prefab aleatorio
        int index = Random.Range(0, plataformas.Length);
        GameObject plataformaElegida = plataformas[index];

        // Elegir si sale por la izquierda o derecha
        float xPos = Random.value > 0.5f ? -6f : 6f;

        // Instanciar
        GameObject nueva = Instantiate(plataformaElegida, new Vector3(xPos, alturaActual, 0f), Quaternion.identity);

        // Ajustar dirección
        float direccion = xPos < 0 ? 1f : -1f;
        nueva.GetComponent<PlataformaMovimiento>().SetVelocidad(2f * direccion);

        // Subir altura para el siguiente spawn
        alturaActual += incrementoAltura;
    }
}

