using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject readyText;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private GameObject winMapBlue;
    [SerializeField] private GameObject winMapWhite;
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject ghosts;
    
    private MusicManager musicManager;
    private ScoreManager scoreManager;
    private LivesManager livesManager;
    private GameObject pacMan;

    private int pelletLeftCount;
    
    private void Awake()
    {
        musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        livesManager = GameObject.Find("LivesManager").GetComponent<LivesManager>();
        pacMan = GameObject.Find("Pac-Man");
        pelletLeftCount = GameObject.Find("Pellets").transform.childCount;
        
        scoreManager.Initialize();
        livesManager.Initialize();
        
        PlayerPrefs.SetInt(PlayerPrefsData.APPLICATION_CLOSED_PROPERLY, 0);
        
        StartCoroutine(StartGame());
    }
    
    private void OnApplicationQuit()
    {
        scoreManager.ResetCurrentScore();
        livesManager.ResetLivesLeft();
        PlayerPrefs.SetInt(PlayerPrefsData.APPLICATION_CLOSED_PROPERLY, 1);
    }

    private IEnumerator StartGame()
    {
        Time.timeScale = 0.0000001f;
        pacMan.GetComponent<Animator>().speed = 0;
        readyText.SetActive(true);
        musicManager.PlayIntroductionMusic();
        yield return new WaitForSeconds(4.5f * Time.timeScale);
        pacMan.GetComponent<Animator>().speed = 1;
        musicManager.PlayGhostSirenSound();
        readyText.SetActive(false);
        Time.timeScale = 1;
    }

    private IEnumerator WinGame()
    {
        Time.timeScale = 0.0000001f;
        
        musicManager.StopGhostSirenSound();
        musicManager.StopWakaWakaSound();
        scoreManager.SaveScore();
        
        pacMan.GetComponent<Animator>().speed = 0;
        
        yield return new WaitForSeconds(Time.timeScale);
        map.GetComponent<SpriteRenderer>().enabled = false;
        HideGhosts();

        // This will make blink 3 times the map
        for (int i = 0; i < 3; i++)
        {
            winMapWhite.SetActive(true);
            winMapBlue.SetActive(false);
            yield return new WaitForSeconds(0.4f * Time.timeScale);
            winMapWhite.SetActive(false);
            winMapBlue.SetActive(true);
            yield return new WaitForSeconds(0.4f * Time.timeScale);
        }

        ReloadScene();
    }

    private void ReloadScene()
    {
        PlayerPrefs.SetInt(PlayerPrefsData.APPLICATION_CLOSED_PROPERLY, 1);
        SceneManager.LoadScene("GameScene");
    }

    public IEnumerator ReplaceScene()
    {
        readyText.SetActive(true);
        ghosts.SetActive(true);
        
        pacMan.GetComponent<PacManMovement>().Reset();
        
        GameObject.Find("Blinky").GetComponent<GhostMovement>().Reset();
        GameObject.Find("Pinky").GetComponent<GhostMovement>().Reset();
        GameObject.Find("Inky").GetComponent<GhostMovement>().Reset();
        GameObject.Find("Clyde").GetComponent<GhostMovement>().Reset();
        
        // To replace explosion sprite by Pac-Man one
        pacMan.GetComponent<Animator>().speed = 1;
        yield return new WaitForSeconds(0.15f * Time.timeScale);
        pacMan.GetComponent<Animator>().speed = 0;
        
        livesManager.ShowLivesLeftSprites();
        
        yield return new WaitForSeconds(2f * Time.timeScale);
        pacMan.GetComponent<Animator>().speed = 1;
        musicManager.PlayGhostSirenSound();
        readyText.SetActive(false);
        Time.timeScale = 1;
    }

    public IEnumerator GameOver()
    {
        gameOverText.SetActive(true);
        scoreManager.ResetCurrentScore();
        scoreManager.SaveScore();
        yield return new WaitForSeconds(Time.timeScale);
        SceneManager.LoadScene("MainMenuScene");
    }

    public void HideGhosts()
    {
        ghosts.SetActive(false);
    }

    public void DecreasePelletLeftCount()
    {
        pelletLeftCount--;

        if (pelletLeftCount <= 0)
        {
            StartCoroutine(WinGame());
        }
    }
}
