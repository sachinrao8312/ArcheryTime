using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMoveScript : MonoBehaviour
{
    public Rigidbody2D board;
    public float boardSpeed = 5f;
    public float boardUpperBound = 4.6f;
    public float boardLowerBound = -4.6f;
    public GameManager gameManager;
    public static float moveBoardScore = 15f; 

    void Start()
    {
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
        // Check if the gameManager is not null
        if (gameManager != null)
        {
            // Checks for score and moves the board only after the score is greater than or equal to moveBoardScore
            if (gameManager.totalScore >= moveBoardScore)
            {
                // Move the board continuously
                if (board.transform.position.y > boardUpperBound)
                {
                    MoveBoard(-boardSpeed);
                }
                else if (board.transform.position.y < boardLowerBound)
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
}
