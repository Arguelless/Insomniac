using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject[] fruitPrefabs; // Array de frutas para que se elijan aleatoriamente
    public float spawnInterval = 1.0f; // Tiempo entre cada fruta
    public float xPadding = 0.5f; // Margen para que no salga por los lados

    private float minX, maxX;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        // Calcular límites horizontales del mundo
        Vector3 screenLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 screenRight = cam.ViewportToWorldPoint(new Vector3(1, 0, 0));
        minX = screenLeft.x + xPadding;
        maxX = screenRight.x - xPadding;

        InvokeRepeating("SpawnFruit", 1f, spawnInterval); // Comienza a spawnear frutas
    }

    void SpawnFruit()
    {
        // Escoge fruta aleatoria
        int index = Random.Range(0, fruitPrefabs.Length);
        GameObject fruit = Instantiate(fruitPrefabs[index]);

        // Posición aleatoria horizontal en la parte superior de la pantalla
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPos = cam.ViewportToWorldPoint(new Vector3(0.5f, 1.1f, 0));
        spawnPos.x = randomX;
        spawnPos.z = 0; // Asegúrate de que no esté detrás de la cámara

        fruit.transform.position = spawnPos;
    }
}
