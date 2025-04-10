using UnityEngine;

public class BasketController : MonoBehaviour
{
    public float screenPadding = 0.5f; // Margen desde los bordes de la pantalla

    private float minX, maxX;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        // Calcular los límites visibles por la cámara
        float basketWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        Vector3 screenLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 screenRight = cam.ViewportToWorldPoint(new Vector3(1, 0, 0));

        minX = screenLeft.x + basketWidth + screenPadding;
        maxX = screenRight.x - basketWidth - screenPadding;
    }

    void Update()
    {
        Vector3 inputPosition = Vector3.zero;
        bool hasInput = false;

        // Entrada táctil (para móvil)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            inputPosition = touch.position;
            hasInput = true;
        }
        // Entrada con ratón (para editor o PC)
        else if (Input.GetMouseButton(0))
        {
            inputPosition = Input.mousePosition;
            hasInput = true;
        }

        if (hasInput)
        {
            Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(inputPosition.x, inputPosition.y, 0));
            float clampedX = Mathf.Clamp(worldPos.x, minX, maxX);
            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        }
    }
}
