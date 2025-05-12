using UnityEngine;

public class ControlGol : MonoBehaviour
{
    public string tipoLinea;
    public Gol lineaGolLocal;
    public Gol lineaGolVisitante;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pelota"))
        {
            if (tipoLinea == "LineaGolLocal")
            {
                // Llamar al script Gol en la l�nea local
                lineaGolLocal.RegistrarGol("LineaGolLocal");
            }
            else if (tipoLinea == "LineaGolVisitante")
            {
                // Llamar al script Gol en la l�nea visitante
                lineaGolVisitante.RegistrarGol("LineaGolVisitante");
            }
        }
    }
}
