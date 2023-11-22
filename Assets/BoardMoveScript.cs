using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMoveScript : MonoBehaviour
{
    public Rigidbody2D board;
    public GameManager gameManager;

    public float boardSpeed = 5f;
    public float maxBoardSpeed = 15f;
    public static float moveBoardScore = 25f;
    public float accelerationRate = 0.1f;
    private float startTime;

    public float UpperBound = 4.6f;
    public float LowerBound = -4.6f;

    void Start()
    {
        //Starts the time
        startTime = Time.time;

        // Move the board downwards initially
        board.velocity = new Vector2(0, -boardSpeed);

        // Check if the GameManager is not null before accessing it
        GameObject managerObj = GameObject.FindGameObjectWithTag("GameManager");
        if (managerObj != null)
        {
            gameManager = managerObj.GetComponent<GameManager>();
        }
    }

    void Update()
    {
        AcclerateBoard();
        
        // Check if the gameManager is not null
        if (gameManager != null)
        {
            // Checks for score and moves the board only after the score is greater than or equal to moveBoardScore
            if (gameManager.totalScore >= moveBoardScore)
            {
                // Move the board continuously
                if (board.transform.position.y > UpperBound)
                {
                    MoveBoard(-boardSpeed);
                }
                else if (board.transform.position.y < LowerBound)
                {
                    MoveBoard(boardSpeed);
                }
            }
            else
            {
                // The board is fixed at a specific position
                board.transform.position = new Vector3(18.94757f, 0f, 0f);
            }

        }
    }

    void MoveBoard(float speed)
    {
        // Move the board in the Y direction
        board.velocity = new Vector2(0, speed);
    }
    

    
    public  void AcclerateBoard()
    {   
        float elapsedSeconds = Time.time - startTime;
        float timeToAcclerate = elapsedSeconds * accelerationRate;
        //Increase Speed Gradually based On Score
        float scoreToAcclerate = gameManager.totalScore * accelerationRate * 0.1f;

        //Choose b/w current Speed and MaxSpeed
        float newSpeed = Mathf.Min(board.velocity.y + scoreToAcclerate + timeToAcclerate,maxBoardSpeed);
        boardSpeed = newSpeed;
    }
}