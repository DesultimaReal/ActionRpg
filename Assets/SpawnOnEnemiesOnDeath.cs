using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using UnityEngine;

public class SpawnOnEnemiesOnDeath : MonoBehaviour {
	// Use this for initialization
	EnemyHealthManager ourEHMan;
	public GameObject EnemyToSpawn;
	public int numberToSpawn;
	void Start () {
		ourEHMan = GetComponent<EnemyHealthManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if(ourEHMan.CurrentHealth <= 0)
		{
			for(int i = 0; i <numberToSpawn; i++)
			{
				Transform myInstance = PoolManager.Pools["SlimeSpawner"].Spawn(EnemyToSpawn.transform);
			}
		}
	}
}
