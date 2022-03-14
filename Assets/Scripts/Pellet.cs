using UnityEngine;

public class Pellet : MonoBehaviour
{
    private GameManager gameManager;
    private MusicManager musicManager;
    private ScoreManager scoreManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.name == "Pac-Man")
        {
            Destroy(gameObject);
            musicManager.PlayWakaWakaSound();
            scoreManager.IncreaseScore(10);
            gameManager.DecreasePelletLeftCount();
        }
    }
}
