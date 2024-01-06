using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float arrowSpeed = 5f;
    [SerializeField] private float destroyAfterTime = 1f;
    [SerializeField] private float destroyAtY = 10f;
    [SerializeField] private Transform hitBoard;
    [SerializeField] private Rigidbody2D arrowRigidbody;
    [SerializeField] private CapsuleCollider2D capsuleCollider2d;

    private bool isAttached = false;

    void Start()
    {
        InitializeComponents();
        ShootArrow();
    }

    void Update()
    {
        if (isAttached && gameManager != null && gameManager.totalScore >= BoardMoveScript.moveBoardScore)
        {
            MoveArrowWithBoard();
        }
        else if (arrowRigidbody != null && arrowRigidbody.position.y >= destroyAtY)
        {
            HandleMissedArrow();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Target"))
            {
                HandleCollision(1);
            }
            else if (collision.gameObject.CompareTag("Center"))
            {
                HandleCollision(3);
            }
        }
    }

    void InitializeComponents()
    {
        arrowRigidbody = GetComponent<Rigidbody2D>();
        capsuleCollider2d = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager")?.GetComponent<GameManager>();
        hitBoard = GameObject.FindGameObjectWithTag("Target")?.transform;
    }

    void ShootArrow()
    {
        if (arrowRigidbody != null)
        {
            arrowRigidbody.velocity = new Vector2(0f, arrowSpeed);
        }
    }

    void MoveArrowWithBoard()
    {
        // Move the arrow with the board
        if (arrowRigidbody != null)
        {
            transform.position = new Vector3(arrowRigidbody.position.x, arrowRigidbody.position.y, 0f);
        }
    }

    void HandleMissedArrow()
    {
        // Arrow has missed the board, destroy it
        Destroy(gameObject);
        if (gameManager != null)
        {   
                gameManager.UseArrow();
        }
    }

    void AttachToBoard()
    {
        if (arrowRigidbody != null)
        {
            // Stop arrow movement and set it as a child of the board
            arrowRigidbody.velocity = Vector2.zero;
            transform.SetParent(hitBoard);
            isAttached = true;
        }
    }

    void HandleCollision(int score)
    {
        if (capsuleCollider2d != null && spriteRenderer != null)
        {
            capsuleCollider2d.enabled = false;
            AttachToBoard();
            gameManager?.UpdateScore(score);
            Destroy(gameObject, destroyAfterTime);
        }
    }

    public void DestroyCollider()
    {
        if (capsuleCollider2d != null)
        {
            Destroy(capsuleCollider2d);
            Debug.Log("Collider Destroyed");
            // capsuleCollider2d.enabled = false;
        }
    }
}
