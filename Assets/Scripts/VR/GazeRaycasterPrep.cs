using UnityEngine;

public class GazeRaycasterPrep : MonoBehaviour
{
    public Transform reticulaVisual;            
    public float distanciaReticula = 2f;         
    public float escalaNormal = 0.01f;
    public float escalaActiva = 0.02f;
    public float tiempoNecesario = 1f;           // Tiempo para activar la interaccion
    private float tiempoMirando = 0f;

    private IInteractuable objetoActual = null;

    void Update()
    {
        // Coloca la retícula delante de la cámara
        Ray ray = new Ray(transform.position, transform.forward);
        reticulaVisual.position = ray.origin + ray.direction * distanciaReticula;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var interactuable = hit.collider.GetComponent<IInteractuable>();

            if (interactuable != null)
            {
                if (interactuable == objetoActual)
                {
                    tiempoMirando += Time.deltaTime;
                    float progreso = tiempoMirando / tiempoNecesario;
                    progreso = Mathf.Clamp01(progreso);

                    // Llama a la animación de mirada
                    interactuable.Mirando(progreso);

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