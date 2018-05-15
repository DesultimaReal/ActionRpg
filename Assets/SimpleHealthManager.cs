using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;
public class SimpleHealthManager : EnemyHealthManager
{
	public int PointWorth;
	// Use this for initialization
	void Start () {
		CurrentHealth = MaxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if(CurrentHealth <= 0)
		{
			PoolManager.Pools["CoinTextPool"].Despawn(this.transform);//Get rid of the object
		}
	}
}
