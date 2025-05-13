using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public float timer = 10f;
    public GameObject mainMenu;
    private bool bucle = false;
    private int currentGameIndex = 0;
    private readonly string[] juegos = { "Juego2D_1", "Juego2D_2", "Juego2D_3", "JuegoAR", "PreparacionVR2D", "PreparacionVR", "JuegoVR", "Puntuacion" };
    private bool juegoTerminadoManualmente = false;

    private VRInitializer vrInitializer; // Cacheamos la referencia al VRInitializer

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject); // Mantener este objeto entre escenas
        SceneManager.sceneLoaded += OnSceneLoaded; // Detectar cuando se cargue una nueva escena
        vrInitializer = FindFirstObjectByType<VRInitializer>(); // Buscar VRInitializer al inicio
        if (vrInitializer == null)
        {
            Debug.LogError("No se encontró el script VRInitializer en la inicialización de MenuPrincipal.");
        }
        else
        {
            Debug.Log("VRInitializer encontrado en Awake: " + vrInitializer.gameObject.name);
        }
    }

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait; // Mantener la orientación en Portrait para el menú principal
        Debug.Log("MenuPrincipal Start - bucle: " + bucle + ", timer: " + timer);
    }

    // Método para iniciar el bucle
    public void IniciarBucle()
    {
        bucle = true;
        timer = 10f;
        currentGameIndex = 0;
        juegoTerminadoManualmente = false; // Asegurarse de que esté en falso al iniciar el bucle
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadSceneAsync("Juego2D_1");
        Debug.Log("IniciarBucle - bucle: " + bucle + ", timer: " + timer);
    }

    private void Update()
    {
        if (bucle)
        {
            timer -= Time.deltaTime;
            Debug.Log("Update (bucle activo) - Juego: " + currentGameIndex + ", T. restante: " + timer + ", Escena: " + SceneManager.GetActiveScene().name);

            if (timer <= 0 && !juegoTerminadoManualmente)
            {
                SiguienteJuego();
            }
        }
        else
        {
            Debug.Log("Update (bucle inactivo) - timer: " + timer + ", Escena: " + SceneManager.GetActiveScene().name);
        }
    }

    private void SiguienteJuego()
    {
        // Detener VR si el juego actual es JuegoVR y el siguiente es Puntuacion
        if (juegos[currentGameIndex] == "JuegoVR" && currentGameIndex + 1 < juegos.Length && juegos[currentGameIndex + 1] == "Puntuacion")
        {
            Debug.Log("SiguienteJuego - Deteniendo VR antes de Puntuacion");
            if (vrInitializer != null)
            {
                Debug.Log("Llamando StopVR desde SiguienteJuego");
                vrInitializer.StopVR();
            }
            else
            {
                Debug.LogError("VRInitializer no encontrado al detener VR.");
            }
        }

        currentGameIndex++;
        Debug.Log("SiguienteJuego - currentGameIndex: " + currentGameIndex);

        if (currentGameIndex < juegos.Length)
        {
            timer = 10f; // Reiniciar el temporizador al cargar el siguiente juego automáticamente
            CargarJuegoActual();
        }
        else
        {
            bucle = false;
            SceneManager.LoadScene("Hub");
        }
        juegoTerminadoManualmente = false; // Resetear la bandera para el siguiente juego
        Debug.Log("SiguienteJuego (fin) - bucle: " + bucle + ", timer: " + timer);
    }

    public void CargarJuegoActual()
    {
        if (currentGameIndex < juegos.Length)
        {
            string nombreJuego = juegos[currentGameIndex];
            AjustarPantalla(nombreJuego);

            if (nombreJuego == "PreparacionVR")
            {
                Debug.Log("Cargando escena PreparacionVR: " + nombreJuego + " - Llamando a VRInitializer para iniciar VR");
                if (vrInitializer != null)
                {
                    Debug.Log("Llamando StartVR desde CargarJuegoActual");
                    vrInitializer.StartVR();
                    SceneManager.LoadScene(nombreJuego);
                }
                else
                {
                    Debug.LogError("VRInitializer no encontrado al iniciar VR.");
                    SceneManager.LoadScene(nombreJuego);
                }
            }
            else
            {
                Debug.Log("Cargando juego: " + nombreJuego);
                SceneManager.LoadScene(nombreJuego);
            }

            if (bucle)
            {
                timer = 10f;
            }
            juegoTerminadoManualmente = false;
            Debug.Log("CargarJuegoActual (fin) - bucle: " + bucle + ", timer: " + timer + ", juegoTerminadoManualmente: " + juegoTerminadoManualmente + ", Próximo juego: " + (currentGameIndex < juegos.Length - 1 ? juegos[currentGameIndex + 1] : "Fin del bucle"));
        }
    }

    public void JuegoTerminado()
    {
        juegoTerminadoManualmente = true;
        Debug.Log("JuegoTerminado - juegoTerminadoManualmente: " + juegoTerminadoManualmente + ", Escena: " + SceneManager.GetActiveScene().name);
    }

    public void AjustarPantalla(string nombreJuego)
    {
        if (nombreJuego == "Juego2D_1" || nombreJuego == "Juego2D_2" || nombreJuego == "JuegoVR" || nombreJuego == "PreparacionVR" || nombreJuego == "PreparacionVR2D")
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        else if (nombreJuego == "Juego2D_3" || nombreJuego == "JuegoAR" || nombreJuego == "Puntuacion")
            Screen.orientation = ScreenOrientation.Portrait;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Escena cargada: " + scene.name + " - bucle: " + bucle + ", timer: " + timer);
        // La lógica de inicialización de VR se ha movido a VRInitializer
    }

    // Métodos para cargar juegos individuales (sin bucle)
    public void CargarJuego2D_1() { bucle = false; timer = 60f; Screen.orientation = ScreenOrientation.LandscapeLeft; SceneManager.LoadSceneAsync("Juego2D_1"); }
    public void CargarJuego2D_2() { bucle = false; timer = 60f; Screen.orientation = ScreenOrientation.LandscapeLeft; SceneManager.LoadSceneAsync("Juego2D_2"); }
    public void CargarJuego2D_3() { bucle = false; timer = 60f; Screen.orientation = ScreenOrientation.Portrait; SceneManager.LoadSceneAsync("Juego2D_3"); }
    public void CargarJuegoAR() { bucle = false; timer = 60f; Screen.orientation = ScreenOrientation.Portrait; SceneManager.LoadSceneAsync("JuegoAR"); }
    public void IniciarCambioAEscenaVR() { bucle = false; timer = 60f; Screen.orientation = ScreenOrientation.LandscapeLeft; SceneManager.LoadSceneAsync("JuegoVR"); }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
