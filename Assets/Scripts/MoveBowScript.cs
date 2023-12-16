using UnityEngine;

public class MoveBowScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D bow;
    [SerializeField] private GameManager gameManager;

    [Header("Bow Movement Settings")]
    [SerializeField] private float accelerationRate = 0.1f;
    [SerializeField] private float bowSpeed = 2f;
    [SerializeField] private float maxBowSpeed = 7f;
    [SerializeField] private float moveBowScore;

    [Header("X-axis Movement Settings")]
    [SerializeField] private float minX = -4.6f;
    [SerializeField] private float maxX = 4.6f;

    private float startTime;

    private void Start()
    {
        InitializeComponents();
        SetupBowVelocity();
    }

    private void Update()
    {
        if (gameManager != null && gameManager.totalScore >= moveBowScore)
        {
            AccelerateBow();
            MoveBowContinuously();
        }
    }

    private void InitializeComponents()
    {
        startTime = Time.time;

        if (bow == null)
        {
            bow = GetComponent<Rigidbody2D>();
        }

        GameObject managerObj = GameObject.FindGameObjectWithTag("GameManager");
        if (managerObj != null)
        {
            gameManager = managerObj.GetComponent<GameManager>();
        }
        else
        {
            Debug.LogError("GameManager not found on MoveBowScript.");
        }
    }

    private void SetupBowVelocity()
    {
        if (bow != null)
        {
            bow.velocity = new Vector2(bowSpeed, 0f);
        }
        else
        {
            Debug.LogError("Rigidbody2D not assigned to MoveBowScript.");
        }
    }

    private void MoveBow(float speed)
    {
        if (bow != null)
        {
            bow.velocity = new Vector2(speed, 0);
        }
    }

    private void MoveBowContinuously()
    {
        if (bow != null)
        {
            // Check if the bow is at the bounds, and reverse the direction if necessary
            if (bow.transform.position.x >= maxX)
            {
                MoveBow(-bowSpeed);
            }
            else if (bow.transform.position.x <= minX)
            {
                MoveBow(bowSpeed);
            }
        }
    }

    private void AccelerateBow()
    {
        if (bow != null)
        {
            float elapsedSeconds = Time.time - startTime;
            float timeToAccelerate = elapsedSeconds * accelerationRate;

            float newSpeed = Mathf.Min(bow.velocity.x + timeToAccelerate, maxBowSpeed);

            bowSpeed = newSpeed <= 0 ? bowSpeed : newSpeed;
        }
    }
}
