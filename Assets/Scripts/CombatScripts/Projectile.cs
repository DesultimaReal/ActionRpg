using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using UnityEngine;

public class Projectile : MonoBehaviour {
	public ParticleSystem Explosion;
	public bool Exists;

	public float lifespan;

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.transform != transform.parent //If we are hitting something that isn't our parent
			&& other.transform.parent != transform.parent //And it isn't made by the same parent that made us
			&& Exists) //It hasn't been despawned yet
		{
			if (transform.parent.GetComponent<Dad>() != null)
			{ if (transform.parent.GetComponent<Dad>().MyDad == other.gameObject.name) Debug.Log("Hit!"); Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), this.GetComponent<Collider2D>()); }
			/*if(transform.parent.parent != null)
			{//our projectile has grandfathers
				if(other.transform == transform.parent.parent)
				{//If we are hitting our parents parent, I.E. our parent hosts a pool to hold us
					return; //We don't want to destroy ourselves or do anything
				}
			}*/
			if(Explosion != null)
			{
				Transform ParticleBurst = PoolManager.Pools["CoinTextPool"].Spawn(Explosion.transform, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
			}
			Exists = false;
			PoolManager.Pools["CoinTextPool"].Despawn(this.transform);//Get rid of the object
		}
	}
	void OnSpawned()
	{
		// Start the timer as soon as this instance is spawned.
		Exists = true;
		this.StartCoroutine(this.TimedDespawn());
	}
	private IEnumerator TimedDespawn()
	{
		// Wait for 'lifespan' (seconds) then despawn this instance.
		//Debug.Log("TimedDespawn");
		yield return new WaitForSeconds(this.lifespan);
		if (Exists)
		{
			Exists = false;
			PoolManager.Pools["CoinTextPool"].Despawn(this.transform);
		}
	}
}
