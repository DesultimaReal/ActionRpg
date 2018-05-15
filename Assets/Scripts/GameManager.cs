using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
	//Singleton//
	public static GameManager Instance { get; private set; }
	
	public float TotalGameTime;
	public float CurrentGameTimeSeconds;
	public bool playerDead;
	public bool gameOver;
	
	public string GameOverScene;
	public GameObject GameOverCanvas;

	public PlayerController thePC;

	public GameOverManager theGOM;
	
	// Use this for initialization

	private void Awake()
	{
		thePC = FindObjectOfType<PlayerController>();
	}



	private void Start()
	{
		theGOM = FindObjectOfType<GameOverManager>();

		CurrentGameTimeSeconds = TotalGameTime;

		//For managing if the game is over
		playerDead = false;
		gameOver = false;
	}

	// Update is called once per frame
	void Update()
	{
		CurrentGameTimeSeconds -= Time.deltaTime;
		CurrentGameTimeSeconds = Mathf.Round(CurrentGameTimeSeconds * 100f) / 100f;
		if (Input.GetKeyDown(KeyCode.M))
		{//Save our score
			playerDead = true;
		}
		if (Input.GetKeyDown(KeyCode.R))
		{//Murder our scores
			PlayerPrefs.DeleteAll();
		}
		if (!gameOver && (CurrentGameTimeSeconds <= 0 || playerDead))
		{
			playerDead = false;
			gameOver = true;
			endGame();
		}
	}
	void endGame()
	{
		Debug.Log("ending game");
		ScoreManager.globalScoreManager.AddScore();
		ScoreManager.globalScoreManager.saveScores();
		Debug.Log("Setting GameOverScreen true");
		GameOverCanvas.SetActive(true);
		theGOM.RenderScores();
		thePC.canMove = false;
		Time.timeScale = 0;
	}

	public void DestroyAllGameObjects()
	{
		GameObject[] GameObjects = (FindObjectsOfType<GameObject>() as GameObject[]);

		for (int i = 0; i < GameObjects.Length; i++)
		{
			Destroy(GameObjects[i]);
		}
	}

}

