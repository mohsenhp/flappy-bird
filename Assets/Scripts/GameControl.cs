using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameControl : MonoBehaviour 
{
	public static GameControl instance;			//A reference to our game control script so we can access it statically.
	public Text scoreText;						//A reference to the UI text component that displays the player's score.
	public GameObject gameOvertext;             //A reference to the object that displays the text which appears when the player dies.
	public GameObject birdObject;               //A reference to the bird.
	public GameObject startMenu;                //A reference to the object that displays the start menu.


	private int score = 0;                      //The player's score.
	public bool scoreChanged = false;           //Has the score changed? 
	public bool gameStarted = false;            //Has the game started?
	public bool gameOver = false;				//Is the game over?
	
	public float scrollSpeed;                   //How fast does the bird fly? (Make sure this coefficient is not set... 
												//... to zero or a positive number in the editor)
	
	public int speedIncreaseInterval;           //After how many columns should the speed increae?
	public float speedIncreaseCoefficient;      //To what extent should the speed increase? (Make sure this coefficient is not set... 
												//... to zero or a negative number in the editor)


	void Awake()
	{
		//If we don't currently have a game control...
		if (instance == null)
			//...set this one to be it...
			instance = this;
		//...otherwise...
		else if(instance != this)
			//...destroy this one because it is a duplicate.
			Destroy (gameObject);
	}

	//Called when the player taps on the 'PLAY' button in the main menu
	public void StartGame()
	{
		gameStarted = true;
		//The bird, which was hidden in the main menu, is shown again
		birdObject.SetActive(true);
		//The start menu goes away
		startMenu.SetActive(false);
	}

	void Update()
	{
			if (score > 0 && score % speedIncreaseInterval == 0 && scoreChanged == true)
			{
				scrollSpeed *= speedIncreaseCoefficient;
				scoreChanged = false;
			}

			//If the game is over and the player has pressed some input...
			if (gameOver && Input.GetMouseButtonDown(0))
			{
				//...reload the current scene.
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
	}

	public void BirdScored()
	{
		//The bird can't score if the game is over.
		if (gameOver)	
			return;
		//If the game is not over, increase the score...
		scoreChanged = true;
		score++;
		//...and adjust the score text.
		scoreText.text = "Score: " + score.ToString();
	}

	public void BirdDied()
	{
		//Activate the game over text.
		gameOvertext.SetActive (true);
		//Set the game to be over.
		gameOver = true;
	}


	//Called when the player taps on the 'QUIT' button in the start menu
	public void QuitGame()
	{
		Debug.Log("Quitting!");
		Application.Quit();
	}
}
