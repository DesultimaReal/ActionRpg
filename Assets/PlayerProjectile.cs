using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour {
	private bool Exists;
	public float lifespan;
	public int damageMult = 1;
	public int damageToGive;
	private int currentDamage;

	public GameObject damageNumber;
	private PlayerStats thePS;
	public bool DeathOnCollide = true;

	private void Start()
	{
		thePS = FindObjectOfType<PlayerStats>();
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (Exists && other.gameObject.tag != "Player" && other.transform.parent != transform.parent)//No matter what we collide with we want to despawn the arrow
		{
			if (DeathOnCollide)
			{
				if(other.gameObject.tag != "Pickup")
				{
					Debug.Log("Collided w/ " + other.gameObject.name + "with tag" + other.gameObject.tag);
					PoolManager.Pools["EnemyPool"].Despawn(this.transform);
					Exists = false;
				}
			}

			if (other.gameObject.tag == "Enemy")
			{
				//Calculate our PlayersDamage to the enemy, then hurt them.
				currentDamage = (damageToGive + thePS.currentAttack) * damageMult;
				other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(currentDamage);


				//USE OUR SPAWNPOOL TO CREATE A DAMAGE NUMBER
				Transform myInstance = PoolManager.Pools["CoinTextPool"].Spawn(damageNumber.transform, transform.position, Quaternion.Euler(Vector3.zero));
				myInstance.gameObject.GetComponent<FloatingNumbers>().damageNumber = currentDamage;
				myInstance.position = new Vector2(transform.position.x, transform.position.y);
			}

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
			PoolManager.Pools["EnemyPool"].Despawn(this.transform);
		}
	}
}
