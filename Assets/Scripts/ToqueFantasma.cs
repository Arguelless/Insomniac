using UnityEngine;

public class ToqueFantasma : MonoBehaviour
{
    public int puntosPorFantasma = 50;

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Fantasma"))
                {
                    ARGhostSpawner spawner = FindFirstObjectByType<ARGhostSpawner>();
                    if (spawner != null)
                    {
                        spawner.FantasmaAtrapado(hit.collider.gameObject);
                    }

                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}

