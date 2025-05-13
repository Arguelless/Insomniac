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
            Destroy(instancia.gameObject); // Primero destruye la instancia anterior
        }

        instancia = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();

        SceneManager.sceneLoaded -= OnSceneLoaded; 
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
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
            if (audioSource != null)
                audioSource.Stop();

            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(gameObject);
        }
        else if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void PararMusica()
    {
        if (audioSource != null && audioSource.isPlaying)
            audioSource.Stop();
    }
}
