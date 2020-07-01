using System.Collections;
using UnityEngine;

public class BombGenerator : MonoBehaviour
{
    public GameObject Parent;             //The game object all bombs will be childeren of
    public GameObject bombPrefab;         //A reference to the bomb prefab
    public float spawnInterval;           //The interval at which bombs are instantiated 
    private Vector2 ScreenBounds;         //A reference to the screen bounds
    public float bombSpeed;               //How fast does the power-up fly?

    void Start()
    {
        //Gets a hold of screen bounds, so the bombs won't be generated out of the bounds.
        ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        //Spawn the bombs randomly, but withing a range.
        StartCoroutine(BombIsComing());
    }

    private void SpawnBomb()
    {
        if (GameControl.instance.gameStarted == true && GameControl.instance.gameOver == false)
        {
            //Bombs are always spawned at the very right end of the screen, but...
            float BombXPosition = ScreenBounds.x;
            //... their Y position will be chosen randomly (within a range).
            float BombYPosition = Random.Range(ScreenBounds.y - 1, -ScreenBounds.y + 4);
            //Instantiates the bombs
            GameObject bomb = Instantiate(bombPrefab) as GameObject;
            //Spawns the bombs at random but accessible places
            bomb.transform.position = new Vector2(BombXPosition, BombYPosition);
            //Makes them become childeren of a given parent in the hierarchy
            bomb.transform.SetParent(Parent.transform);
            //Assigns them a given speed, set in the editor.
            bomb.GetComponent<Rigidbody2D>().velocity = new Vector2(bombSpeed, 0);
        }
    }

    IEnumerator BombIsComing()
    {
        while (true)
        {
            //Waits for a random amount of time (within a range), and then...
            yield return new WaitForSeconds(Random.Range(1, spawnInterval));
            //... spawns the bombs.
            SpawnBomb();
        }
    }
}
