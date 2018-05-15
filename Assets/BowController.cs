using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using UnityEngine;

public class BowController : MonoBehaviour {
	Vector3 playerPos;
	Vector3 ourPos;
	private SFXManager sfxMan;
	private PlayerController thePC;
	public GameObject Arrow;
	private Vector3 Dir;
	public int Force;
	// Use this for initialization
	void Awake () {
		sfxMan = FindObjectOfType<SFXManager>();
		thePC = FindObjectOfType<PlayerController>();
	}

	public void SpawnArrow()
	{
		GameObject projectile = spawnOnBow(Arrow, 0.5f);
		shootInBowDir(projectile, Force);//Use force to fire it at our player, at a force multiplier of 1
	}

	public GameObject spawnOnBow(GameObject ToSpawn, float distanceFromUs)
	{
		Vector3 PointBetween = transform.position;
		Vector3 dir = thePC.lastMove;
		//float zrot;
		

		float zrot = (Mathf.Abs(dir.x) * (dir.x * 90 + 90)) + (Mathf.Abs(dir.y) * (dir.y * 90 + 180)); 
		//Until I figure out how to convert a normalized Dir V3 to a z rot i'll use a custom formula
		/*if (dir.x == 1)
		{
			zrot = 180;
		}
		else if (dir.x == -1)
		{
			zrot = 0;
		}
		else if(dir.y == 1)
		{
			zrot = 270;
		}
		else
		{
			zrot = 90;
		}*/
		Transform myInstance = PoolManager.Pools["EnemyPool"].Spawn(ToSpawn.transform, PointBetween, Quaternion.Euler(0,0,zrot));
		GameObject spawn = myInstance.gameObject;
		return spawn;
	}
	public void shootInBowDir(GameObject projectile, int forceMultiplier)
	{
		//Get our projectiles body and the player position
		Rigidbody2D projectileBody = projectile.GetComponent<Rigidbody2D>();
		projectileBody.AddForce(thePC.lastMove * forceMultiplier, ForceMode2D.Impulse);
	}
}
