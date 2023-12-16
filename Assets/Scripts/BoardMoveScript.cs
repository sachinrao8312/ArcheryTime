using UnityEngine;

public class BoardMoveScript : MonoBehaviour
{
    // Fields made private and serialized for better encapsulation and Unity Inspector visibility
    [SerializeField] private Rigidbody2D board;
    [SerializeField] private GameManager gameManager;

    [Header("Movement Settings")]
    [SerializeField] private float maxBoardSpeed = 5f;
    [SerializeField] private float initialBoardSpeed = 2f;
    [SerializeField] private float accelerationRate = 0.1f;
    public static float moveBoardScore = 35f;

    [Header("X-axis Movement Settings")]
    [SerializeField] private float minX = -5f;
    [SerializeField] private float maxX = 5f;

    private float startTime;
    private float targetXCoordinate;

    private void Start()
    {
        startTime = Time.time;
        FindGameManager();
    }

    private void FindGameManager()
    {
        GameObject managerObj = GameObject.FindGameObjectWithTag("GameManager");
        gameManager = managerObj?.GetComponent<GameManager>();
        // Add null check for GameManager
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found on BoardMoveScript.");
        }
    }

    private void Update()
    {
        // Add null check for GameManager
        if (gameManager == null)
        {
            return;
        }

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
            ResetBoardPosition();
        }
    }

    private void MoveBoard(float speed)
    {
        // Add null check for board
        if (board != null)
        {
            board.velocity = new Vector2(speed, 0f);
        }
    }

    private void MoveBoardContinuously()
    {
        // Add null check for board
        if (board != null)
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
    }

    private void AccelerateBoard()
    {
        float elapsedSeconds = Time.time - startTime;
        float timeToAccelerate = elapsedSeconds * accelerationRate;

        float scoreToAccelerate = gameManager.totalScore * accelerationRate * 0.1f;
        float newSpeed = Mathf.Min(board.velocity.x + scoreToAccelerate + timeToAccelerate, maxBoardSpeed);

        initialBoardSpeed = newSpeed > 0 ? newSpeed : initialBoardSpeed;
    }

    private void ResetBoardPosition()
    {
        // Add null check for board
        if (board != null)
        {
            board.transform.position = new Vector3(0f, 9f, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Arrow"))
        {
            targetXCoordinate = Random.Range(minX, maxX);
            MoveBoard(targetXCoordinate > 0 ? maxBoardSpeed : -maxBoardSpeed);
        }
    }
}
