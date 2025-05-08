using UnityEngine;

public class PlataformaMovimiento : MonoBehaviour
{
    private float velocidad;
    private bool haParado = false;

    public void SetVelocidad(float v)
    {
        velocidad = v;
    }

    void Update()
    {
        // Solo se mueve si no ha parado
        if (!haParado)
        {
            transform.Translate(Vector3.right * velocidad * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            foreach (ContactPoint2D punto in collision.contacts)
            {
                // Solo se detiene si el jugador cae desde arriba
                if (punto.normal.y < -0.5f)
                {
                    haParado = true;
                    break;
                }
            }
        }
    }
}

