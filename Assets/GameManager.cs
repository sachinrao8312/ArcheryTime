using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public int totalScore = 0;

    void Start()
    {
        // Initialize the score text when the game starts
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        // Update the score displayed in the UI
        scoreText.text = totalScore.ToString();
    }

    public void UpdateScore(int score)
    {
        // Update the score
        totalScore += score;

        // Update the UI to reflect the new score
        UpdateScoreUI();
    }
}
