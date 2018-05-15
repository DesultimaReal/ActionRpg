using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using UnityEngine;

public class DestroyOverTimeEnemy : MonoBehaviour {

	private float timeToDestroy;
	public float maxTime;

	// Use this for initialization
	void OnSpawned () {
		timeToDestroy = maxTime;
	}
	
	// Update is called once per frame
	void Update () {
		timeToDestroy -= Time.deltaTime;
		if(timeToDestroy <= 0)
		{
			PoolManager.Pools["EnemyPool"].Despawn(gameObject.transform);//Get rid of the object
		}
	}
}
