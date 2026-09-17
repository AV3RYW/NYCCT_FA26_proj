using UnityEngine;

public class HelloWorld : MonoBehaviour
{
    public SpriteRenderer playerSprite;
    public float speed;
    public float health = 5;
    public float score = 0;
    public float timer = 0;
    public GameObject coin;

    public string startText = "Hello World";
    // Start is called once before the first execution of Update after this script gets loaded into your game scene
    void Start()
    {
        playerSprite.color = Color.white;
        Debug.Log(startText);
    }
    void Update()
    {
        if(health <= 0 )
        { playerSprite.color = Color.red; }
        timer += Time.deltaTime;
        if(timer > 3f) //for a repeating timer, just reset timer when it hits the limit
        {
            Vector2 pos;
            pos.x = Random.Range(-9, 9);
            pos.y = Random.Range(-4, 5);
            Instantiate(coin, pos, Quaternion.identity);
            timer = 0;
        }
    }
    //this function runs when the gameObject hits a SOLID collider object
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hit an object");
        if (collision.gameObject.tag == "hazard")
            { health--; }
    }

    //this runs when the gameObject enters a trigger VOLUME
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "collectible")
        {
            score++;
            Destroy(collision.gameObject);
        }
    }
}
