using System.Collections;
using UnityEngine;

public class BoardMoveScript : MonoBehaviour
{
    public Rigidbody2D board;
    public GameManager gameManager;

    public float maxBoardSpeed;
    public float initialBoardSpeed;
    public static float moveBoardScore = 35f;
    public float accelerationRate;
    private float startTime;

    private float targetXCoordinate;

    // Add these variables for x-axis movement
    public float minX = -5f;
    public float maxX = 5f;

    void Start()
    {
        startTime = Time.time;
        GameObject managerObj = GameObject.FindGameObjectWithTag("GameManager");
        gameManager = managerObj?.GetComponent<GameManager>();
    }

    void Update()
    {
        if (gameManager != null)
        {
            if (gameManager.totalScore < moveBoardScore)
            {
                if (Mathf.Abs(board.transform.position.x - targetXCoordinate) <= 0.1f)
                {
                    MoveBoard(0);
                }
                MoveBoardContinuously();
            }
            else if (gameManager.totalScore >= moveBoardScore)
            {
                MoveBoardContinuously();
                AccelerateBoard();
            }
            else
            {
                board.transform.position = new Vector3(0f, 9f, 0f);
            }
        }
    }

    void MoveBoard(float speed)
    {
        board.velocity = new Vector2(speed, 0f);
    }

    void MoveBoardContinuously()
    {
        if (board.transform.position.x >= maxX)
        {
            MoveBoard(-initialBoardSpeed);
        }
        else if (board.transform.position.x <= minX)
        {
            MoveBoard(initialBoardSpeed);
        }
    }

    void AccelerateBoard()
    {
        float elapsedSeconds = Time.time - startTime;
        float timeToAccelerate = elapsedSeconds * accelerationRate;

        float scoreToAccelerate = gameManager.totalScore * accelerationRate * 0.1f;
        float newSpeed = Mathf.Min(board.velocity.x + scoreToAccelerate + timeToAccelerate, maxBoardSpeed);

        initialBoardSpeed = newSpeed > 0 ? newSpeed : initialBoardSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            targetXCoordinate = Random.Range(minX, maxX);
            MoveBoard(targetXCoordinate > 0 ? maxBoardSpeed : -maxBoardSpeed);
        }
    }
}
