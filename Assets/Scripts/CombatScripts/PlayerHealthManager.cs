using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour {
	public int playerMaxHealth;
	public int playerCurrentHealth;

	//To manage our energy
	public int playerMaxEnergy;
	public float playerCurrentEnergy;
	public float energyRegenRate = 5.0f;
	
	//For making hurt sound fx
	private SFXManager sfxMan;

	//For flashing hurt frames
	private bool flashActive;
	public float flashLength;
	private float flashCounter;
	//For when we die we make a reaper
	public GameObject Reaper;
	private SpriteRenderer playerSprite;
	//For healing ourselves
	public float PercentToRegen;
	public float OnePercentOfHealth;
	public GameManager theGM;
	// Use this for initialization
	void Awake () {
		//Debug.Log("Resetting health and energy");
		sfxMan = FindObjectOfType<SFXManager>();
		playerCurrentHealth = playerMaxHealth;
		//Debug.Log("CH " + playerCurrentHealth + " " + playerMaxHealth);
		playerCurrentEnergy = playerMaxEnergy;
		playerSprite = GetComponent<SpriteRenderer>();
		theGM = FindObjectOfType<GameManager>();
		OnePercentOfHealth = (playerMaxHealth / 100) + 1;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(playerCurrentHealth + "In HM");
		if(playerCurrentHealth <= 0)
		{
			theGM.playerDead = true;
			sfxMan.playerDead.Play();
			Transform myInstance = PoolManager.Pools["CoinTextPool"].Spawn(Reaper.transform, transform.position, transform.rotation);
			gameObject.SetActive(false);
			//GameManager.Instance.playerDead = true;
		}
		if(PercentToRegen > 0)
		{
			PercentToRegen--;
			if(playerCurrentHealth < playerMaxHealth)
			{
				playerCurrentHealth += (int)(OnePercentOfHealth);
				if(playerCurrentHealth > playerMaxHealth)
				{
					playerCurrentHealth = playerMaxHealth;
				}
			}
		}
		if(playerCurrentEnergy < playerMaxEnergy)
		{
			playerCurrentEnergy += energyRegenRate * Time.deltaTime;
		}
		if (flashActive)
		{
			if(flashCounter > flashLength * .66f)
			{
				playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
			}else if(flashCounter > flashLength * .33f)
			{
				playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
			}else if(flashCounter > 0)
			{
				playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
			}else{
				playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
				flashActive = false;
			}
		} flashCounter -= Time.deltaTime;
	}
	public void HurtPlayer(int damageToGive)
	{
		playerCurrentHealth -= damageToGive;

		flashActive = true;
		flashCounter = flashLength;

		sfxMan.playerHurt.Play();
	}
	public bool SpendEnergy(int energyToSpend)
	{
		if(energyToSpend < playerCurrentEnergy)
		{
			playerCurrentEnergy -= energyToSpend;
			return true;
		}
		return false;
	}
	public void SetMaxHealth()
	{
		playerCurrentHealth = playerMaxHealth;
	}
	public void Regen(float PercentAmount)
	{
		PercentToRegen = PercentAmount;
	}
}
