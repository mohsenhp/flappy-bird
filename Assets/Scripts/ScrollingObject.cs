using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour 
{
	private Rigidbody2D rb2d;
	private void Start()
	{
		//Get and store a reference to the Rigidbody2D attached to this GameObject.
		rb2d = GetComponent<Rigidbody2D>();
		rb2d.velocity = Vector2.zero;
	}

	void Update()
	{
		if (GameControl.instance.gameStarted == true)
		{
			//Start the object moving
			rb2d.velocity = new Vector2(GameControl.instance.scrollSpeed, 0);

			// If the game is over, stop scrolling.
			if (GameControl.instance.gameOver == true)
			{
				rb2d.velocity = Vector2.zero;
			}
		}
	}
}
