using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header ("Opciones del juego")]
    [SerializeField]
    [Tooltip ("Cantidad de pickables que el jugador ha recogido")]
    public int playerScore;
    [SerializeField]
    [Tooltip("Cantidad de pickables que le quedan por recoger al jugador")]
    public int pickableNumber;
    [SerializeField]
    [Tooltip("Panel que se muestra cuando se pausa el juego")]
    public GameObject pausePanel;
    [SerializeField]
    [Tooltip("Panel que se muestra mientras se juega")]
    public GameObject resumePanel;
    [SerializeField]
    [Tooltip("Panel que se muestra cuando se acaba el juego tras recoger todos los pickables")]
    public GameObject victoryPanel;
    [SerializeField]
    [Tooltip("Jugador")]
    public GameObject player;
    [SerializeField]
    [Tooltip("Audio que se reproduce cuando se acaba el juego tras recoger todos los pickables")]
    public AudioSource victoryMusic;
    [SerializeField]
    [Tooltip("Audio de fondo durante el juego")]
    public AudioSource backgroundMusic;

    private bool musicHasPlayed = false;
    private bool gamePaused = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame();
        }
        checkConditionVictory();
    }

    //Code to pause the game
    public void pauseGame()
    {
        if (!gamePaused)
        {
            gamePaused = true;
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            resumePanel.SetActive(false);
            Cursor.visible = true;
        }
        else
        {
            gamePaused = false;
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            resumePanel.SetActive(true);
            Cursor.visible = false;
        }
    }

    //Code to win the game
    private void checkConditionVictory()
    {
        if (pickableNumber == 0)
        {
            Destroy(backgroundMusic);
            Time.timeScale = 0;
            victoryPanel.SetActive(true);
            resumePanel.SetActive(false);
            gamePaused = true;
            if (!victoryMusic.isPlaying && !musicHasPlayed)
            {
                victoryMusic.Play();
                musicHasPlayed = true;
            }
            
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
