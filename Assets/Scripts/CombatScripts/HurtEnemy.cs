using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using UnityEngine;

public class HurtEnemy : MonoBehaviour {
	public int damageToGive;
	private int currentDamage;

	public GameObject damageBurst;
	public Transform hitPoint;
	public GameObject damageNumber;
	private PlayerStats thePS;
	// Use this for initialization
	void Start () {
		thePS = FindObjectOfType<PlayerStats>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			//Calculate our PlayersDamage to the enemy, then hurt them.
			currentDamage = damageToGive + thePS.currentAttack;
			other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(currentDamage);
			//Create a our defined damageBurst
			if(damageBurst != null)
			{
				Transform ParticleBurst = PoolManager.Pools["CoinTextPool"].Spawn(damageBurst.transform, hitPoint.position, hitPoint.rotation); 
			}

			//USE OUR SPAWNPOOL TO CREATE A DAMAGE NUMBER
			Transform myInstance = PoolManager.Pools["CoinTextPool"].Spawn(damageNumber.transform, transform.position, Quaternion.Euler(Vector3.zero));
			myInstance.gameObject.GetComponent<FloatingNumbers>().damageNumber = currentDamage;
			myInstance.position = new Vector2(transform.position.x, transform.position.y);
		}
	}
}
