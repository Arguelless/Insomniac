using UnityEngine;

public class PuntuacionVRManager : MonoBehaviour
{
    public int puntuacionVR = 0;
    public int multiplicador = 1;
    public int aciertosConsecutivos = 0;
    public int puntosPorForma = 1;

    public void RegistrarGolpe()
    {
        aciertosConsecutivos++;
        if (aciertosConsecutivos % 10 == 0)
        {
            multiplicador = Mathf.Max(1,(aciertosConsecutivos / 10)*10);
        }

        puntuacionVR += puntosPorForma * multiplicador;
        Debug.Log($"Puntos: {puntuacionVR} (x{multiplicador})");
    }

    public void RegistrarFallo()
    {
        aciertosConsecutivos = 0;
        multiplicador = 1;
        Debug.Log("¡Fallaste! Multiplicador reiniciado.");
    }
}
