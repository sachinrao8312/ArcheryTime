using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMoveScript : MonoBehaviour
{
    public Rigidbody2D board;
    public GameManager gameManager;

    public float boardSpeed = 5f;
    public float maxBoardSpeed = 5f;
    public static float moveBoardScore = 25f;

    public float UpperBound = 0f;
    public float LowerBound = 0f;

    void Start()
    {
        CalculateBounds();
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

    public void CalculateBounds()
    {
        float screenHeight = Camera.main.orthographicSize * 2f;
        float screenWidth = screenHeight * Camera.main.aspect;
        UpperBound  = Camera.main.ScreenToWorldPoint(new Vector3(
            0,screenHeight,0
        )).y ;

        UpperBound += (screenHeight - 2.1f );
        LowerBound  =  -UpperBound ;

    }
}