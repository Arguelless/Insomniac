using UnityEngine;

public class GazeRaycasterPrep : MonoBehaviour
{
    [Header("Retícula")]
    public Transform reticulaVisual;
    public float distanciaReticula = 2f;
    public float escalaNormal = 0.01f;
    public float escalaActiva = 0.02f;

    [Header("Interacción")]
    public float tiempoNecesario = 1f;  // Tiempo que debes mirar para activar
    private float tiempoMirando = 0f;

    private IInteractuable objetoActual = null;

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 10f, Color.red);

        // Raycast desde el centro de la cámara (este objeto)
        Ray ray = new Ray(transform.position, transform.forward);

        // Coloca la retícula delante del jugador
        reticulaVisual.position = ray.origin + ray.direction * distanciaReticula;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var interactuable = hit.collider.GetComponent<IInteractuable>();

            if (interactuable != null)
            {
                if (interactuable == objetoActual)
                {
                    // Aumenta el tiempo mirando al mismo objeto
                    tiempoMirando += Time.deltaTime;

                    float progreso = tiempoMirando / tiempoNecesario;
                    progreso = Mathf.Clamp01(progreso);

                    // Llama a la animación de carga visual
                    interactuable.Mirando(progreso);

                    // Escala visual de retícula
                    float escala = Mathf.Lerp(escalaNormal, escalaActiva, progreso);
                    reticulaVisual.localScale = Vector3.one * escala;

                    if (tiempoMirando >= tiempoNecesario)
                    {
                        interactuable.Accion();
                        tiempoMirando = 0f;
                    }
                }
                else
                {
                    // Cambió el objeto al que se mira
                    objetoActual = interactuable;
                    tiempoMirando = 0f;
                    reticulaVisual.localScale = Vector3.one * escalaNormal;
                }
            }
            else
            {
                Reiniciar();
            }
        }
        else
        {
            Reiniciar();
        }
    }

    void Reiniciar()
    {
        objetoActual = null;
        tiempoMirando = 0f;
        reticulaVisual.localScale = Vector3.one * escalaNormal;
    }
}
