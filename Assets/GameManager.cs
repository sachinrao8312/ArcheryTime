using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Text arrowCountText;
    public Text HighScore;
    public Text currentScore;

    public int totalScore = 0;
    public int score = 0;
    public int arrowCount = 10;

    // To enable and disable gameObject during GameOver
    public GameObject gameOverScreen;
    public GameObject HitBoard;
    public GameObject arrow;


    // Key to store and retireve HighScore
    private string highScoreKey = "HighScore";

    void Start()
    {
        HitBoard = GameObject.FindGameObjectWithTag("Target");

        // Initialize the score text when the game starts
        UpdateUI();

        //Load the highestScore from PlayerPrefs 
        LoadHighestScore();
        HighScore.enabled = false;

    }

    void UpdateUI()
    {
        // Update the score displayed in the UI
        scoreText.text = "Score: " + totalScore.ToString();
        currentScore.text = "Current " +scoreText.text ;
        arrowCountText.text = "Arrows: " + arrowCount.ToString();
        HighScore.text = "High Score: " + GetHighestScore();

    }

    public void UpdateScore(int score)
    {
        // Update the score
        totalScore += score;

        if (totalScore > GetHighestScore())
        {
            //Save the highestScore
            SaveHighScore();
        }

        if (score == 5)
        {
            AddNewArrow(2);
        }
        if (score == 1)
        {
            AddNewArrow(1);
        }
        if (score == 3)
        {
            AddNewArrow(1);
        }
        CheckArrowCount(arrowCount);

        // Update the UI to reflect the new score
        UpdateUI();
    }

    int GetHighestScore()
    {
        return PlayerPrefs.GetInt(highScoreKey, 0);
    }

    void SaveHighScore()
    {
        //Setting key value pair
        PlayerPrefs.SetInt(highScoreKey, totalScore);
        PlayerPrefs.Save();
    }

    void LoadHighestScore()
    {
        score = GetHighestScore();
    }

    public void UseArrow()
    {
        --arrowCount;
        CheckArrowCount(arrowCount);
        UpdateUI();
    }

    public void AddNewArrow(int arrow)
    {
        arrowCount += arrow + 1 ;
        UpdateUI();
    }

    public void RestartNewGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void CheckArrowCount(int arrowCount)
    {
        if (arrowCount <= 0)
        {
            arrowCount = 0; // Ensure arrow count doesn't go negative
            gameOverScreen.SetActive(true); 
            HitBoard.SetActive(false); 
            arrow.SetActive(false); 
            scoreText.enabled = false;
            arrowCountText.enabled = false;
            HighScore.enabled = true;
            UpdateUI(); // Update UI to show 0 arrows

        }
    }
}
