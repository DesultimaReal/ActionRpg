using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

public class WeaponPickup : MonoBehaviour {
	public int WeaponTypeUsed;
	public PlayerController thePC;
	public enum WeaponType
	{
		Bow,
		Boomerang,
		Rocket
	}
	public void Start()
	{
		thePC = FindObjectOfType<PlayerController>();
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			WeaponController.Instance.SetWeapon(WeaponTypeUsed);
			thePC.SetWeapon(WeaponTypeUsed);
			PoolManager.Pools["EnemyPool"].Despawn(this.transform);
		}
	}
}
