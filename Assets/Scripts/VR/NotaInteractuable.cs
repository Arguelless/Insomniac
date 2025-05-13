using UnityEngine;

public class NotaInteractuable : MonoBehaviour, IInteractuable
{
    [Header("Efectos")]
    public GameObject efectoGolpe; // Asigna un prefab visual (partícula, luz, etc.)

    private Material materialOriginal;
    private Color colorOriginal;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
        {
            materialOriginal = rend.material;
            colorOriginal = materialOriginal.color;
        }
    }

    public void Mirando(float progreso)
    {
        // Visual feedback de progreso (cambiar color, brillo, etc.)
        if (materialOriginal != null)
        {
            Color nuevoColor = Color.Lerp(colorOriginal, Color.green, progreso);
            materialOriginal.color = nuevoColor;
        }
    }

    public void Accion()
    {
        // Marcar como golpeada para que no cuente como fallo al llegar al destino
        GetComponent<FormaRitmo>()?.MarcarComoGolpeada();

        FindObjectOfType<PuntuacionVRManager>().RegistrarGolpe();

        if (efectoGolpe != null)
        {
            GameObject efecto = Instantiate(efectoGolpe, transform.position, Quaternion.identity);
            Destroy(efecto, 2f);
        }

        Destroy(gameObject);
    }

}
