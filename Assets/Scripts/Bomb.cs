using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bomb : MonoBehaviour
{
	private void Update()
	{
        //Gets a hold of the bomb's sprite renderer.
        SpriteRenderer spriteR = gameObject.GetComponent<SpriteRenderer>();
        
        //Destroys the bombs once they are out of the scene.
        if (!spriteR.isVisible)
            Destroy(gameObject);
    }
}
