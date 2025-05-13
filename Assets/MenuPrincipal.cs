using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public float timer = 60f;
    public GameObject mainMenu;
    private bool bucle = false;
    private int currentGameIndex = 0;
    private readonly string[] juegos = { "Juego2D_1", "Juego2D_2", "Juego2D_3", "JuegoAR", "JuegoVR" };

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject); // Mantener este objeto entre escenas
        SceneManager.sceneLoaded += OnSceneLoaded; // Detectar cuando se cargue una nueva escena
    }

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait; // Mantener la orientación en Portrait para el menú principal
    }

    // Método para iniciar el bucle
    public void IniciarBucle()
    {
        bucle = true;
        timer = 60f;
        currentGameIndex = 0;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadSceneAsync("Juego2D_1");
    }

    private void Update()
    {
        if (bucle)
        {
            timer -= Time.deltaTime;
            Debug.Log($"Juego actual: {currentGameIndex}, T.restante: {timer}");

            if (timer <= 0)
            {
                currentGameIndex++;

                if (currentGameIndex < juegos.Length)
                {
                    timer = 60f;
                    CargarJuegoActual();
                }
                else
                {
                    bucle = false;
                    SceneManager.LoadScene("Hub");
                }
            }
        }
    }

    public void CargarJuegoActual()
    {
        if (currentGameIndex < juegos.Length)
        {
            string nombreJuego = juegos[currentGameIndex];
            SceneManager.LoadScene(nombreJuego);
            Debug.Log($"Cargando juego: {nombreJuego}");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Escena cargada: {scene.name}");
        // La lógica de inicialización de VR se ha movido a VRInitializer
    }

    // Cargar el juego 2D_1
    public void CargarJuego2D_1()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadSceneAsync("Juego2D_1");
    }

    // Cargar el segundo juego 2D
    public void CargarJuego2D_2()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft; // Cambiar orientación antes de cargar
        SceneManager.LoadSceneAsync("Juego2D_2");
    }

    // Cargar el tercer juego 2D
    public void CargarJuego2D_3()
    {
        SceneManager.LoadSceneAsync("Juego2D_3");
    }

    // Cargar el juego AR
    public void CargarJuegoAR()
    {
        SceneManager.LoadSceneAsync("JuegoAR");
    }

    // Iniciar el cambio a la escena VR
    public void IniciarCambioAEscenaVR()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadSceneAsync("JuegoVR");
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
