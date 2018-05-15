using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour {
	public Text moneyText;
	public int currentGold;
	public SFXManager sfxMan;
	// Use this for initialization
	void Start () {
		PlayerPrefs.DeleteKey("CurrentMoney");//Makes it so our money deletes itself between plays.
		//Remove this if we want our money to Stay between plays.
		if (PlayerPrefs.HasKey("CurrentMoney"))
		{
			currentGold = PlayerPrefs.GetInt("CurrentMoney");
		}
		else
		{
			currentGold = 0;
			PlayerPrefs.SetInt("CurrentMoney", 0);
		}
		moneyText.text = "Gold: " + currentGold;
		sfxMan = FindObjectOfType<SFXManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void AddMoney(int goldToAdd)
	{
		for (int i = 0; i < goldToAdd; i++)
		{
			sfxMan.CollectCoin.Play();
		}
		currentGold += goldToAdd;
		PlayerPrefs.SetInt("CurrentMoney", currentGold);
		moneyText.text = "Gold: " + currentGold;
	}
}
