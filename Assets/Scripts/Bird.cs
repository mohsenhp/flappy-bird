using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour 
{
	public float upForce;					//Upward force of the "flap".
	private bool isDead = false;			//Has the player collided with a wall?

	private Animator anim;					//Reference to the Animator component.
	private Rigidbody2D rb2d;               //Holds a reference to the Rigidbody2D component of the bird.
	void Start()
	{
		//Get reference to the Animator component attached to this GameObject.
		anim = GetComponent<Animator> ();
		//Get and store a reference to the Rigidbody2D attached to this GameObject.
		rb2d = GetComponent<Rigidbody2D>();
		//If the game hasn't started yet, let the bird just float.
		rb2d.isKinematic = true;
	}

	void Update()
	{
		//Don't allow control if the bird has died.
		if (isDead == false) 
		{
			//If the game has started...
			if (GameControl.instance.gameStarted == true) 
			{
				//Now the player has to control the bird.
				rb2d.isKinematic = false;

				//Look for input to trigger a "flap".
				if (Input.GetMouseButtonDown(0))
				{
					//...tell the animator about it and then...
					anim.SetTrigger("Flap");
					//...zero out the birds current y velocity before...
					rb2d.velocity = Vector2.zero;
					//	new Vector2(rb2d.velocity.x, 0);
					//..giving the bird some upward force.
					rb2d.AddForce(new Vector2(0, upForce));
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
			// Zero out the bird's velocity
			rb2d.velocity = Vector2.zero;
			// If the bird collides with something set it to dead...
			isDead = true;
			//...tell the Animator about it...
			anim.SetTrigger("Die");
			//...and tell the game control about it.
			GameControl.instance.BirdDied();
	}

	//Once the bird hits a bomb (power-up)
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform.name == "Bomb(Clone)")
		{
			//Detroy the bomb game object and...
			Destroy(other.gameObject);
			//... and let the Game Control know that a power-up has been acquired.
			GameControl.instance.BombAcquired();
		}
		else if (other.transform.name == "Enemy(Clone)")
		{
			// Zero out the bird's velocity
			rb2d.velocity = Vector2.zero;
			// If the bird collides with something set it to dead...
			isDead = true;
			//...tell the Animator about it...
			anim.SetTrigger("Die");
			//...and tell the game control about it.
			GameControl.instance.BirdDied();
		}
	}
}
