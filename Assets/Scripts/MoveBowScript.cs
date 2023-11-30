using UnityEngine;

public class MoveBowScript : MonoBehaviour
{
    public Rigidbody2D bow;
    public GameManager gameManager;

    public float bowLeftBound = -2.5f;
    public float bowRightBound = 2.5f;

    public float accelerationRate = 0.1f;
    public float bowSpeed = 2f;
    public float startTime;
    public float maxBowSpeed = 7f;
    public float moveBowScore;

    private void Start()
    {
        startTime = Time.time;
        bow.velocity = new Vector2(-bowSpeed, 0); // Adjust initial velocity for horizontal motion

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

            if (bow.transform.position.x > bowRightBound)
            {
                MoveBow(-bowSpeed);
            }
            else if (bow.transform.position.x < bowLeftBound)
            {
                MoveBow(bowSpeed);
            }
        }
        else
        {
            // Bow is fixed at a specific position
            bow.transform.position = new Vector3(0f, 0f, 0f); // Adjust position for portrait mode
        }
    }

    private void MoveBow(float speed)
    {
        bow.velocity = new Vector2(speed, 0); // Adjust velocity for horizontal motion
    }

    private void AccelerateBow()
    {
        float elapsedSeconds = Time.time - startTime;
        float timeToAccelerate = elapsedSeconds * accelerationRate;

        float newSpeed = Mathf.Min(bow.velocity.x + timeToAccelerate, maxBowSpeed);

        bowSpeed = newSpeed <= 0 ? bowSpeed : newSpeed;
    }
}
