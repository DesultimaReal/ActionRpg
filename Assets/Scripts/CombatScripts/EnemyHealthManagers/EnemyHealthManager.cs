using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour {
	public GameObject ItemDrop;

	public GameObject EnemyToSpawnOnDeath;
	public int numToSpawnOnDeath;

	public int MaxHealth;
	public int CurrentHealth;

	protected PlayerStats thePlayerStats;
	public int expToGive;
	public string enemyQuestName;

	protected QuestManager theQM;
	protected MoneyManager theMM;

	public float MoneyDrop;
	public GameObject Coin;
	public List<Vector2> spawnOffsets;//Why are all my coins spawning on top of each other
	public List<Vector2> deathSpawnOffsets;
	protected Vector2 v;
	public float moneySpawnRange;
	protected Vector3 SpawnLocation;

	protected Quaternion SaveR;
	protected Vector3 SaveT;
	// When we die how many of something do we spawn
	public int MaxNumToSpawn;
	public GameObject DeathSludge;

	void Update()
	{
		if (CurrentHealth <= 0)
		{
			OnDeath();
		}
	}

	public virtual Vector2 generateVectorInHitbox()
	{
		//Add something to our list
		float xOffset = Random.Range(0.1f, moneySpawnRange);
		float yOffset = Random.Range(0.1f, moneySpawnRange);
		v = new Vector2(xOffset, yOffset);
		return v;
	}

	public virtual void OnDeath()
	{
		SaveT = transform.position;
		SaveR = transform.rotation;
		theQM.enemyKilled = enemyQuestName;

		thePlayerStats.AddExperience(expToGive);
		if (ItemDrop)
		{
			PoolManager.Pools["EnemyPool"].Spawn(ItemDrop.transform, transform.position, transform.rotation);
		}
		for (int i = 0; i < MoneyDrop; i++)
		{
			//Spawn Money in unused location near death
			Transform myInstance = PoolManager.Pools["CoinTextPool"].Spawn(Coin.transform, new Vector3(SaveT.x + spawnOffsets[i].x, SaveT.y + spawnOffsets[i].y, SaveT.z), SaveR);
		}

		if (EnemyToSpawnOnDeath != null)
		{
			Invoke("SpawnThings", 5);
		}
		if(DeathSludge != null)
		{
			PoolManager.Pools["EnemyPool"].Spawn
			(DeathSludge.transform,
			new Vector3(SaveT.x,SaveT.y,SaveT.z),
			SaveR);
		}
		
	}

	public virtual void SpawnThings()
	{
		float SpawnOffset = Random.Range(0.5f, -0.5f);//Generate an offset from our transform and set it as a location to spawn stuff
		SpawnLocation = new Vector3(SaveT.x + SpawnOffset, SaveT.y + SpawnOffset, SaveT.z);

		while (numToSpawnOnDeath > 0)
		{
			//If there is nohing at overlap circle it will return null
			if (Physics2D.OverlapCircle(SpawnLocation, 0.36f, 0) == null)
			{
				numToSpawnOnDeath--;
				PoolManager.Pools["EnemyPool"].Spawn
			(EnemyToSpawnOnDeath.transform,
			SpawnLocation,
			SaveR);
			}
			else
			{
				SpawnOffset = Random.Range(0.5f, -0.5f);
			}
		}
	}

	public virtual void HurtEnemy(int damageToGive)
	{
		CurrentHealth -= damageToGive;
	}

	public virtual void SetMaxHealth()
	{
		CurrentHealth = MaxHealth;
	}
}
