using System.Collections;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] livesSprites;

    private int livesLeft;

    private GameManager gameManager;
    private MusicManager musicManager;
    private ScoreManager scoreManager;
    private GameObject pacMan;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        pacMan = GameObject.Find("Pac-Man");
    }
    
    public void Initialize()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsData.APPLICATION_CLOSED_PROPERLY) == 0)
        {
            livesLeft = 3;
        }
        else
        {
            livesLeft = PlayerPrefs.GetInt(PlayerPrefsData.LIVES_LEFT, 3);
        }
        
        ShowLivesLeftSprites();
    }

    public void ResetLivesLeft()
    {
        PlayerPrefs.SetInt(PlayerPrefsData.LIVES_LEFT, 3);
    }

    public void MakePacManDie()
    {
        StartCoroutine(MakePacManDieCoroutine());
    }

    private IEnumerator MakePacManDieCoroutine()
    {
        Time.timeScale = 0.0000001f;
        
        musicManager.StopGhostSirenSound();
        musicManager.StopWakaWakaSound();
        scoreManager.SaveScore();

        pacMan.GetComponent<Animator>().speed = 0;
            
        yield return new WaitForSeconds(2 * Time.timeScale);
        pacMan.GetComponent<Animator>().speed = 1;
        gameManager.HideGhosts();
        musicManager.PlayDeathSound();
        pacMan.GetComponent<Animator>().SetBool("IsDead", true);
        yield return new WaitForSeconds(1.75f * Time.timeScale);
        pacMan.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.5f * Time.timeScale);
        
        livesLeft--;
        PlayerPrefs.SetInt(PlayerPrefsData.LIVES_LEFT, livesLeft);

        if (livesLeft <= 0)
        {
            StartCoroutine(gameManager.GameOver());
        }
        else
        {
            StartCoroutine(gameManager.ReplaceScene());
        }
    }

    public void ShowLivesLeftSprites()
    {
        for (int i = 0; i < 3 - livesLeft; i++)
        {
            livesSprites[i].enabled = false;
        }
    }
}
