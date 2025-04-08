using UnityEngine;
using UnityEngine.InputSystem;

public class Touch : MonoBehaviour
{
    public InputAction touchAction;

    void OnEnable()
    {
        touchAction.Enable();  // Habilita la acción
    }

    void OnDisable()
    {
        touchAction.Disable();  // Deshabilita la acción
    }

    void Update()
    {
        if (touchAction.triggered)
        {
            Vector2 touchPosition = touchAction.ReadValue<Vector2>();
            Debug.Log("Touch position: " + touchPosition);
            // Aquí puedes poner el código para arrastrar o lanzar el objeto
        }
    }
}
