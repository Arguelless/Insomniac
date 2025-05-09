using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTurnos : MonoBehaviour
{
    public List<TirarChapa> fichasHumano;
    public List<TirarIA> fichasIA;
    public Rigidbody2D pelota;

    private enum Turno { Humano, IA }
    private Turno turnoActual;

    void Start()
    {
        turnoActual = Turno.Humano;
        ActivarFichaHumanoActual();
    }

    public void SiguienteTurno()
    {
        StartCoroutine(EsperarYPasarTurno());
    }

    IEnumerator EsperarYPasarTurno()
    {
        // Esperar hasta que todas las fichas se detengan
        yield return new WaitUntil(() => !AlgoEnMovimiento());

        if (turnoActual == Turno.Humano)
        {
            DesactivarTodasFichasHumano();
            turnoActual = Turno.IA;

            yield return new WaitForSeconds(0.5f);

            foreach (var fichaIA in fichasIA)
            {
                if (!fichaIA.EstaEnMovimiento())
                {
                    // Lanzamos hacia la pelota por ejemplo
                    fichaIA.JugarTurno(pelota.position);
                    break;
                }
            }
        }
        else // Turno de la IA ha terminado
        {
            // Volver a activar el turno para el humano
            turnoActual = Turno.Humano;

            // Activar la siguiente ficha que no haya sido lanzada aún
            foreach (var ficha in fichasHumano)
            {
                if (!ficha.EstaEnMovimiento()) // Si la ficha no está en movimiento
                {
                    ficha.ActivarTurno();  // Activa esa ficha
                    break;  // Solo activamos una ficha
                }
            }
        }
    }

    private void ActivarFichaHumanoActual()
    {
        foreach (var ficha in fichasHumano)
            ficha.ActivarTurno(); // Activamos todas las fichas humanas
    }

    private void DesactivarTodasFichasHumano()
    {
        foreach (var ficha in fichasHumano)
            ficha.DesactivarTurno();
    }

    private bool AlgoEnMovimiento()
    {
        foreach (var ficha in fichasHumano)
            if (ficha.EstaEnMovimiento())
                return true;

        foreach (var ficha in fichasIA)
            if (ficha.EstaEnMovimiento())
                return true;

        return pelota != null && pelota.linearVelocity.magnitude > 0.1f;
    }
}
