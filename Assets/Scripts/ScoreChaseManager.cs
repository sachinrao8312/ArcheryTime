using UnityEngine;
using TMPro;

public class ScoreChaseManager : MonoBehaviour
{
    public TMP_Text scoreChaseText;
    public int scoreThreshold = 25; // Example threshold for demonstration
    
                                    // public float chaseDuration = 60f; // Example duration for demonstration
                                    // private float chaseTimer;
    public static ScoreChaseManager Instance { get; private set; }

    void Start()
    {
        // Initialize score chase timer
        // chaseTimer = chaseDuration;
    }


    void Update()
    {
        // Check if the GameManager instance is not null before accessing its members
        if (GameManager.Instance != null)
        {
            // Example: Check if the player achieved the score threshold
            if (GameManager.Instance.totalScore >= scoreThreshold)
            {
                // Example: Display a message or initiate a reward
                scoreChaseText.text = "Score Chase " + scoreThreshold.ToString();

                scoreThreshold += Random.Range(25, 50);
            }
        }
        else
        {
            // Handle the case where GameManager.Instance is null
            Debug.LogError("GameManager instance is null. Make sure GameManager is properly initialized.");
        }
    }



}
