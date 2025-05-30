using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerControllerm : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreTotalText;
    public ScoreManager scoreManager;
    public GameObject panelFinalJuego;
    public GameObject panelInicioJuego;
    public AudioSource audioSource;
    public float timeRemaining = 75f; // por ejemplo, 60 segundos
    private float playTime = 60f;
    public bool isTimerRunning = true;
    public FruitSpawner fruitSpawner;

    void Update()
    {
        if (isTimerRunning)
        {
            if (timeRemaining > 66f)
            {
                panelInicioJuego.SetActive(true);
                timeRemaining -= Time.deltaTime;
                audioSource.Play();
            }
            else if (timeRemaining > 65f)
            {
                timeRemaining -= Time.deltaTime;
                panelInicioJuego.SetActive(false);

            }
            else if (timeRemaining > 5)
            {
                playTime -= Time.deltaTime;
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay();
                fruitSpawner.StartSpawning();
            }
            else
            {
                timeRemaining = 0;
                isTimerRunning = false;
                fruitSpawner.StopSpawning();
                audioSource.Stop();
                UpdateTimerDisplay();
                panelFinalJuego.SetActive(true);
                int score = scoreManager.GetScore() * 20;
                if (score > 1000)
                {
                    score = 1000;
                }
                if (PuntuacionManager.Instance != null && MenuPrincipal.instance.bucle == true)
                {
                    PuntuacionManager.Instance.AsignarPuntos(1, score);
                }
                scoreTotalText.text = "Score Total: " + score.ToString();
                if (PuntuacionManager.Instance != null && MenuPrincipal.instance.bucle == true)
                {
                    PuntuacionManager.Instance.AsignarPuntos(1, score);
                }
            }
        }
    }

    void UpdateTimerDisplay()
    {
        int seconds = Mathf.FloorToInt(playTime % 60);
        timerText.text = seconds.ToString("00");
    }

    public void PauseTimer()
    {
        isTimerRunning = false;
        audioSource.Pause();
    }

    public void ResumeTimer()
    {
        isTimerRunning = true;
        audioSource.UnPause();
    }
}
