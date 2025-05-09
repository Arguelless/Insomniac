using UnityEngine;
using System.Collections;

public class TirarIA : TirarChapa
{
    protected override void Start()
    {
        base.Start(); // Asegura que 'rb' y otros componentes se inicialicen
    }

    public void JugarTurno(Vector2 objetivo)
    {
        StartCoroutine(LanzarIA(objetivo));
    }

    IEnumerator LanzarIA(Vector2 objetivo)
    {
        yield return new WaitForSeconds(1f);

        Vector2 direccion = (objetivo - (Vector2)transform.position).normalized;
        float fuerza = 4.5f;

        rb.AddForce(direccion * fuerza, ForceMode2D.Impulse);

        yield return new WaitUntil(() => rb.linearVelocity.magnitude < 0.1f);

        FindFirstObjectByType<ControlTurnos>().SiguienteTurno();
    }
}