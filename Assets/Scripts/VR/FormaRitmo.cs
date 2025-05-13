using UnityEngine;

public class FormaRitmo : MonoBehaviour
{
    public Transform destino;
    public float velocidad = 1f;
    public float distanciaMinima = 0.1f; // distancia para considerar que llegó

    private bool haSidoGolpeada = false;

    void Update()
    {
        if (destino != null)
        {
            Vector3 direccion = (destino.position - transform.position).normalized;
            transform.position += direccion * velocidad * Time.deltaTime;

            // Si llega al destino y no fue golpeada, destruir y registrar fallo
            if (!haSidoGolpeada && Vector3.Distance(transform.position, destino.position) < distanciaMinima)
            {
                FindObjectOfType<PuntuacionVRManager>().RegistrarFallo();
                Destroy(gameObject);
            }
        }
    }

    // Este método se llamará desde NotaInteractuable
    public void MarcarComoGolpeada()
    {
        haSidoGolpeada = true;
    }
}
