using UnityEngine;

public class FormaRitmo : MonoBehaviour
{
    public float velocidad = 200f;
    RectTransform rt;

    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    void Update()
    {
        rt.anchoredPosition += Vector2.down * velocidad * Time.deltaTime;
    }
}
