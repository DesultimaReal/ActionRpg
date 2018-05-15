using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScoreManager : MonoBehaviour { 
  public static ScoreManager globalScoreManager;

	//Singleton//
	//public static ScoreManager Instance { get; private set; }
	public int Score;
	public string SaveScore = "Score";
	public bool hasScoresSaved;
	public int NumberWinners = 3;
	public List<int> ScoreList;


	//For chaining for more points
	public float ChainTimer;
	public float MaxChainTimer;
	public int ChainAmount = 1;

	public void AddToScore(int Amount)
	{
		Score += Amount * ChainAmount;
		//GameManager.Instance.CurrentGameTimeSeconds += 1;
		AddToChain();
	}
	private void Awake()
	{
		globalScoreManager = this;
	}
	// Use this for initialization
	void Start () {
		ScoreList = new List<int>(NumberWinners);
		InitIntList(NumberWinners, ScoreList);
		if (PlayerPrefs.HasKey(SaveScore + 0))//We have some saved scores
		{
			loadScores();
		}
	}
	public static void InitIntList(int ListSize, List<int> Listname)
	{
		//Debug.Log(ListSize);
		for (int i = 0; i < ListSize; i++)
		{
			Listname.Add(0);
		}
	}
	public void saveScores()
	{
		for (int i = 0; i < NumberWinners; i++)
		{
			PlayerPrefs.SetInt(SaveScore + i, ScoreList[i]);
		}
	}

	public List<int> loadScores()
	{
		for (int i = 0; i < NumberWinners; i++)
		{
			//Debug.Log(i);
			ScoreList[i] = PlayerPrefs.GetInt(SaveScore + i);
		}
		return ScoreList;
	}
	public List<int> AddScore()
	{
		
		Debug.Log("Adding to highscores " + Score);
		//NumberWinners = 2
		if (Score > ScoreList[NumberWinners - 1])//Check ScoreList[2]
		{   //If our Score is greater than the worst score on this list
			ScoreList[NumberWinners - 1] = Score;//Make it the last Score
		}
		if (NumberWinners == 1)
		{
			return ScoreList;//At this point we are done as we only have one high score
		}//NumberWinners = 2
		for (int i = NumberWinners - 1; i > 0; i--)
		{   // i = 1//
			if (ScoreList[i] > ScoreList[i - 1])
			{
				swap(i, i - 1);
			}
		}
		//Debug.Log("After adding score"); PrintList(ScoreList);
		return ScoreList;
	}
	void AddToChain()
	{
		ChainAmount += 1;
		ChainTimer = MaxChainTimer;
	}
	private void Update()
	{
		ChainTimer -= Time.deltaTime;
		if(ChainTimer <= 0)
		{
			ChainAmount = 1;
		}
	}
	public void swap(int firstpos, int secondpos)
	{
		int temp = ScoreList[firstpos];
		ScoreList[firstpos] = ScoreList[secondpos];
		ScoreList[secondpos] = temp;
	}
	public static void PrintList<T>(List<T> list)
	{
		foreach (T Thing in list)
		{
			Debug.Log(Thing);
		}
	}
}
