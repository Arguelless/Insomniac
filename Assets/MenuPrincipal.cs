using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public float timer = 62f;
    public GameObject mainMenu;
    public bool bucle = false;
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
        timer = 62f;
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
            string nombreJuego = juegos[currentGameIndex];
            // Establecer el timer a 10 segundos si la próxima escena es PreparacionVR2D
            if (nombreJuego == "PreparacionVR2D")
            {
                timer = 10f;
            }
            else
            {
                timer = 62f; // Reiniciar el temporizador para otras escenas
            }
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

            // El timer ya se ajustó en SiguienteJuego antes de llamar a CargarJuegoActual
            Debug.Log("CargarJuegoActual (fin) - bucle: " + bucle + ", timer: " + timer + ", juegoTerminadoManualmente: " + juegoTerminadoManualmente + ", Próximo juego: " + (currentGameIndex < juegos.Length - 1 ? juegos[currentGameIndex + 1] : "Fin del bucle"));
        }
    }

    public void JuegoTerminado()
    {
        if (bucle) // Solo actuar si el bucle está activo
        {
            bucle = false; // Detener el bucle
            timer = 62f; // Resetear el timer
            juegoTerminadoManualmente = true; // Marcar que se avanzó manualmente
            currentGameIndex++; // Incrementar el índice del juego inmediatamente
            Debug.Log("JuegoTerminado (manual) - bucle detenido, índice incrementado a: " + currentGameIndex);

            if (currentGameIndex < juegos.Length)
            {
                string siguienteJuego = juegos[currentGameIndex];
                Debug.Log("Cargando siguiente juego (manual): " + siguienteJuego);
                CargarEscena(siguienteJuego); // Usamos una función separada para cargar la escena
            }
            else
            {
                Debug.Log("Fin del bucle alcanzado manualmente.");
                SceneManager.LoadScene("Hub");
            }
        }
        else
        {
            Debug.Log("JuegoTerminado llamado pero el bucle no está activo.");
        }
    }

    private void CargarEscena(string nombreEscena)
    {
        AjustarPantalla(nombreEscena);
        if (nombreEscena == "PreparacionVR")
        {
            Debug.Log("Cargando escena PreparacionVR - Llamando a VRInitializer para iniciar VR");
            if (vrInitializer != null)
            {
                Debug.Log("Llamando StartVR desde CargarEscena");
                vrInitializer.StartVR();
                SceneManager.LoadScene(nombreEscena);
            }
            else
            {
                Debug.LogError("VRInitializer no encontrado al iniciar VR.");
                SceneManager.LoadScene(nombreEscena);
            }
        }
        else
        {
            Debug.Log("Cargando escena: " + nombreEscena);
            SceneManager.LoadScene(nombreEscena);
        }
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
    public void IniciarCambioAEscenaVR()
    {
        bucle = false;
        timer = 60f;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Debug.Log("Cargando escena PreparacionVR directamente - Llamando a VRInitializer para iniciar VR");
        if (vrInitializer != null)
        {
            Debug.Log("Llamando StartVR desde IniciarCambioAEscenaVR");
            vrInitializer.StartVR();
            SceneManager.LoadSceneAsync("PreparacionVR");
        }
        else
        {
            Debug.LogError("VRInitializer no encontrado al iniciar VR desde IniciarCambioAEscenaVR.");
            SceneManager.LoadSceneAsync("PreparacionVR");
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
