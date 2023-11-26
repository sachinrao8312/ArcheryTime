using UnityEngine;

public class MoveBowScript : MonoBehaviour
{
    public Rigidbody2D bow;
    public GameManager gameManager;

    public float bowUpperBound = 4.6f;
    public float bowLowerBound = -4.6f;

    public float accelerationRate = 0.1f;
    public float bowSpeed = 2f;
    public float startTime;
    public float maxBowSpeed = 7f;
    public float movebowScore;

    private void Start()
    {
        startTime = Time.time;
        bow.velocity = new Vector2(0, -bowSpeed);

        GameObject managerObj = GameObject.FindGameObjectWithTag("GameManager");
        if (managerObj != null)
        {
            gameManager = managerObj.GetComponent<GameManager>();
        }
    }

    private void Update()
    {
        if (gameManager != null && gameManager.totalScore >= movebowScore)
        {
            AcclerateBoard();

            if (bow.transform.position.y > bowUpperBound)
            {
                Movebow(-bowSpeed);
            }
            else if (bow.transform.position.y < bowLowerBound)
            {
                Movebow(bowSpeed);
            }
        }
        else
        {
            // Bow is fixed at a specific position
            bow.transform.position = new Vector3(-0.57f, 0f, 0f);
        }
    }

    private void Movebow(float speed)
    {
        bow.velocity = new Vector2(0, speed);
    }

    private void AcclerateBoard()
    {
        float elapsedSeconds = Time.time - startTime;
        float timeToAcclerate = elapsedSeconds * accelerationRate;

        float newSpeed = Mathf.Min(bow.velocity.y + timeToAcclerate, maxBowSpeed);

        bowSpeed = newSpeed <= 0 ? bowSpeed : newSpeed;
    }
}
