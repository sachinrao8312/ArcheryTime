using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Text arrowCountText;
    public int totalScore = 0;
    public int arrowCount = 10;

    void Start()
    {
        // Initialize the score text when the game starts
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        // Update the score displayed in the UI
        scoreText.text = "Score: " + totalScore.ToString();
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
        // Update the UI to reflect the new score
        UpdateScoreUI();
    }


    public void UseArrow()
    {
        arrowCount--;

        if (arrowCount == 0)
        {
            Debug.Log("Game Over");
        }
        UpdateScoreUI();
    }

    public void AddNewArrow()
    {
        arrowCount++;
        UpdateScoreUI();
    }
}
