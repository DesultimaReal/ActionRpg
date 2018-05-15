using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {
	public string StartingScene;
	public string StartPointName;
	public PlayerController thePC;
	public List<int> Scores;
	public Text ScoreText;
	GameManager theGM;

	public void RenderScores()
	{
		Debug.Log("Rendering Scores");
		Scores = ScoreManager.globalScoreManager.ScoreList;
		string Original = "TopScores\n";
		foreach (int score in Scores)
		{
			Original =  Original + (score).ToString() + "\n";
		}
		Debug.Log(Original);
		ScoreText.text = Original;
	}

	public void Awake()
	{
		thePC = FindObjectOfType<PlayerController>();
		theGM = FindObjectOfType<GameManager>();
		theGM.theGOM = this;
		//gameObject.SetActive(false);
	}
	public void Quit()
	{
		Debug.Log("QUITTING OUT");

		Application.Quit();
	}
	public void Retry()
	{
		Debug.Log("Hitting Retry");
		Time.timeScale = 1.0f;
		theGM.CurrentGameTimeSeconds = theGM.TotalGameTime;
		
		gameObject.SetActive(false);
		//thePC.startPoint = StartPointName;
		//thePC.canMove = true;
		Debug.Log("Resetting Scene");
		SceneManager.LoadScene(StartingScene);

		Debug.Log("Scene has been reset.");
		
	}

}
