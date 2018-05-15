using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using UnityEngine;

public class GreenSlimeController : SeeingEnemy {
	Vector3 playerPos;
	Vector3 ourPos;
	private EnemyHealthManager myHealthMan;

	public float moveSpeed;
	private Rigidbody2D myRigidbody;
	private bool moving;

	public float timeBetweenMove;
	private float timeBetweenMoveCounter;//Make private
	public float timeToMove;
	private float timeToMoveCounter;

	private Vector3 moveDirection;
	private PlayerController thePlayer;

	public GameObject GreenFireball;
	private SFXManager sfxMan;
	private bool Healthy;
	// Use this for initialization

	void Start () {
		myRigidbody = GetComponent<Rigidbody2D>();
		myHealthMan = GetComponent<EnemyHealthManager>();
		timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
		timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeBetweenMove * 1.25f);
		thePlayer = FindObjectOfType<PlayerController>();
		sfxMan = FindObjectOfType<SFXManager>();
		Healthy = true;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();
		if (Healthy)
		{
			if (myHealthMan.CurrentHealth < myHealthMan.MaxHealth * .5)
			{
				moveSpeed = moveSpeed * 1.1f;
				timeBetweenMove = 0.5f; 
				Healthy = false;
			}
		}
		
		if (moving)
		{
			timeToMoveCounter -= Time.deltaTime;
			myRigidbody.velocity = moveDirection;

			if(timeToMoveCounter < 0f)
			{
				moving = false;
				if (seesPlayer)
				{
					GameObject projectile = spawnAtPlayerFace(GreenFireball, 0.5f);
					shootAtPlayer(projectile, 1, playerPos);//Use force to fire it at our player, at a force multiplier of 1
					sfxMan.FireballSpit.Play();
				}
				timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
			}
		} else {
			timeBetweenMoveCounter -= Time.deltaTime;
			myRigidbody.velocity = Vector2.zero;
			if(timeBetweenMoveCounter < 0f)
			{
				moving = true;
				//timeToMoveCounter = timeToMove;
				timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeBetweenMove * 1.25f);
				moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed,0f);
				if(Random.Range(0,10) > 4)
				{
					moveDirection = Vector3.Scale((PlayerTracker.Instance.PlayerLocation - transform.position), new Vector3(0.5f,0.5f,0.5f));
					//Debug.Log("Headed somewhere!");
				}
			}
		}

	}
	public GameObject spawnAtPlayerFace(GameObject ToSpawn, float distanceFromUs)
	{
		ourPos = transform.position;
		playerPos = thePlayer.transform.position;

		//Calculate a point between us and the player that is distanceFromUs away
		Vector3 PointBetween = LerpByDistance(ourPos, playerPos, distanceFromUs);
		//Create the object and assign it as a child
		Transform myInstance = PoolManager.Pools["CoinTextPool"].Spawn(ToSpawn.transform, PointBetween, Quaternion.identity);
		myInstance.parent = transform;

		GameObject spawn = myInstance.gameObject;
		return spawn;
	}
	public Vector3 LerpByDistance(Vector3 A, Vector3 B, float x)
	{
		Vector3 P = x * Vector3.Normalize(B - A) + A;
		return P;
	}
	public void shootAtPlayer(GameObject projectile, int forceMultiplier, Vector3 playerPos)
	{
		//Get our projectiles body and the player position
		Rigidbody2D projectileBody = projectile.GetComponent<Rigidbody2D>();

		//Add force to the fireball in the players direction
		
		var Direction = playerPos - this.transform.position;
		projectileBody.AddForce(Direction * forceMultiplier, ForceMode2D.Impulse);
	}
}
