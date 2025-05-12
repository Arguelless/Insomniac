using UnityEngine;
using System.Collections;

public class TirarIA : TirarChapa
{
    protected override void Start()
    {
        base.Start(); // Asegura que 'rb' y otros componentes se inicialicen
    }

    public void JugarTurno(Vector2 posicionPelota, Vector2 posicionPorteria)
    {
        StartCoroutine(LanzarIA(posicionPelota, posicionPorteria));
    }

    IEnumerator LanzarIA(Vector2 posicionPelota, Vector2 posicionPorteria)
    {
        yield return new WaitForSeconds(1f);

        // Dirección desde la ficha hacia la pelota
        Vector2 direccion = (posicionPelota - (Vector2)transform.position).normalized;

        // Distancia desde la ficha hasta la pelota
        float distanciaFichaPelota = Vector2.Distance(transform.position, posicionPelota);

        // Distancia desde la pelota hasta la portería
        float distanciaPelotaPorteria = Vector2.Distance(posicionPelota, posicionPorteria);

        // Calcular fuerza total: combinación de ambas distancias
        float fuerza = (distanciaFichaPelota + distanciaPelotaPorteria) * 1.2f; // Puedes ajustar el multiplicador

        rb.AddForce(direccion * fuerza, ForceMode2D.Impulse);

        yield return new WaitUntil(() => rb.linearVelocity.magnitude < 0.1f);

        FindFirstObjectByType<ControlTurnos>().SiguienteTurno();
    }
}