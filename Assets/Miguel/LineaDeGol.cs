using UnityEngine;

public class LineaDeGol : MonoBehaviour
{
    public string tipoDeLinea; // "LineaGolLocal" o "LineaGolVisitante"
    public Gol gestorDeGol; // Referencia al GameObject con el script Gol.cs

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pelota")) // Asegúrate de que tu pelota tenga el tag "Pelota"
        {
            if (gestorDeGol != null)
            {
                gestorDeGol.RegistrarGol(tipoDeLinea);
            }
            else
            {
                Debug.LogError("El gestor de Gol no está asignado en " + gameObject.name);
            }
        }
    }
}