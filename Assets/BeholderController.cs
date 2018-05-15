using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using UnityEngine;

public class BeholderController : SeeingEnemy {
	public GameObject Eye;
	public bool Attacking;
	public bool Active;
	Transform myInstance;
	// Use this for initialization
	void Start () {
		Attacking = false;
		//Spawn our Eye in the appropriate point
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
		if (seesPlayer && !Attacking)
		{
			Active = true;
			Attacking = true;
			myInstance = PoolManager.Pools["EnemyPool"].Spawn(Eye.transform, new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z + 0.5f), transform.rotation);
			myInstance.parent = transform;
		}
		if (!seesPlayer)
		{
			Attacking = false;
			if (Active)
			{
				Active = !Active;
				myInstance.parent = null;
				PoolManager.Pools["EnemyPool"].Despawn(myInstance);
			}
		}
	}
}
