using UnityEngine;
using UnityEngine.InputSystem;

public class TirarChapa : MonoBehaviour
{
    protected Rigidbody2D rb;
    private bool isDragging = false;
    private Vector2 startPos;
    private Vector2 dragStartPos;
    private LineRenderer lineRenderer;
    private Touchscreen touchscreen;

    public ControlTurnos controlTurnos;
    private bool puedeLanzar = false;

    private static TirarChapa fichaActualmenteArrastrada = null;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();

        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.enabled = false;
        }

        if (Touchscreen.current != null)
        {
            touchscreen = Touchscreen.current;
        }
        else
        {
            Debug.LogError("El sistema de entrada 'Touchscreen' no está disponible.");
        }
    }

    void Update()
    {
        // Si no está permitido lanzar, no hay sistema de entrada, o ya se arrastra otra ficha, salir
        if (!puedeLanzar || touchscreen == null || (fichaActualmenteArrastrada != null && fichaActualmenteArrastrada != this))
            return;

        if (touchscreen.primaryTouch.press.isPressed)
        {
            Vector2 worldTouchPos = Camera.main.ScreenToWorldPoint(touchscreen.primaryTouch.position.ReadValue());

            if (!isDragging)
            {
                RaycastHit2D hit = Physics2D.Raycast(worldTouchPos, Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    isDragging = true;
                    fichaActualmenteArrastrada = this; // ← AQUI MARCAMOS QUE ESTA ES LA SELECCIONADA

                    startPos = worldTouchPos;
                    dragStartPos = transform.position;
                    rb.linearVelocity = Vector2.zero;
                    rb.gravityScale = 0;

                    if (lineRenderer != null)
                        lineRenderer.enabled = true;
                }
            }

            if (isDragging && lineRenderer != null)
            {
                Vector2 dragDirection = (startPos - worldTouchPos).normalized;
                float previewLength = 2f;

                lineRenderer.SetPosition(0, dragStartPos);
                lineRenderer.SetPosition(1, dragStartPos + dragDirection * previewLength);
            }
        }

        if (touchscreen.primaryTouch.press.wasReleasedThisFrame)
        {
            if (isDragging)
            {
                Vector2 endPos = Camera.main.ScreenToWorldPoint(touchscreen.primaryTouch.position.ReadValue());
                Vector2 direction = (startPos - endPos).normalized;
                float force = Vector2.Distance(startPos, endPos) * 5f;

                rb.gravityScale = 0;
                rb.AddForce(direction * force, ForceMode2D.Impulse);

                isDragging = false;
                puedeLanzar = false;
                fichaActualmenteArrastrada = null; // ← LIBERAMOS PARA QUE OTRA PUEDA USARSE

                if (lineRenderer != null)
                    lineRenderer.enabled = false;

                if (controlTurnos != null)
                {
                    foreach (var ficha in FindObjectsByType<TirarChapa>(FindObjectsSortMode.None))
                        ficha.DesactivarTurno();

                    controlTurnos.SiguienteTurno();
                }
            }
        }

        RestrictToScreen();
    }

    // Método para restringir el objeto dentro de la pantalla
    void RestrictToScreen()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        screenPos.x = Mathf.Clamp(screenPos.x, 0, Screen.width);
        screenPos.y = Mathf.Clamp(screenPos.y, 0, Screen.height);

        transform.position = Camera.main.ScreenToWorldPoint(screenPos);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    public void ActivarTurno()
    {
        puedeLanzar = true;
    }

    public void DesactivarTurno()
    {
        puedeLanzar = false;
    }

    public bool EstaEnMovimiento()
    {
        return rb.linearVelocity.magnitude > 0.1f;
    }
}