using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject mainMenu;
    public bool bucle = false;
    public float timer = 15; // Valor predeterminado para el temporizador
    public int currentGameIndex = 0;
    private readonly string[] juegos = { "Juego2D_1", "Juego2D_2", "Juego2D_3", "JuegoAR", "PreparacionVR2D", "PreparacionVR", "JuegoVR", "FinVR", "Puntuacion" };
    private bool juegoTerminadoManualmente = false;

    private VRInitializer vrInitializer;
    private XRManager xrManagerInstance;

    public static MenuPrincipal instance;

    private void Awake()
    {
        // --- Implementación correcta del patrón Singleton ---
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        // --- Fin de la implementación del Singleton ---

        SceneManager.sceneLoaded += OnSceneLoaded;

        vrInitializer = FindFirstObjectByType<VRInitializer>();
        if (vrInitializer == null)
        {
            Debug.LogError("No se encontró el script VRInitializer en la inicialización de MenuPrincipal.");
        }
        else
        {
            Debug.Log("VRInitializer encontrado en Awake: " + vrInitializer.gameObject.name);
        }

        xrManagerInstance = FindObjectOfType<XRManager>();
        if (xrManagerInstance == null)
        {
            Debug.LogError("No se encontró la instancia de XRManager en MenuPrincipal.");
        }
        else
        {
            Debug.Log("XRManager encontrado en Awake: " + xrManagerInstance.gameObject.name);
        }
    }

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Time.timeScale = 1f;
        Debug.Log("MenuPrincipal Start - bucle: " + bucle);
    }

    public void IniciarBucle()
    {
        Debug.Log("IniciarBucle llamado. Estado activo de MenuPrincipal antes de activar: " + gameObject.activeSelf);
        gameObject.SetActive(true);
        bucle = true;
        currentGameIndex = 0;
        juegoTerminadoManualmente = false;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadSceneAsync("Juego2D_1");
        Debug.Log("IniciarBucle - bucle: " + bucle);
        if (mainMenu != null)
        {
            mainMenu.SetActive(false);
            Debug.Log("UI del MenuPrincipal oculta.");
        }
    }

    private void Update()
    {
        // Solo disminuye el temporizador si el bucle está activo Y la escena actual requiere un temporizador
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (bucle && (currentSceneName == "PreparacionVR2D" || currentSceneName == "FinVR"))
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                // Reinicia el temporizador a un valor alto para evitar llamadas múltiples
                timer = 9999f;
                Debug.Log($"Temporizador agotado en {currentSceneName}. Pasando al siguiente juego.");
                SiguienteJuego();
            }
        }
    }

    public void SiguienteJuego()
    {
        juegoTerminadoManualmente = false;
        string currentSceneNameInSequence = juegos[currentGameIndex];

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
            CargarJuegoActual(); // CargarJuegoActual manejará la configuración del temporizador
            bucle = true;
        }
        else
        {
            bucle = false;
            SceneManager.LoadScene("MainMenu");
        }

        if (currentSceneNameInSequence == "PreparacionVR2D")
        {
            Debug.Log("Detectada escena PreparacionVR2D. Cargando directamente 'PreparacionVR'.");
            AjustarPantalla("PreparacionVR");
            SceneManager.LoadSceneAsync("PreparacionVR");
            bucle = true;
            return;
        }
        Debug.Log("SiguienteJuego (fin) - bucle: " + bucle);
    }

    public void CargarJuegoActual()
    {
        if (currentGameIndex < juegos.Length)
        {
            string nombreJuego = juegos[currentGameIndex];
            AjustarPantalla(nombreJuego);

            // Reinicia el temporizador solo si es una de las escenas específicas
            SetTimerForScene(nombreJuego);

            if (nombreJuego == "JuegoVR")
            {
                Debug.Log("Cargando escena JuegoVR: " + nombreJuego + " - Llamando a VRInitializer para iniciar VR");
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

            Debug.Log("CargarJuegoActual (fin) - bucle: " + bucle + ", Próximo juego: " + (currentGameIndex < juegos.Length - 1 ? juegos[currentGameIndex + 1] : "Fin del bucle"));
        }
    }

    public void JuegoTerminado()
    {
        if (bucle)
        {
            bucle = false; // El bucle se detiene por finalización manual del juego actual
            currentGameIndex++;
            Debug.Log("JuegoTerminado (manual) - bucle detenido, índice incrementado a: " + currentGameIndex);

            if (currentGameIndex < juegos.Length)
            {
                string siguienteJuego = juegos[currentGameIndex];
                Debug.Log("Cargando siguiente juego (manual): " + siguienteJuego);
                CargarEscena(siguienteJuego); // Llama a CargarEscena para la transición
            }
            else
            {
                Debug.Log("Fin del bucle alcanzado manualmente.");
                SceneManager.LoadScene("MainMenu");
                bucle = false;
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

        // Reinicia el temporizador solo si es una de las escenas específicas
        SetTimerForScene(nombreEscena);

        if (nombreEscena == "JuegoVR")
        {
            Debug.Log("Cargando escena JuegoVR - Llamando a VRInitializer para iniciar VR");
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
        Debug.Log("CargarEscena (fin) - bucle activado a: " + bucle + " escena cargada: " + nombreEscena);
    }

    // Función centralizada para establecer el temporizador según la escena
    private void SetTimerForScene(string sceneName)
    {
        if (sceneName == "PreparacionVR2D" || sceneName == "FinVR")
        {
            timer = 15f; // Estas escenas tienen un temporizador fijo de 15 segundos
            Debug.Log($"Temporizador establecido en 15s para {sceneName}.");
        }
        else
        {
            // Para el resto de las escenas, el temporizador NO está activo para el avance automático.
            // Lo ponemos en un valor que no active el if (timer <= 0) de Update
            // Puede ser un valor grande positivo o simplemente un valor que no haga que la condición se cumpla.
            timer = 15f; // Lo mantenemos en 15, pero Update no lo descontará para estas escenas
                         // porque ya no cumple la condición de nombre de escena.
            Debug.Log($"Temporizador **no** activo para avance automático en {sceneName}.");
        }
    }

    public void AjustarPantalla(string nombreJuego)
    {
        if (nombreJuego == "Juego2D_1" || nombreJuego == "Juego2D_2" || nombreJuego == "JuegoVR" || nombreJuego == "FinVR" || nombreJuego == "PreparacionVR2D")
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        else if (nombreJuego == "Juego2D_3" || nombreJuego == "JuegoAR" || nombreJuego == "Puntuacion")
            Screen.orientation = ScreenOrientation.Portrait;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Escena cargada: " + scene.name + " - bucle: " + bucle);

        // Al cargar una escena, si el bucle está activo, aseguramos la configuración del temporizador
        if (bucle)
        {
            SetTimerForScene(scene.name);
        }
        else if (scene.name == "MainMenu")
        {
            // Opcional: Asegúrate de que el timer esté en un estado neutro en el MainMenu
            timer = 15f;
        }

        if (scene.name.Contains("VR") && xrManagerInstance != null)
        {
            xrManagerInstance.SwitchXRProvider(scene.name);
        }
    }

    // Funciones de carga directa (no forman parte del bucle, así que el timer no debería forzar el avance)
    public void CargarJuego2D_1() {
        if (mainMenu != null)
        {
            mainMenu.SetActive(false);
            Debug.Log("UI del MenuPrincipal oculta.");
        }
        bucle = false; Screen.orientation = ScreenOrientation.LandscapeLeft; SceneManager.LoadSceneAsync("Juego2D_1"); SetTimerForScene("Juego2D_1"); }
    public void CargarJuego2D_2() { bucle = false; Screen.orientation = ScreenOrientation.LandscapeLeft; SceneManager.LoadSceneAsync("Juego2D_2"); SetTimerForScene("Juego2D_2"); }
    public void CargarJuego2D_3() { bucle = false; Screen.orientation = ScreenOrientation.Portrait; SceneManager.LoadSceneAsync("Juego2D_3"); SetTimerForScene("Juego2D_3"); }
    public void CargarJuegoAR() { bucle = false; Screen.orientation = ScreenOrientation.Portrait; SceneManager.LoadSceneAsync("JuegoAR"); SetTimerForScene("JuegoAR"); }
    public void IniciarCambioAEscenaVR()
    {
        bucle = false;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Debug.Log("Cargando escena PreparacionVR directamente - Llamando a VRInitializer para iniciar VR");
        if (vrInitializer != null)
        {
            Debug.Log("Llamando StartVR desde IniciarCambioAEscenaVR");
            vrInitializer.StartVR();
            SceneManager.LoadSceneAsync("PreparacionVR");
            SetTimerForScene("PreparacionVR"); // Aquí puede que quieras un timer si "PreparacionVR" es como PreparacionVR2D
        }
        else
        {
            Debug.LogError("VRInitializer no encontrado al iniciar VR desde IniciarCambioAEscenaVR.");
            SceneManager.LoadSceneAsync("PreparacionVR");
            SetTimerForScene("PreparacionVR");
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
