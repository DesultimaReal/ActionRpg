using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {
	public Slider healthBar;
	public Slider energyBar;

	public Text hpText;
	private PlayerHealthManager PlayerHealth;

	private GameManager theGM;
	private PlayerStats thePS;
	private ScoreManager theSM;
	public Text levelText;
	public Text Timer;
	public Text Score;

	private static bool uiExists;
	// Use this for initialization
	void Awake () {
		//SingleTon Design that will ensure there will always be one thing with this script 
		//and that it won't die between plays
		/*if (!uiExists)
		{
			uiExists = true;
			DontDestroyOnLoad(transform.gameObject);///Dont destroy our player
		}
		else
		{
			Destroy(gameObject);
		}*/
		theGM = GetComponent<GameManager>();
		thePS = GetComponent<PlayerStats>();
		theSM = FindObjectOfType<ScoreManager>();
		PlayerHealth = FindObjectOfType<PlayerHealthManager>();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(PlayerHealth.playerCurrentHealth);
		healthBar.maxValue = PlayerHealth.playerMaxHealth;
		healthBar.value = PlayerHealth.playerCurrentHealth;
		//Debug.Log(PlayerHealth.playerCurrentHealth + "In UI");
		hpText.text = "HP: " + PlayerHealth.playerCurrentHealth + "/" + PlayerHealth.playerMaxHealth;
		levelText.text = "Lvl: " + (thePS.currentLevel + 1);
		Timer.text = "" + (theGM.CurrentGameTimeSeconds);
		Score.text = "SCORE: " + (theSM.Score) + " CHAIN: " + theSM.ChainAmount;

		energyBar.maxValue = PlayerHealth.playerMaxEnergy;
		energyBar.value = PlayerHealth.playerCurrentEnergy;
	}
}
