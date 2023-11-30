using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Text Shown in the UI
    public TMP_Text scoreText;
    public TMP_Text arrowCountText;
    public TMP_Text HighScore;
    public TMP_Text currentScore;
    public TMP_Text timerText;

    // Total Score while game and arrowCounts
    public int totalScore = 0;
    public int score = 0;
    public int noNewArrowfromSideScore;
    // Initial Arrow Count
    public int arrowCount = 7;

    // To enable and disable gameObject during GameOver
    public GameObject gameOverScreen;
    public GameObject HitBoard;
    public GameObject arrow;

    // Timer for the arrow
    public float arrowTimer = 10f;

    // Key to store and retrieve HighScore
    private string highScoreKey = "HighScore";

    void Start()
    {
        noNewArrowfromSideScore = Random.Range(40, 60);
        HitBoard = GameObject.FindGameObjectWithTag("Target");

        // Initialize the timer
        arrowTimer = 10f;

        // Initialize the score text when the game starts
        UpdateUI();

        // Load the highestScore from PlayerPrefs 
        LoadHighestScore();
    }

    void Update()
    {
        // Update the timer only if the game is not over
        if (!gameOverScreen.activeSelf)
        {
            arrowTimer -= Time.deltaTime;

            // Automatically shoot arrow when timer reaches zero
            if (arrowTimer < 0)
            {
                ArrowSpawner.Instance.AutoReleaseArrow();
                arrowTimer = 10f; // Reset the timer
            }

            UpdateUI();
        }
    }

    void UpdateUI()
    {
        // Update the score displayed in the UI
        scoreText.text = "Score: " + totalScore.ToString();
        currentScore.text = "Current " + scoreText.text;
        arrowCountText.text = "Arrows: " + arrowCount.ToString();
        HighScore.text = "High Score: " + GetHighestScore();
        // Round the timer value
        timerText.text = "Timer: " + Mathf.Round(arrowTimer).ToString();
    }

    public void UpdateScore(int score)
    {
        // Update the score
        totalScore += score;

        // Updates the HighScore If currentTotalScore is greater than HighScore Saved
        if (totalScore > GetHighestScore())
        {
            // Save the highestScore
            SaveHighScore();
        }

        // New Arrows Based On Score
        if (totalScore <= noNewArrowfromSideScore)
        {
            AddNewArrow(1);
        }
        else if (score == 5)
        {
            AddNewArrow(2);
        }

        // Checks If arrow is Sufficient to Continue the game
        CheckArrowCount(arrowCount);

        // Update the UI to reflect the new score
        UpdateUI();
    }

    // Returns Highest Score
    int GetHighestScore()
    {
        return PlayerPrefs.GetInt(highScoreKey, 0);
    }

    void SaveHighScore()
    {
        // Setting key-value pair
        PlayerPrefs.SetInt(highScoreKey, totalScore);
        PlayerPrefs.Save();
    }

    // Load HighScore from memory
    void LoadHighestScore()
    {
        score = GetHighestScore();
    }

    // Reduces arrowCount if used
    public void UseArrow()
    {
        --arrowCount;
        CheckArrowCount(arrowCount);
        UpdateUI();
    }

    public void AddNewArrow(int arrow)
    {
        arrowCount += arrow + 1;
        UpdateUI();
    }

    public void RestartNewGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    // Checks for valid no of arrows
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
            timerText.enabled = false;
            HighScore.enabled = true;
            UpdateUI(); // Update UI to show 0 arrows
        }
    }
}
