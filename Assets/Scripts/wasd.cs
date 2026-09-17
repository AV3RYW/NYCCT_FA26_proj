using UnityEngine;
using UnityEngine.InputSystem;

public class wasd : MonoBehaviour
{
    public float speed = 10f;
    public Vector2 direction;
    //we could declare the keys as variables here but we're using the new input system


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
}
