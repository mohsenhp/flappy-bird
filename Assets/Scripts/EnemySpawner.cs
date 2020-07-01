using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Parent;              //The game object all enemies will be childeren of
    public GameObject enemyPrefab;         //A reference to the enemy prefab
    public float spawnInterval;            //The interval at which bombs are instantiated 

    private Vector2 ScreenBounds;         //A reference to the screen bounds
    public float enemySpeed;              //How fast does the ebemy fly?  


    // Start is called before the first frame update
    void Start()
    {
        //Gets a hold of screen bounds, so the enemies won't be spawned out of the bounds.
        ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        //Spawn the enemies randomly, but withing a range.
        StartCoroutine(EnemyIsComing());
    }

    private void SpawnEnemy()
    {
        if (GameControl.instance.gameStarted == true && GameControl.instance.gameOver == false)
        {
            //Enemies are always spawned at the very right end of the screen, but...
            float enemyXPosition = ScreenBounds.x;
            //... their Y position will be chosen randomly (within a range).
            float enemyYPosition = Random.Range(ScreenBounds.y - 1, -ScreenBounds.y + 4);
            //Instantiates the enemies
            GameObject Enemy = Instantiate(enemyPrefab) as GameObject;
            //Spawns the enemies at random but accessible places
            Enemy.transform.position = new Vector2(enemyXPosition, enemyYPosition);
            //Makes them become childeren of a given parent in the hierarchy
            Enemy.transform.SetParent(Parent.transform);
            //Assigns them a given speed, set in the editor.
            Enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(enemySpeed, 0);
        }
    }

    IEnumerator EnemyIsComing()
    {
        while (true)
        {
            //Waits for a random amount of time (within a range), and then...
            yield return new WaitForSeconds(Random.Range(1, spawnInterval));
            //Spawns the enemy
            SpawnEnemy();
        }
    }
}
