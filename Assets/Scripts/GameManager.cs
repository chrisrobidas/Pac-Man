using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject readyText;
    [SerializeField] private GameObject winMapBlue;
    [SerializeField] private GameObject winMapWhite;
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject ghosts;
    
    private MusicManager musicManager;
    private ScoreManager scoreManager;

    private int pelletLeftCount;
    
    private void Awake()
    {
        musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        pelletLeftCount = GameObject.Find("Pellets").transform.childCount;
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        Time.timeScale = 0.0000001f;
        readyText.SetActive(true);
        musicManager.PlayIntroductionMusic();
        yield return new WaitForSeconds(4.5f * Time.timeScale);
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
        
        yield return new WaitForSeconds(Time.timeScale);
        map.GetComponent<SpriteRenderer>().enabled = false;
        ghosts.SetActive(false);

        for (int i = 0; i < 3; i++)
        {
            winMapWhite.SetActive(true);
            winMapBlue.SetActive(false);
            yield return new WaitForSeconds(0.4f * Time.timeScale);
            winMapWhite.SetActive(false);
            winMapBlue.SetActive(true);
            yield return new WaitForSeconds(0.4f * Time.timeScale);
        }
        
        PlayerPrefs.SetInt(PlayerPrefsData.APPLICATION_CLOSED_PROPERLY, 1);
        SceneManager.LoadScene("GameScene");
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
