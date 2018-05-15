using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
	public int currentLevel;
	public int currentExp;
	public int[] toLevelUp;

	public int[] HPLevels;
	public int[] attackLevels;
	public int[] defenceLevels;

	public int currentHP;
	public int currentAttack;
	public int currentDefence;

	private PlayerHealthManager thePlayerHealth;
	// Use this for initialization
	void Awake () {
		//Debug.Log("SettingUpPlayerStats");
		currentHP = HPLevels[0];
		currentAttack = attackLevels[0];
		currentDefence = defenceLevels[0];
		thePlayerHealth = FindObjectOfType<PlayerHealthManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if(currentExp >= toLevelUp[currentLevel])
		{
			LevelUp();
		}
	}
	public void AddExperience(int expToAdd)
	{
		currentExp += expToAdd;
	}
	public void LevelUp()
	{
		currentLevel++;
		currentHP = HPLevels[currentLevel];
		thePlayerHealth.playerMaxHealth = currentHP;
		thePlayerHealth.playerCurrentHealth += currentHP - HPLevels[currentLevel - 1];
		thePlayerHealth.OnePercentOfHealth = thePlayerHealth.playerMaxHealth / 100;
		currentAttack = attackLevels[currentLevel];
		currentDefence = defenceLevels[currentLevel];
	}
}
