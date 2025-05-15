using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpForce = 10f;
    private int jumpCount = 0;
    public int maxJumps = 2; //maximo doble saltos

    public AudioClip sonidoSalto;
    public AudioClip sonidoPlataforma;
    public AudioClip sonidoCafe;

    public Sprite spriteIdle;
    public Sprite spriteSubiendo;
    public Sprite spriteCayendo;

    private SpriteRenderer spriteRenderer;

    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();  //añadimos sonido al salto y a la colisión
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Jump() //saltos solo verticales
    {
        if (jumpCount < maxJumps)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); // Reinicia velocidad vertical
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;

            if (sonidoSalto != null)
                audioSource.PlayOneShot(sonidoSalto); //sonido salto
        }
    }

    //Para cambiar animaciones del player
    void Update()
    {
        if (rb.linearVelocity.y > 0.1f) //animación cuando sube y cuando cae
        {
            spriteRenderer.sprite = spriteSubiendo;
        }
        else if (rb.linearVelocity.y < -0.1f)
        {
            spriteRenderer.sprite = spriteCayendo;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            jumpCount = 0; //al tocar la plataforma se reinician los saltos, tienes otros 2 para hacer

            spriteRenderer.sprite = spriteIdle; //animacion encima la plataforma

            if (sonidoPlataforma != null)
                audioSource.PlayOneShot(sonidoPlataforma); //sonido solision
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cafe"))
        {
            GameManager gm = FindFirstObjectByType<GameManager>();

            if (gm != null && gm.JuegoFinalizado()) return; // No hacer nada si el juego ya acabó

            if (sonidoCafe != null)
                audioSource.PlayOneShot(sonidoCafe); //sonido al caer en la raza

            gm.ElJugadorCayoEnLaTaza();

            StartCoroutine(DesaparecerTrasSonido());
        }
    }

    private IEnumerator DesaparecerTrasSonido()
    {
        yield return new WaitForSeconds(2f); // Ajusta si el sonido es más largo
        gameObject.SetActive(false);
    }
}

