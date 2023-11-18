using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Text arrowCountText;
    public Text HighScore;
    public int totalScore = 0;
    public int arrowCount = 10;
    public GameObject gameOverScreen;
    public GameObject HitBoard;
    public GameObject arrow;

    void Start()
    {
        HitBoard = GameObject.FindGameObjectWithTag("Target");

        // Initialize the score text when the game starts
        UpdateUI();

    }

    void UpdateUI()
    {
        // Update the score displayed in the UI
        scoreText.text = "Score: " + totalScore.ToString();
        HighScore.text = "HighScore: " + totalScore.ToString() ;
        arrowCountText.text = "Arrows: " + arrowCount.ToString();

    }

    public void UpdateScore(int score)
    {
        // Update the score
        totalScore += score;

        if (score == 5)
        {
            AddNewArrow();
        }
        CheckArrowCount(arrowCount);

        // Update the UI to reflect the new score
        UpdateUI();
    }


    public void UseArrow()
    {
        arrowCount--;
        CheckArrowCount(arrowCount);
        UpdateUI();
    }

    public void AddNewArrow()
    {
        arrowCount += 2;
        UpdateUI();
    }

    public void RestartNewGame()
    {
        Debug.LogError("load new scene");
        SceneManager.LoadScene("MainScene");
    }

    public void CheckArrowCount(int arrowCount)
    {
        if (arrowCount <= 0)
        {
            Debug.Log("Game Over");
            arrowCount = 0; // Ensure arrow count doesn't go negative
            gameOverScreen.SetActive(true); 
            HitBoard.SetActive(false); 
            arrow.SetActive(false); 
            scoreText.enabled = false;
            arrowCountText.enabled = false;
            UpdateUI(); // Update UI to show 0 arrows

        }
    }
}
