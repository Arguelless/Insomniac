using UnityEngine;

public class NotaInteractuable : MonoBehaviour, IInteractuable
{
    [Header("Efectos visuales y sonoros")]
    public GameObject efectoGolpe;
    public AudioClip sonidoGolpe;

    private Material materialOriginal;
    private Color colorOriginal;
    private Renderer rend;
    private bool golpeado = false;

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
        if (materialOriginal != null)
        {
            Color nuevoColor = Color.Lerp(colorOriginal, Color.green, progreso);
            materialOriginal.color = nuevoColor;
        }
    }

    public void Accion()
    {
        if (golpeado) return;
        golpeado = true;

        GetComponent<FormaRitmo>()?.MarcarComoGolpeada();
        FindAnyObjectByType<PuntuacionVRManager>()?.RegistrarGolpe();

        if (efectoGolpe != null)
        {
            GameObject efecto = Instantiate(efectoGolpe, transform.position, Quaternion.identity);
            Destroy(efecto, 2f);
        }

        if (sonidoGolpe != null)
        {
            AudioSource.PlayClipAtPoint(sonidoGolpe, transform.position);
        }

        Destroy(gameObject);
    }
}
