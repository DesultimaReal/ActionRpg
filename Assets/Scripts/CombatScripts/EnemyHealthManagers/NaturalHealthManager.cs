using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using UnityEngine;

public class NaturalHealthManager : EnemyHealthManager
{
	public Controller ourController;
	public int PointWorth;
	public bool isShatter = false;
	public SFXManager sfxMan;
	public bool hasLimbs;
	private void Awake()
	{
		Debug.Log("HealthManager");
		ourController = GetComponent<Controller>();
		CurrentHealth = MaxHealth;
		numToSpawnOnDeath = MaxNumToSpawn;
		theMM = FindObjectOfType<MoneyManager>();
		thePlayerStats = FindObjectOfType<PlayerStats>();
		theQM = FindObjectOfType<QuestManager>();
		sfxMan = FindObjectOfType<SFXManager>();
		
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
	}

	void Update()
	{
		if (CurrentHealth <= 0)
		{
			if (isShatter)
			{
				sfxMan.TargetDestroy.Play();
			}
			
			base.OnDeath();
			ScoreManager.globalScoreManager.AddToScore(PointWorth);
			if (hasLimbs)//We want to despawn the children with it
			{
				foreach(Transform child in transform)
				{
					if (transform.parent == this.transform)
					{
						if (child.gameObject.GetComponent<SPAWNEDHealthManager>())
						{//If our Children have health, they might have already been despawned
							SPAWNEDHealthManager S = child.gameObject.GetComponent<SPAWNEDHealthManager>();
							if (S.Exists == true)
							{
								S.Exists = false;
								PoolManager.Pools["EnemyPool"].Despawn(child);
							}
						}
						else
						{
							PoolManager.Pools["EnemyPool"].Despawn(child);
						}
					}
				}
			}
			else//We have projectile children we want to live beyond our death
			{
				transform.DetachChildren();
			}

			Destroy(gameObject);
		}
	}
}
