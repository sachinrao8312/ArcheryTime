using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBowScript : MonoBehaviour
{
    public Rigidbody2D bow;
    public GameManager gameManager;

    public float bowUpperBound = 0f;
    public float bowLowerBound = 0f;
    
    public float bowSpeed = 5f;
    public float movebowScore ;
    // public SpriteRenderer myBowSprite;
    void Start()
    {   
        
        CalculateBounds();
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

    public  void CalculateBounds()
    {
        float screenHeight = Camera.main.orthographicSize * 2f;
        float screenWidth = screenHeight * Camera.main.aspect;
        bowUpperBound  = Camera.main.ScreenToWorldPoint(new Vector3(
            0,screenHeight,0
        )).y ;

        bowUpperBound += (screenHeight - 2.1f );
        bowLowerBound  =  -bowUpperBound ;

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
