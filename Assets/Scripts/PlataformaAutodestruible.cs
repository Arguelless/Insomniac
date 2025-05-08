using UnityEngine;
using System.Collections;

public class PlataformaAutodestruible : MonoBehaviour
{
    private bool cuentaAtrasActiva = false;
    private bool juegoActivo = false;
    private GameManager gameManager;

    public void Activar()
    {
        Debug.Log("Plataforma inicial activada.");
        juegoActivo = true;
    }

    //para finalizar que desaparezcan tras finalizar el juego
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!juegoActivo || gameManager == null || gameManager.JuegoFinalizado()) return;

        if (collision.collider.CompareTag("Player"))
        {
            foreach (ContactPoint2D punto in collision.contacts)
            {
                if (punto.normal.y < -0.5f && !cuentaAtrasActiva)
                {
                    cuentaAtrasActiva = true;
                    StartCoroutine(DestruirEnTiempo(6f));
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
