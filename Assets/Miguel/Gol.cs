using UnityEngine;
using TMPro;

public class Gol : MonoBehaviour
{
    public int contador1;
    public int contador2;
    public GameObject[] fichas;    // Las fichas que se reiniciarán
    private Vector3[] posicionesIniciales;    // Guardamos las posiciones iniciales de las fichas

    public TextMeshProUGUI golesLocal;    // Referencia al texto de goles local
    public TextMeshProUGUI golesVisitante;    // Referencia al texto de goles visitante

    void Start()
    {
        Debug.Log("Gol.cs: Start() llamado."); // <--------------------- DEBUG
        contador1 = 0;
        contador2 = 0;
        ActualizarTexto();

        // Guardamos las posiciones iniciales de las fichas
        posicionesIniciales = new Vector3[fichas.Length];
        for (int i = 0; i < fichas.Length; i++)
        {
            posicionesIniciales[i] = fichas[i].transform.position;
        }
    }

    // Método llamado por el script de la línea de gol al detectar una colisión
    public void RegistrarGol(string tipoLinea)
    {
        Debug.Log("¡La pelota ha cruzado la línea: " + tipoLinea + "!");

        if (tipoLinea == "LineaGolLocal")
        {
            contador1++;    // Sumar un gol al contador 1 (Local)
            Debug.Log("Gol local! Contador: " + contador1);
        }
        else if (tipoLinea == "LineaGolVisitante")
        {
            contador2++;    // Sumar un gol al contador 2 (Visitante)
            Debug.Log("Gol visitante! Contador: " + contador2);
        }

        ActualizarTexto();
        ReiniciarPosicionChapas();
    }

    // Método para reiniciar las posiciones de las fichas
    private void ReiniciarPosicionChapas()
    {
        for (int i = 0; i < fichas.Length; i++)
        {
            fichas[i].transform.position = posicionesIniciales[i];

            Rigidbody2D rb = fichas[i].GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;    // Detener el movimiento
                rb.angularVelocity = 0f;        // Detener la rotación
            }
        }
    }

    void ActualizarTexto()
    {
        if (golesLocal != null)
        {
            golesLocal.text = contador1.ToString();
        }
        else
        {
            Debug.LogError("Referencia a golesLocal no asignada en Gol.cs");
        }

        if (golesVisitante != null)
        {
            golesVisitante.text = contador2.ToString();
        }
        else
        {
            Debug.LogError("Referencia a golesVisitante no asignada en Gol.cs");
        }
    }
}
