using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class wasd : MonoBehaviour
{

    public GameObject gm;

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
        gm = HelloWorld.gameManager.gameObject; //you can find a static variable without searching for it

        mySprite = GetComponent<SpriteRenderer>();
        myCol = GetComponent<Collider2D>();
        myRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //clean our direction vector before figuring out what the WASD is this frame
        direction = Vector2.zero;

        //one fourth of a WASD
        if(Keyboard.current.wKey.isPressed) //checking input with the NEW InputSystem Keyboard.current struct
        {
            direction += Vector2.up * speed; //we can use Vector2/3 shorthands for up/down/left/right/forwards
        }
        if(Keyboard.current.sKey.isPressed)
        {
            direction += Vector2.down * speed;
        }
        if(Keyboard.current.aKey.isPressed)
        {
            direction += Vector2.left * speed;
        }
        if(Keyboard.current.dKey.isPressed)
        {
            direction += Vector2.right * speed;
        }

        transform.Translate(direction);

        //what if we wanted to rotate?
        float rot = 0f;
        if(Keyboard.current.qKey.isPressed)
        {
            rot += 1f;
        }
        if(Keyboard.current.eKey.isPressed)
        {
            rot -= 1f;
        }
        transform.Rotate(new Vector3(0, 0, rot));
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            //code that executes a jump could go in here
            Debug.Log("the SPACEBAR was PRESSED this frame: " + Time.frameCount);
        }

        if(Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            Debug.Log("spacebar was RELEASED this frame: " + Time.frameCount);
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
