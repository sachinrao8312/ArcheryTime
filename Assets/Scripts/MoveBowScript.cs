using UnityEngine;

public class MoveBowScript : MonoBehaviour
{
    public Rigidbody2D bow;
    public GameManager gameManager;

    public float accelerationRate = 0.1f;
    public float bowSpeed = 2f;
    public float startTime;
    public float maxBowSpeed = 7f;
    public float moveBowScore;

    // Add these variables for x-axis movement
    public float minX = -4.6f;
    public float maxX = 4.6f;

    private void Start()
    {
        startTime = Time.time;
        bow.velocity = new Vector2(bowSpeed, 0f);

        GameObject managerObj = GameObject.FindGameObjectWithTag("GameManager");
        if (managerObj != null)
        {
            gameManager = managerObj.GetComponent<GameManager>();
        }
    }

    private void Update()
    {
        if (gameManager != null && gameManager.totalScore >= moveBowScore)
        {
            AccelerateBow();
            MoveBowContinuously();
        }
    }

    private void MoveBow(float speed)
    {
        bow.velocity = new Vector2(speed, 0);
    }

    private void MoveBowContinuously()
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

    private void AccelerateBow()
    {
        float elapsedSeconds = Time.time - startTime;
        float timeToAccelerate = elapsedSeconds * accelerationRate;

        float newSpeed = Mathf.Min(bow.velocity.x + timeToAccelerate, maxBowSpeed);

        bowSpeed = newSpeed <= 0 ? bowSpeed : newSpeed;
    }
}
