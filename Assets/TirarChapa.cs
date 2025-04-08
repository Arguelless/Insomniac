using UnityEngine;
using UnityEngine.InputSystem;

public class TirarChapa : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isDragging = false;
    private Vector2 startPos;
    private Vector2 dragStartPos;
    private LineRenderer lineRenderer;

    private Touchscreen touchscreen;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();

        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.enabled = false;
        }

        // Inicializamos el Touchscreen solo si el sistema de entrada está disponible
        if (Touchscreen.current != null)
        {
            touchscreen = Touchscreen.current;
        }
        else
        {
            Debug.LogError("El sistema de entrada 'Touchscreen' no está disponible. Asegúrate de tener el 'Input System' habilitado.");
        }
    }

    void Update()
    {
        // Asegúrate de que el touchscreen no sea null
        if (touchscreen == null)
        {
            Debug.LogError("El touchscreen no está inicializado correctamente.");
            return;  // Salimos del método si no está inicializado
        }

        // Verificamos si el primer toque está presionado
        if (touchscreen.primaryTouch.press.isPressed)
        {
            Vector2 worldTouchPos = Camera.main.ScreenToWorldPoint(touchscreen.primaryTouch.position.ReadValue());

            if (!isDragging)
            {
                RaycastHit2D hit = Physics2D.Raycast(worldTouchPos, Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    isDragging = true;
                    startPos = worldTouchPos;  // Guardamos la posición inicial del toque
                    dragStartPos = transform.position;  // Guardamos la posición inicial del objeto (estática)
                    rb.linearVelocity = Vector2.zero;  // Detenemos cualquier movimiento previo
                    rb.gravityScale = 0;  // Desactivamos la gravedad durante el arrastre

                    if (lineRenderer != null)
                        lineRenderer.enabled = true;  // Activamos el LineRenderer para la visualización
                }
            }

            if (isDragging)
            {
                if (lineRenderer != null)
                {
                    Vector2 dragDirection = (startPos - worldTouchPos).normalized;
                    float previewLength = 2f;

                    lineRenderer.SetPosition(0, dragStartPos);  // Usamos la posición original del objeto
                    lineRenderer.SetPosition(1, (Vector2)dragStartPos + dragDirection * previewLength);  // Muestra la dirección de arrastre
                }
            }
        }

        // Detectamos cuando el dedo se levanta y lanzamos el objeto
        if (touchscreen.primaryTouch.press.wasReleasedThisFrame)  // Detecta cuando el toque se levanta
        {
            Vector2 endPos = touchscreen.primaryTouch.position.ReadValue();
            Vector2 direction = (startPos - endPos).normalized;  // Dirección del lanzamiento
            float force = Vector2.Distance(startPos, endPos) * 10f;  // La fuerza se basa en la distancia entre el toque inicial y final

            rb.gravityScale = 1;  // Reactivamos la gravedad
            rb.AddForce(direction * force, ForceMode2D.Impulse);  // Lanzamos el objeto con la fuerza calculada

            isDragging = false;  // Desactivamos el modo de arrastre

            if (lineRenderer != null)
                lineRenderer.enabled = false;  // Desactivamos la línea cuando el toque se levanta
        }

        // Restringir al círculo a la pantalla en caso de que se salga de los límites
        RestrictToScreen();
    }

    // Método para restringir el objeto dentro de la pantalla
    void RestrictToScreen()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        // Restringir a los límites de la pantalla (en píxeles)
        screenPos.x = Mathf.Clamp(screenPos.x, 0, Screen.width);
        screenPos.y = Mathf.Clamp(screenPos.y, 0, Screen.height);

        // Convertir las coordenadas de pantalla de nuevo a mundo
        transform.position = Camera.main.ScreenToWorldPoint(screenPos);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);  // Mantener el valor z igual
    }
}