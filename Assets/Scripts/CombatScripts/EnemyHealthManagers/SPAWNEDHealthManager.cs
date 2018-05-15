using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using UnityEngine;

public class SPAWNEDHealthManager : EnemyHealthManager {
	public int PointWorth;
	public bool Exists;
	private void OnSpawned()
	{
		CurrentHealth = MaxHealth;
		numToSpawnOnDeath = MaxNumToSpawn;
		theMM = FindObjectOfType<MoneyManager>();
		thePlayerStats = FindObjectOfType<PlayerStats>();
		theQM = FindObjectOfType<QuestManager>();
		MoneyDrop = Random.Range(1.0f, MoneyDrop);
		int NumOffsets = 0;
		v = generateVectorInHitbox();
		spawnOffsets.Add(v);
		NumOffsets++;
		while (NumOffsets < MoneyDrop)
		{
			v = generateVectorInHitbox();
			if (!spawnOffsets.Contains(v))
			{
				NumOffsets++;
				spawnOffsets.Add(v);
			}
		}
		Exists = true;
	}

	void Update()
	{
		if (CurrentHealth <= 0)
		{
			ScoreManager.globalScoreManager.AddToScore(PointWorth);
			OnDeath();
			if (Exists)
			{
				PoolManager.Pools["EnemyPool"].Despawn(gameObject.transform);
				Exists = false;
			}
		}
	}
}
