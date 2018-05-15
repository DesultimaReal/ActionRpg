using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {
	public int damageToGive;
	private int currentDamage;
	public GameObject damageNumber;
	private PlayerStats thePS;
	private GameObject thePlayer;
	public int Magnitude = 1000;

	// Use this for initialization
	void Start () {
		thePS = FindObjectOfType<PlayerStats>();
		thePlayer = GameObject.FindGameObjectWithTag("Player");
	}
	private void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.name == "Player")
		{
			//Calculate damage to give the player
			currentDamage = damageToGive - thePS.currentDefence;
			if(currentDamage <= 0)
			{
				currentDamage = 1;
			}

			//Apply that damage to the players health in their stats
			other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(currentDamage);

			//Make a damage number that floats upwards of that Damage
			Transform myInstance = PoolManager.Pools["CoinTextPool"].Spawn(damageNumber.transform, transform.position, Quaternion.Euler(Vector3.zero));
			myInstance.gameObject.GetComponent<FloatingNumbers>().damageNumber = currentDamage;
			myInstance.position = new Vector2(transform.position.x, transform.position.y);

			//Knock the player in a direction. Sometimes?
			var Direction = thePlayer.transform.position - this.transform.position;
			Direction.Normalize();
			thePlayer.GetComponent<Rigidbody2D>().AddForce(Direction * Magnitude);
		}
	}

}
