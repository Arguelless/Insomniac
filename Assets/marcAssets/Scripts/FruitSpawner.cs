using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject[] fruitPrefabs;
    public float spawnInterval = 1.0f;
    public float xPadding = 0.5f;

    private float minX, maxX;
    private Camera cam;

    private bool isSpawning = false;

    void Start()
    {
        cam = Camera.main;

        Vector3 screenLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 screenRight = cam.ViewportToWorldPoint(new Vector3(1, 0, 0));
        minX = screenLeft.x + xPadding;
        maxX = screenRight.x - xPadding;
    }

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            InvokeRepeating(nameof(SpawnFruit), 0f, spawnInterval);
        }
    }

    public void StopSpawning()
    {
        if (isSpawning)
        {
            isSpawning = false;
            CancelInvoke(nameof(SpawnFruit));
        }
    }

    void SpawnFruit()
    {
        int index = Random.Range(0, fruitPrefabs.Length);
        GameObject fruit = Instantiate(fruitPrefabs[index]);

        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPos = cam.ViewportToWorldPoint(new Vector3(0.5f, 1.1f, 0));
        spawnPos.x = randomX;
        spawnPos.z = 0;

        fruit.transform.position = spawnPos;
    }
}
