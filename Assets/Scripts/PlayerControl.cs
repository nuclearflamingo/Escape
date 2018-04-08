using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public GameObject player;
    private Rigidbody2D playerRigidBody;
    public float playerSpeedMult;
    public float playerMaxSpeed;
    
    // Use this for initialization
    void Start ()
    {
        playerRigidBody = player.GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Limits the velocity of the player game object to the playerMaxSpeed variable.
    /// <params>@None</params> 
    /// <returns>@Void</returns>
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

    void FixedUpdate()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalAxis * playerSpeedMult, verticalAxis * playerSpeedMult);
        playerRigidBody.AddForce(movement,ForceMode2D.Impulse);

        LimitVelocity();

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
