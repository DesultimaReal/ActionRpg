using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;
public class ChickenHealthController : NaturalHealthManager {
	bool Running;
	public GameObject ArcingCoin;
	public Vector3 PlayerLocation;
	private Rigidbody2D rb;
	public float speed = 5f;
	public float maxRunningTime;
	public float runningTime;
	public bool isRunning;
	public override void HurtEnemy(int damageToGive)
	{
		base.HurtEnemy(damageToGive);
		Transform myInstance = PoolManager.Pools["CoinTextPool"].Spawn(ArcingCoin.transform, transform.position, transform.rotation);
		int WhichSound = Random.Range(0, 4);
		Debug.Log(WhichSound);
		SFXManager.Instance.ChickenSounds[WhichSound].Play();
		isRunning = true;

	}
	// Use this for initialization
	void Start()
	{
		
		rb = GetComponent<Rigidbody2D>();
		isRunning = false;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		PlayerLocation = PlayerTracker.Instance.PlayerLocation;
		if (isRunning)
		{
			Debug.Log("Running!");
			runningTime -= Time.deltaTime;
			if (runningTime <= 0)
			{
				runningTime = maxRunningTime;
				isRunning = false;
			}
			Vector2 direction = transform.position - PlayerLocation;
			transform.Translate(direction.normalized * speed * Time.deltaTime);
		}
	}
}

