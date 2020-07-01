using UnityEngine;
using System.Collections;

public class ColumnPool : MonoBehaviour 
{
	public GameObject columnPrefab;									//The column game object.
	public int columnPoolSize;									//How many columns to keep on standby.
	public float spawnRate;									//How quickly columns spawn.
	public float columnMin;									//Minimum y value of the column position.
	public float columnMax;									//Maximum y value of the column position.

	private GameObject[] columns;									//Collection of pooled columns.
	private int currentColumn = 0;									//Index of the current column in the collection.

	private Vector2 objectPoolPosition = new Vector2 (-15,-25);		//A holding position for our unused columns offscreen.
	private float spawnXPosition = 10f;

	private float timeSinceLastSpawned;

	public GameObject Parent;         // The parent of all of the columns


	void Start()
	{
		timeSinceLastSpawned = 0f;
		InitializeColumns();
	}

	//Initializes the columns at the start of the game
	private void InitializeColumns()
	{
		//Initialize the columns collection.
		columns = new GameObject[columnPoolSize];
		//Loop through the collection... 
		for (int i = 0; i < columnPoolSize; i++)
		{
			//...and create the individual columns.
			columns[i] = (GameObject)Instantiate(columnPrefab, objectPoolPosition, Quaternion.identity);
			columns[i].transform.SetParent(Parent.transform);

		}
	}

	//Destroys the current columns in case a power-up is used.
	private void DestroyObstacles()
	{
		//for (int i = 0; i < columnPoolSize; i++)
		//{
		//	//...and create the individual columns.
		//	Destroy(columns[i]);
		//}
		foreach (Transform child in Parent.transform)
			Destroy(child.gameObject);
		AudioManager.instance.PlayMusic("DestroyColumns");
	}


	//This spawns columns as long as the game is not over.
	void Update()
	{
		//If the game has started...
		if (GameControl.instance.gameStarted == true)
		{
			timeSinceLastSpawned += Time.deltaTime;

			if (GameControl.instance.gameOver == false && timeSinceLastSpawned >= spawnRate)
			{
				timeSinceLastSpawned = 0f;

				//Set a random y position for the column
				float spawnYPosition = Random.Range(columnMin, columnMax);

				//...then set the current column to that position.
				columns[currentColumn].transform.position = new Vector2(spawnXPosition, spawnYPosition);

				//Increase the value of currentColumn. If the new size is too big, set it back to zero
				currentColumn++;

				if (currentColumn >= columnPoolSize)
					currentColumn = 0;
			}
			
			//In the Unity editor, we can use a power-up also by right-clicking.
			if (Input.GetMouseButtonDown(1) && GameControl.instance.bombCount > 0)
				BombActivated();
		}
	}

	public void BombActivated()
	{
		if (!GameControl.instance.gameOver)
		{
			//Reduces one from the number of bombs...
			GameControl.instance.bombCount--;
			//...but keep the count at zero at the least
			if (GameControl.instance.bombCount < 0)
				GameControl.instance.bombCount = 0;
			//Destroy the columns...
			DestroyObstacles();
			//... and re-initialise them again.
			InitializeColumns();
		}
	}
}