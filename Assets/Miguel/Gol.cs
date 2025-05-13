using UnityEngine;
using TMPro;

public class Gol : MonoBehaviour
{
    public string tipoLinea;   // Para diferenciar la l�nea
    public int contador1;
    public int contador2;
    public GameObject[] fichas;  // Las fichas que se reiniciar�n
    private Vector3[] posicionesIniciales;  // Guardamos las posiciones iniciales de las fichas

    public TextMeshProUGUI golesLocal;  // Referencia al texto de goles local
    public TextMeshProUGUI golesVisitante;  // Referencia al texto de goles visitante

    void Start()
    {
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

    // Detectar colisiones con el trigger (l�nea)
    public void RegistrarGol(string tipoLinea)
    {
        
        Debug.Log("�La pelota ha cruzado la l�nea!");

        if (tipoLinea == "LineaGolLocal")
        {
            contador1++;  // Sumar un gol al contador 1 (Local)
            Debug.Log("Gol local! Contador: " + contador1);
        }
        else if (tipoLinea == "LineaGolVisitante")
        {
            contador2++;  // Sumar un gol al contador 2 (Visitante)
            Debug.Log("Gol visitante! Contador: " + contador2);
        }

        ActualizarTexto();
        ReiniciarPosicionChapas();
    }

    // M�todo para reiniciar las posiciones de las fichas
    private void ReiniciarPosicionChapas()
    {
        for (int i = 0; i < fichas.Length; i++)
        {
            fichas[i].transform.position = posicionesIniciales[i];

            Rigidbody2D rb = fichas[i].GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;  // Detener el movimiento
                rb.angularVelocity = 0f;     // Detener la rotaci�n
            }
        }
    }

    void ActualizarTexto()
    {
        golesLocal.text = contador1.ToString();
        golesVisitante.text = contador2.ToString();
    }
}
