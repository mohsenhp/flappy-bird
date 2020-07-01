using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb2d;     //Holds a reference to the Rigidbody2D component of the enemy.
    private void Start()
    {
        //Get and store a reference to the Rigidbody2D attached to this GameObject.
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameControl.instance.gameStarted == true)
        {
            // If the game is over, stop scrolling.
            if (GameControl.instance.gameOver == true)
            {
                rb2d.velocity = Vector2.zero;
            }

            //Gets a hold of the enemy's sprite renderer.
            SpriteRenderer spriteR = gameObject.GetComponent<SpriteRenderer>();

            //Destroys the enemies once they are out of the scene.
            if (!spriteR.isVisible)
                Destroy(gameObject);
        }
    }
}
