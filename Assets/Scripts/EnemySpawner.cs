using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;         //A reference to the enemy prefab
    public float spawnInterval;           //The interval at which bombs are instantiated 

    private Vector2 ScreenBounds;         //A reference to the screen bounds

    public float enemySpeed;                   //How fast does the bird fly? (Make sure this coefficient is not set... 




    // Start is called before the first frame update
    void Start()
    {
        ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(EnemyIsComing());
    }

    private void SpawnEnemy()
    {
        if (GameControl.instance.gameStarted == true && GameControl.instance.gameOver == false)
        {
            float enemyXPosition = Random.Range(ScreenBounds.x - 1, -ScreenBounds.x + 15);
            float enemyYPosition = Random.Range(ScreenBounds.y - 1, -ScreenBounds.y + 4);
            GameObject Enemy = Instantiate(enemyPrefab) as GameObject;
            Enemy.transform.position = new Vector2(enemyXPosition, enemyYPosition);
        }
    }

    IEnumerator EnemyIsComing()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, spawnInterval));
            SpawnEnemy();
        }
    }
}
