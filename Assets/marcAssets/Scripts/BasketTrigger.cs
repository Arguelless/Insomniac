using UnityEngine;

public class BasketTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fruit"))
        {
            ScoreManager.Instance.AddPoint();
            Destroy(other.gameObject);

        }
        else if (other.CompareTag("Bomba"))
        {
            ScoreManager.Instance.ResetScore();
            Destroy(other.gameObject);
        }
    }
}
