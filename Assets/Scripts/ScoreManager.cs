using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Sprite zeroSprite;
    [SerializeField] private Sprite oneSprite;
    [SerializeField] private Sprite twoSprite;
    [SerializeField] private Sprite threeSprite;
    [SerializeField] private Sprite fourSprite;
    [SerializeField] private Sprite fiveSprite;
    [SerializeField] private Sprite sixSprite;
    [SerializeField] private Sprite sevenSprite;
    [SerializeField] private Sprite eightSprite;
    [SerializeField] private Sprite nineSprite;

    [SerializeField] private SpriteRenderer[] currentScoreSprites;
    [SerializeField] private SpriteRenderer[] highScoreSprites;

    private int currentScore;
    private int highScore;

    private const int MAX_SCORE = 999999;

    public void Initialize()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsData.APPLICATION_CLOSED_PROPERLY) == 0)
        {
            currentScore = 0;
        }
        else
        {
            currentScore = PlayerPrefs.GetInt(PlayerPrefsData.CURRENT_SCORE);
        }
        
        highScore = PlayerPrefs.GetInt(PlayerPrefsData.HIGH_SCORE);
        
        UpdateCurrentScoreSprites();
        UpdateHighScoreSprites();
    }

    public void IncreaseScore(int amount)
    {
        currentScore += amount;

        if (currentScore > MAX_SCORE)
            currentScore = MAX_SCORE;

        UpdateCurrentScoreSprites();

        if (currentScore > highScore)
        {
            highScore = currentScore;
            UpdateHighScoreSprites();
        }
    }

    public void ResetCurrentScore()
    {
        PlayerPrefs.SetInt(PlayerPrefsData.CURRENT_SCORE, 0);
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt(PlayerPrefsData.CURRENT_SCORE, currentScore);
        PlayerPrefs.SetInt(PlayerPrefsData.HIGH_SCORE, highScore);
    }

    private void UpdateCurrentScoreSprites()
    {
        string currentScoreString = currentScore.ToString();

        int spriteIndex = currentScoreSprites.Length - 1;
        for (int i = currentScoreString.Length - 1; i >= 0; i--)
        {
            currentScoreSprites[spriteIndex].enabled = true;
            currentScoreSprites[spriteIndex].sprite = NumberToSprite(currentScoreString[i]);
            spriteIndex--;
        }
    }
    
    private void UpdateHighScoreSprites()
    {
        string highScoreString = highScore.ToString();

        int spriteIndex = highScoreSprites.Length - 1;
        for (int i = highScoreString.Length - 1; i >= 0; i--)
        {
            highScoreSprites[spriteIndex].enabled = true;
            highScoreSprites[spriteIndex].sprite = NumberToSprite(highScoreString[i]);
            spriteIndex--;
        }
    }

    private Sprite NumberToSprite(int number)
    {
        switch (number)
        {
            case '0':
                return zeroSprite;
            case '1':
                return oneSprite;
            case '2':
                return twoSprite;
            case '3':
                return threeSprite;
            case '4':
                return fourSprite;
            case '5':
                return fiveSprite;
            case '6':
                return sixSprite;
            case '7':
                return sevenSprite;
            case '8':
                return eightSprite;
            case '9':
                return nineSprite;
            default:
                return null;
        }
    }
}
