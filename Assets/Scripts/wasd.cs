using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class wasd : MonoBehaviour
{

    public GameObject gm;

    //variables for jumping
    public float accel = 10f;
    public float accelTime = .3f;
    public bool grounded;

    public float health = 5f;
    public float score = 0f;
    public float speed = 10f;
    public Vector2 direction;

    public SpriteRenderer mySprite;
    public Collider2D myCol;
    public Rigidbody2D myRB;
    //we could declare the keys as variables here but we're using the new input system


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        gm = GameManager.gameManager.gameObject; //you can find a static variable without searching for it

        mySprite = GetComponent<SpriteRenderer>();
        myCol = GetComponent<Collider2D>();
        myRB = GetComponent<Rigidbody2D>();

        grounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        //clean our direction vector before figuring out what the WASD is this frame
        direction = Vector2.zero;

        //one fourth of a WASD
        if(Keyboard.current.wKey.isPressed) //checking input with the NEW InputSystem Keyboard.current struct
        {
            //direction += Vector2.up * speed; //we can use Vector2/3 shorthands for up/down/left/right/forwards
        }
        if(Keyboard.current.sKey.isPressed)
        {
            //direction += Vector2.down * speed;
        }
        if(Keyboard.current.aKey.isPressed)
        {
            direction += Vector2.left * speed;
        }
        if(Keyboard.current.dKey.isPressed)
        {
            direction += Vector2.right * speed;
        }

        //transform.Translate(direction); 
        //translate doesn't interact with physics or colliders so it's not ideal
        //instead we can make the direction input a velocity vector
        Vector3 newVelocity = direction;
        //then we can add in gravity/jump/existing Y value
        newVelocity.y = direction.y + myRB.linearVelocity.y;
        //finally we set the object velocity == our new target velocity
        myRB.linearVelocity = newVelocity; 


        //what if we want to flip the sprite to face how we move?
        //first we check to see if player input a new direction this turn or is idle
        if (direction != Vector2.zero)
        {
            //if the player is NOT idle, flip the sprite when moving left
            if (direction.x < 0f) 
            {
                mySprite.flipX = true;
            }
            else //make sure to flip it back when moving right!
            {
                mySprite.flipX = false;
            }
        }

        //what if we wanted to rotate? - these lines handle rotation
        float rot = 0f;
        if(Keyboard.current.qKey.isPressed)
        {
            rot += 1f;
        }
        if(Keyboard.current.eKey.isPressed)
        {
            rot -= 1f;
        }
        //transform.Rotate is good for rotating along the Z (camera-aligned) axis
        transform.Rotate(new Vector3(0, 0, rot));


        //example code for pressed and released keypresses:
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("the SPACEBAR was PRESSED this frame: " + Time.frameCount);
        }

        if(Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            Debug.Log("spacebar was RELEASED this frame: " + Time.frameCount);
        }    
    }

    void FixedUpdate()
    {
        if(Keyboard.current.spaceKey.isPressed && grounded)
        {
            myRB.AddForce(Vector2.up * accel);
        }
    }

    //this function runs when the gameObject hits a SOLID collider object
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hit an object");
        if (collision.gameObject.tag == "hazard")
        { 
            health--;
            gm.SendMessage("ChangePlayerHealth", health);
        }
        //this runs the frame we first contact an object
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        //this runs any frame we stay in contact with an object
        if (collision.gameObject.tag == "ground")
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //this runs the frame we leave contact with an object
        if(collision.gameObject.tag == "ground")
        {
            grounded = false;
        }
    }

    //this runs when the gameObject enters a trigger VOLUME
    void OnTriggerEnter2D(Collider2D collision)
    {

        collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;

        if (collision.gameObject.tag == "collectible")
        {
            score++;
            Destroy(collision.gameObject);
        }
    }
}
