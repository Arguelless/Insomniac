using UnityEngine;
using TMPro;

public class PuntuacionManager : MonoBehaviour
{
    public static PuntuacionManager Instance;

    private int[] puntuaciones = new int[5]; // Asumiendo 5 juegos

    public TextMeshProUGUI Puntuacion;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre escenas
        }
        else
        {
            Destroy(gameObject); // Evita duplicados
        }
    }

    // Asignar puntos a un juego espec�fico (0 a 4)
    public void AsignarPuntos(int indiceJuego, int puntos)
    {
        if (indiceJuego >= 0 && indiceJuego < puntuaciones.Length)
        {
            puntuaciones[indiceJuego] = puntos;
            Debug.Log($"Juego {indiceJuego} -> {puntos} puntos asignados");
        }
        else
        {
            Debug.LogWarning("�ndice de juego inv�lido al asignar puntos.");
        }
    }

    // Obtener la puntuaci�n total
    public int ObtenerPuntuacionTotal()
    {
        int total = 0;
        foreach (int p in puntuaciones)
            total += p;

        return total;
    }

    public void ResetPuntuaciones()
    {
        for (int i = 0; i < puntuaciones.Length; i++)
        {
            puntuaciones[i] = 0;
        }
        Debug.Log("Puntuaciones reiniciadas.");
    }
}