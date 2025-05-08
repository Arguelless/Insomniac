using UnityEngine;
using UnityEngine.InputSystem;

public class BasketController : MonoBehaviour
{
    public float screenPadding = 0.5f;

    private float minX, maxX;
    private Camera cam;
    private PlayerInputActions inputActions;

    void Awake()
    {
        inputActions = new PlayerInputActions();
        cam = Camera.main;
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void Start()
    {
        float basketWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        Vector3 screenLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 screenRight = cam.ViewportToWorldPoint(new Vector3(1, 0, 0));

        minX = screenLeft.x + basketWidth + screenPadding;
        maxX = screenRight.x - basketWidth - screenPadding;
    }

    void Update()
    {
        Vector2 screenPos = inputActions.Gameplay.PointerPosition.ReadValue<Vector2>();

        Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, cam.nearClipPlane));
        float clampedX = Mathf.Clamp(worldPos.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
