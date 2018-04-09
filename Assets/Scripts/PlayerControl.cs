using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public GameObject player;
    public GameObject interactBox;
    private Rigidbody2D playerRigidBody;
    private BoxCollider2D interactBoxCollision;
    public float playerSpeedMult;
    public float playerMaxSpeed;
    private byte playerDirection; //0 is right, 1 is up, 2 is left, 3 is down.
    
    // Use this for initialization
    void Start ()
    {
        playerRigidBody = player.GetComponent<Rigidbody2D>();
        interactBoxCollision = interactBox.GetComponent<BoxCollider2D>();
        playerDirection = 0;
    }

    /// <summary>
    /// Limits the velocity of the player game object to the playerMaxSpeed variable.
    /// </summary>
    private void LimitVelocity()
    {
        if (playerRigidBody.velocity.x > playerMaxSpeed)
        {
            playerRigidBody.velocity = new Vector2(playerMaxSpeed, playerRigidBody.velocity.y);
        }
        else if (playerRigidBody.velocity.x < -playerMaxSpeed)
        {
            playerRigidBody.velocity = new Vector2(-playerMaxSpeed, playerRigidBody.velocity.y);
        }

        if (playerRigidBody.velocity.y > playerMaxSpeed)
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, playerMaxSpeed);
        }
        else if (playerRigidBody.velocity.y < -playerMaxSpeed)
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, -playerMaxSpeed);
        }

    }

    /// <summary>
    /// Sets the direction that the player is facing based off of the movement vector parameter.
    /// </summary>
    /// <param name="velocity"></param>
    private void GetDirection(Vector2 movement)
    {
        float absMovementX = Mathf.Abs(movement.x); //no sense in calculating these more than twice per tick
        float absMovementY = Mathf.Abs(movement.y);

        if (absMovementX > absMovementY)
        {
            if(movement.x > 0)
            {
                playerDirection = 0;
            }
            else if(movement.x < 0)
            {
                playerDirection = 2;
            }
        }
        else if(absMovementX < absMovementY)
        {
            if(movement.y > 0)
            {
                playerDirection = 1;
            }
            else if(movement.y < 0)
            {
                playerDirection = 3;
            }
        }
    }
    /// <summary>
    /// Reorients the Interact Box game object to the appropriate orientation in relation to the players current direction of movement.
    /// </summary>
    private void ReorientInteractBox()
    {
        int rotationZ = 0;
        float playerObjectWidth = player.GetComponent<BoxCollider2D>().size.x;
        float playerObjectHeight = player.GetComponent<BoxCollider2D>().size.y;
        float interactBoxWidth = interactBox.GetComponent<BoxCollider2D>().size.x;
        float interactBoxHeight = interactBox.GetComponent<BoxCollider2D>().size.y;
        if (playerDirection == 0 || playerDirection == 2)
        {
            rotationZ = 0;
            if(playerDirection == 0)
            {
                interactBox.transform.position = new Vector3(player.transform.position.x + (playerObjectWidth / 2.0f) + (interactBoxWidth / 2.0f),
                                                             player.transform.position.y,
                                                             player.transform.position.z);
            }
            else if (playerDirection == 2)
            {
                interactBox.transform.position = new Vector3(player.transform.position.x - (playerObjectWidth / 2.0f) - (interactBoxWidth / 2.0f),
                                                             player.transform.position.y,
                                                             player.transform.position.z);
            }
        }
        else
        {
            rotationZ = 90;
            if (playerDirection == 1)
            {
                interactBox.transform.position = new Vector3(player.transform.position.x,
                                                             player.transform.position.y + (playerObjectHeight / 2.0f) + (interactBoxHeight / 2.0f),
                                                             player.transform.position.z);
            }
            else if (playerDirection == 3)
            {
                interactBox.transform.position = new Vector3(player.transform.position.x,
                                                             player.transform.position.y - (playerObjectHeight / 2.0f) - (interactBoxHeight / 2.0f),
                                                             player.transform.position.z);
            }
        }

        interactBox.transform.eulerAngles = new Vector3(
                interactBox.transform.eulerAngles.x,
                interactBox.transform.eulerAngles.y,
                rotationZ
                );
        
    }

    void FixedUpdate()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalAxis * playerSpeedMult, verticalAxis * playerSpeedMult);
        playerRigidBody.AddForce(movement,ForceMode2D.Impulse);

        LimitVelocity();
        
        //if there is input, get new direction and reorient the interact box.
        if (horizontalAxis != 0.0f || verticalAxis != 0.0f)
        {
            GetDirection(movement);
            ReorientInteractBox();
        }

        //stop player movement if there is no input.
        if(horizontalAxis == 0.0f)
        {
            playerRigidBody.velocity = new Vector2(0.0f, playerRigidBody.velocity.y);
        }
        if (verticalAxis == 0.0f)
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 0.0f);
        }

        
    }

    // Update is called once per frame
    void Update ()
    {

    }
}
