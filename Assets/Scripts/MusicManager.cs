using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instancia;

    public string[] escenasConMusica; 
    private AudioSource audioSource;

    void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        instancia = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Si la escena actual no está en la lista de permitidas, paramos y destruimos
        bool deberiaSonar = false;

        foreach (string escena in escenasConMusica)
        {
            if (scene.name == escena)
            {
                deberiaSonar = true;
                break;
            }
        }

        if (!deberiaSonar)
        {
            Destroy(gameObject);
        }
    }

    public void PararMusica()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }
}
