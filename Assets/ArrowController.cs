using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public GameManager gameManager;
    public float arrowSpeed = 5f;
    public float destroyAfterTime = 1f;
    public Rigidbody2D arrowRigidbody;
    public float DestroyAt = 10f;
    private Transform HitBoard;
    private bool isAttached = false;

    // public AudioSource arrowHit;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        HitBoard = GameObject.FindGameObjectWithTag("Target").transform;
        ShootArrow();
        
    }

    void Update()
    {
        // Moves the arrow that is attached to the board
        if (isAttached && gameManager.totalScore >= BoardMoveScript.moveBoardScore)
        {
            // Move the arrow with the board
            transform.position = new Vector3(arrowRigidbody.position.x, arrowRigidbody.position.y, 0f);
        }
        
        else if (transform.position.x > DestroyAt)
        {
            // Arrow has missed the board, destroy it
            Destroy(gameObject);
        }
    }

    void ShootArrow()
    {
        // Set initial velocity for shooting
        arrowRigidbody.velocity = new Vector2(arrowSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            // Arrow has collided with the board, attach it and destroy after some time
            AttachToBoard();
            // arrowHit.Play();
            gameManager.UpdateScore(1);
            Destroy(gameObject, destroyAfterTime);
        }
    }

    void AttachToBoard()
    {
        // Stop arrow movement and set it as a child of the board
        arrowRigidbody.velocity = Vector2.zero;
        transform.SetParent(HitBoard);
        isAttached = true;
    }
}
