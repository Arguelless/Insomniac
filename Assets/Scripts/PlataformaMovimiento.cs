using UnityEngine;
using System.Collections;

public class PlataformaMovimiento : MonoBehaviour
{
    private float velocidad;
    private bool haParado = false;
    private bool cuentaAtrasActiva = false;

    private GameManager gameManager;

    public void SetVelocidad(float v)
    {
        velocidad = v;
    }

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        // Detener el movimiento si el juego ha terminado
        if (gameManager != null && gameManager.JuegoFinalizado()) return;

        if (!haParado)
        {
            transform.Translate(Vector3.right * velocidad * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameManager != null && gameManager.JuegoFinalizado()) return;

        if (collision.collider.CompareTag("Player")) //en el momento que el player toque la plataforma esta va a parar
        {
            foreach (ContactPoint2D punto in collision.contacts)
            {
                if (punto.normal.y < -0.5f && !haParado)
                {
                    haParado = true; //confirmamos que hay toque y paramos

                    if (!cuentaAtrasActiva) //empieza cuenta atrás de autodestrucción
                    {
                        cuentaAtrasActiva = true;
                        StartCoroutine(DestruirEnTiempo(3f)); // podemos ajustar el tiempo si queremos cambiar dificultad
                    }
                    break;
                }
            }
        }
    }

    IEnumerator DestruirEnTiempo(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        Destroy(gameObject);
    }
}



