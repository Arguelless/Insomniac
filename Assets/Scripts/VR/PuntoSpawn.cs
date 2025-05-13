using UnityEngine;

public class PuntoSpawn : MonoBehaviour
{
    public GameObject prefabForma;
    public Transform[] puntosCarril;  // 3 posiciones en el mundo (izq, centro, der)
    public Transform[] destinosCarril; //3 finales de carriles (izq, cen, dewr)
    public float intervalo = 0.06f;   // cada 2 beats a 129 BPM

    void Start()
    {
        InvokeRepeating(nameof(Spawnear), 1f, intervalo);
    }

    void Spawnear()
    {
        int carrilAleatorio = Random.Range(0, puntosCarril.Length);

        Transform puntoInicio = puntosCarril[carrilAleatorio];
        Transform puntoDestino = destinosCarril[carrilAleatorio];

        GameObject nuevaForma = Instantiate(prefabForma, puntoInicio.position, Quaternion.identity);
        FormaRitmo forma = nuevaForma.GetComponent<FormaRitmo>();
        forma.destino = puntoDestino;
    }
}
