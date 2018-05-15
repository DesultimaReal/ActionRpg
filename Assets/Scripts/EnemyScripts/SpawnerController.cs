using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using UnityEngine;

public class SpawnerController : MonoBehaviour {

	public float maxSpawnTime;
	public float currentSpawnTime;
	public GameObject EnemyToSpawn;
	public float ActiveChildren;
	public ParticleSystem Eruption;
	public float xMod;

	private Vector3 V;
	public float speed;
	public float amount;
	public float MaxShakes;
	public float ShakeCounter;
					   // Use this for initialization
	void Start()
	{
		speed = 100.0f;
		amount = 0.1f;
		currentSpawnTime = maxSpawnTime;
		//ShakeCounter = MaxShakes;
	}

	// Update is called once per frame
	void Update()
	{
		currentSpawnTime -= Time.deltaTime;
		if (currentSpawnTime <= 0)
		{
			currentSpawnTime = maxSpawnTime;
			if (GetComponentsInChildren<Transform>().GetLength(0) <= 3)
			{
				// Spawn an instance (with same argument options as Instantiate())
				//Set its parent and put it in our EnemyPool for cloning
				if(Random.Range(0.1f,0.5f) > 0.2f)
				{
					Erupt();
				}
				SpawnEnemy();
			}
		}
	}
	void Erupt()
	{
		//Spawn Eruption particles
		Transform ParticleBurst = PoolManager.Pools["CoinTextPool"].Spawn(Eruption.transform, new Vector3(transform.position.x, transform.position.y + 1.1f, transform.position.z), transform.rotation);
	}
	void SpawnEnemy()
	{
		//Wait for anim to end before spawning lol
		Transform myInstance = PoolManager.Pools["EnemyPool"].Spawn(EnemyToSpawn.transform, new Vector3(transform.position.x, transform.position.y - 2, transform.position.z), transform.rotation);
		myInstance.parent = transform;
	}
}
