using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ControlTurnos : MonoBehaviour
{
    public List<TirarChapa> fichasHumano;
    public List<TirarIA> fichasIA;
    public Rigidbody2D pelota;

    private enum Turno { Humano, IA }
    private Turno turnoActual;

    public Transform porteriaHumano;

    void Start()
    {
        Debug.Log("ControlTurnos.cs: Start() llamado."); // <--------------------- DEBUG
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

        // Restablecer fichas que cruzaron la línea de gol al final del turno
        ResetearFichasQueCruzaronGol();

        if (turnoActual == Turno.Humano)
        {
            DesactivarTodasFichasHumano();
            turnoActual = Turno.IA;

            yield return new WaitForSeconds(0.5f);

            Vector2 posicionPelota = pelota.position;
            Vector2 posicionPorteria = porteriaHumano.position;

            TirarIA mejorFicha = null;
            float mejorPuntaje = float.MinValue;

            foreach (var fichaIA in fichasIA)
            {
                if (!fichaIA.EstaEnMovimiento())
                {
                    Vector2 posFicha = fichaIA.transform.position;
                    Vector2 direccionFichaPelota = (posicionPelota - posFicha).normalized;
                    Vector2 direccionPelotaPorteria = (posicionPorteria - posicionPelota).normalized;

                    float alineacion = Vector2.Dot(direccionFichaPelota, direccionPelotaPorteria);
                    float distancia = Vector2.Distance(posFicha, posicionPelota);

                    // Puntaje = alineación (máximo 1) * 100 - distancia
                    float puntaje = alineacion * 100f - distancia;

                    // Verifica si hay algo entre la ficha y la pelota
                    RaycastHit2D hit = Physics2D.Raycast(posFicha, direccionFichaPelota, distancia);
                    if (hit.collider != null && hit.collider.attachedRigidbody != pelota)
                    {
                        puntaje -= 100f; // Penaliza obstáculos
                    }

                    if (puntaje > mejorPuntaje)
                    {
                        mejorPuntaje = puntaje;
                        mejorFicha = fichaIA;
                    }
                }
            }

            if (mejorFicha != null)
            {
                mejorFicha.JugarTurno(posicionPelota, posicionPorteria);
            }
        }
        else // Turno de la IA ha terminado
        {
            turnoActual = Turno.Humano;

            foreach (var ficha in fichasHumano)
            {
                ficha.ActivarTurno();
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

    // Nuevo método para resetear las fichas que han cruzado la línea de gol
    private void ResetearFichasQueCruzaronGol()
    {
        foreach (var ficha in fichasHumano.Concat(fichasIA))
        {
            ficha.ResetFicha(); // Llamamos al método para resetear la ficha si cruzó la línea de gol
        }
    }
}
