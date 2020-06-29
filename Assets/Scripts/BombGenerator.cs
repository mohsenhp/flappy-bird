using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGenerator : MonoBehaviour
{
    public GameObject bombPrefab;         //A reference to the bomb prefab
    public float spawnInterval;           //The interval at which bombs are instantiated 

    private Vector2 ScreenBounds;         //A reference to the screen bounds

    // Start is called before the first frame update
    void Start()
    {
        ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(BombIsComing());
    }

    //Spawns the bombs at random but accessible places
    private void SpawnBomb()
    {
        if (GameControl.instance.gameStarted == true && GameControl.instance.gameOver == false)
        {
            float BombXPosition = Random.Range(ScreenBounds.x -1 , -ScreenBounds.x + 15);
            float BombYPosition = Random.Range(ScreenBounds.y - 1, -ScreenBounds.y + 4);
            GameObject bomb = Instantiate(bombPrefab) as GameObject;
            bomb.transform.position = new Vector2(BombXPosition, BombYPosition);
        }
    }

    //Sets the random interval at which bombs are spawned
    IEnumerator BombIsComing()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, spawnInterval));
            SpawnBomb();
        }
    }
}
