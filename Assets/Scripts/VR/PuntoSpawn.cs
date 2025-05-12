using UnityEngine;

public class SpawnerFormas : MonoBehaviour
{
    public GameObject prefabForma;
    public RectTransform[] puntosCarril;  // 3 puntos de spawn (izq, centro, der)
    public float intervalo = 0.06f;       // cada 2 beats a 129 BPM

    void Start()
    {
        InvokeRepeating("Spawnear", 1f, intervalo);
    }

    void Spawnear()
    {
        int carrilAleatorio = Random.Range(0, puntosCarril.Length);
        RectTransform puntoSeleccionado = puntosCarril[carrilAleatorio];

        GameObject nuevaForma = Instantiate(prefabForma, transform);
        RectTransform rtForma = nuevaForma.GetComponent<RectTransform>();
        rtForma.anchoredPosition = puntoSeleccionado.anchoredPosition;
    }
}
