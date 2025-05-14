using UnityEngine;
using UnityEngine.InputSystem;

public class ToqueFantasma : MonoBehaviour
{
    public int puntosPorFantasma = 50;
    private Camera mainCamera;
    private PlayerInput playerInput;

    void Awake()
    {
        mainCamera = Camera.main;
        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            Debug.LogError("PlayerInput component not found on this GameObject!");
            enabled = false;
        }
    }

    // Nueva función para manejar el evento "TouchPress"
    public void OnTouchPress(InputAction.CallbackContext context)
    {
        if (context.performed) // Verificar si la acción se realizó (se presionó el botón/táctil)
        {
            Vector2 inputPosition = Vector2.zero;

#if UNITY_EDITOR
            inputPosition = Mouse.current.position.ReadValue();
#else
            if (Touchscreen.current != null && Touchscreen.current.touches.Count > 0)
            {
                inputPosition = Touchscreen.current.touches[0].position.ReadValue();
            }
            else if (Mouse.current.leftButton.isPressed)
            {
                inputPosition = Mouse.current.position.ReadValue();
            }
#endif

            Ray ray = mainCamera.ScreenPointToRay(inputPosition);
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.yellow, 0.1f);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("El rayo golpeó algo: " + hit.collider.gameObject.name + ", tag: " + hit.collider.tag);
                if (hit.collider.CompareTag("Fantasma"))
                {
                    Debug.Log("¡Tocado un Fantasma!");
                    ARGhostSpawner spawner = FindFirstObjectByType<ARGhostSpawner>();
                    if (spawner != null)
                    {
                        spawner.FantasmaAtrapado(hit.collider.gameObject);
                    }
                    else
                    {
                        Debug.LogError("No se encontró ARGhostSpawner.");
                    }
                    Destroy(hit.collider.gameObject);
                }
                else
                {
                    Debug.Log("El objeto golpeado no era un Fantasma.");
                }
            }
            else
            {
                Debug.Log("El rayo no golpeó nada.");
            }
        }
    }

    void Update()
    {
        // Ya no necesitamos la lógica de Input aquí si estamos usando eventos.
    }
}

