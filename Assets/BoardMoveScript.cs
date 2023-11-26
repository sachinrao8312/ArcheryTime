using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMoveScript : MonoBehaviour
{
    public Rigidbody2D board;
    public GameManager gameManager;

    public float maxBoardSpeed;
    public float initialboardSpeed;
    public static float moveBoardScore = 35f;
    public float accelerationRate;
    private float startTime;

    public float UpperBound;
    public float LowerBound;
    private float targetYCoordinate;

    void Start()
    {
        // Starts the time
        startTime = Time.time;

        // Check if the GameManager is not null before accessing it
        GameObject managerObj = GameObject.FindGameObjectWithTag("GameManager");
        gameManager = managerObj?.GetComponent<GameManager>();
    }

    void Update()
    {
        AcclerateBoard();

        // Check if the gameManager is not null
        if (gameManager != null)
        {
            if (gameManager.totalScore < moveBoardScore)
            {
                // Check if the board is close enough to the target Y-coordinate
                if (Mathf.Abs(board.transform.position.y - targetYCoordinate) < 0.1f)
                {
                    MoveBoard(0); // Stop the board
                }
                // Move the board continuously
                MoveBoardContinuously();

            }
            // Checks for score and moves the board only after the score is greater than or equal to moveBoardScore
            else if (gameManager.totalScore >= moveBoardScore)
            {
                MoveBoardContinuously();
                AcclerateBoard();

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

    void MoveBoardContinuously()
    {
        // Move the board continuously
        if (board.transform.position.y > UpperBound)
        {
            MoveBoard(-initialboardSpeed);
        }
        else if (board.transform.position.y < LowerBound)
        {
            MoveBoard(initialboardSpeed);
        }
    }

    void AcclerateBoard()
    {
        float elapsedSeconds = Time.time - startTime;
        float timeToAcclerate = elapsedSeconds * accelerationRate;

        // Increase Speed Gradually based On Score
        float scoreToAcclerate = gameManager.totalScore * accelerationRate * 0.1f;

        // Choose between current Speed and MaxSpeed
        float newSpeed = Mathf.Min(board.velocity.y + scoreToAcclerate + timeToAcclerate, maxBoardSpeed);

        initialboardSpeed = newSpeed > 0 ? newSpeed : initialboardSpeed;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            targetYCoordinate = Random.Range(LowerBound, UpperBound);

            // Move the board towards the target Y-coordinate                    
            MoveBoard(targetYCoordinate > 0 ? maxBoardSpeed : -maxBoardSpeed);
        }
    }
}
