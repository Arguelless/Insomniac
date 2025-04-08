using UnityEngine;
using UnityEngine.InputSystem;

public class Touch : MonoBehaviour
{
    public InputAction touchAction;

    void OnEnable()
    {
        touchAction.Enable();  // Habilita la acci�n
    }

    void OnDisable()
    {
        touchAction.Disable();  // Deshabilita la acci�n
    }

    void Update()
    {
        if (touchAction.triggered)
        {
            Vector2 touchPosition = touchAction.ReadValue<Vector2>();
            Debug.Log("Touch position: " + touchPosition);
            // Aqu� puedes poner el c�digo para arrastrar o lanzar el objeto
        }
    }
}
