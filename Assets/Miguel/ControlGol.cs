using UnityEngine;

public class ControlGol : MonoBehaviour
{
    public string tipoLinea;
    public Gol gestorDeGol; // Referencia al GameObject que tiene el script Gol.cs

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pelota"))
        {
            if (gestorDeGol != null)
            {
                gestorDeGol.RegistrarGol(tipoLinea); // Pasamos el tipo de línea directamente
            }
            else
            {
                Debug.LogError("El gestor de Gol no está asignado en " + gameObject.name);
            }
        }
    }
}
