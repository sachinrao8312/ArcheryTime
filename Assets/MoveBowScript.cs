using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBowScript : MonoBehaviour
{
    public Rigidbody2D bow;
    public float bowSpeed = 5f;
    public float bowUpperBound = 4.6f;
    public float bowLowerBound = -4.6f;
    public GameManager gameManager;
    public float movebowScore ;
    // public SpriteRenderer myBowSprite;
    void Start()
    {
        // Move the bow downwards initially
        bow.velocity = new Vector2(0, -bowSpeed);

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
            // Checks for score and moves bow only after score is greater than or equal to movebowScore
            if (gameManager.totalScore >= movebowScore)
            {
                // Move the bow continuously
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
                bow.transform.position = new Vector3(3.887575f, 0f, 0f);
            }
        }
    }

    void Movebow(float speed)
    {
        // Move the bow in the Y direction
        bow.velocity = new Vector2(0, speed);
    }

    //Change the BOwSprite
//     void ChangeSprite()
// {
//     // Load a new sprite from Resources (assuming the sprite is in the Resources folder)
//     // Sprite newSprite = Resources.Load<Sprite>("/");

//     // Assign the new sprite to the SpriteRenderer
//     mySpriteRenderer.sprite = newSprite;
// }
}
