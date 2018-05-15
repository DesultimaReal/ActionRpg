using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour {
	public float lifespan;
	// Use this for initialization
	void OnSpawned () {
		// Start the timer as soon as this instance is spawned.
		this.StartCoroutine(this.TimedDespawn());
	}
	private IEnumerator TimedDespawn()
	{
		// Wait for 'lifespan' (seconds) then despawn this instance.
		yield return new WaitForSeconds(this.lifespan);
		PoolManager.Pools["CoinTextPool"].Despawn(this.transform);
	}
}
