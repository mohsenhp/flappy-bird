using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Rigidbody2D rb2d;                 //Holds a reference to the Rigidbody2D component of the bomb.

    private void Start()
    {
        //Get and store a reference to the Rigidbody2D attached to the bomb.
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //If the gae is already started
        if (GameControl.instance.gameStarted == true)
        {
            // If the game is over, stop scrolling.
            if (GameControl.instance.gameOver == true)
            {
                rb2d.velocity = Vector2.zero;
            }

            //Gets a hold of the bomb's sprite renderer.
            SpriteRenderer spriteR = gameObject.GetComponent<SpriteRenderer>();

            //Destroys the bombs once they are out of the scene.
            if (!spriteR.isVisible)
                Destroy(gameObject);
        }
    }
}
