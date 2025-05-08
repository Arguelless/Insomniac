using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    private static MusicManager instancia;

    void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject); // Si ya hay uno, elimina este
            return;
        }

        instancia = this;
        DontDestroyOnLoad(gameObject);

        AudioSource audio = GetComponent<AudioSource>();
        audio.loop = true;
        audio.playOnAwake = true;

        if (!audio.isPlaying)
        {
            audio.Play();
        }
    }
}
