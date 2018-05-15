using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using UnityEngine;

public class MagiController : MonoBehaviour {
	public enum Face
	{
		Up, Down, Left, Right
	}
	//Variables for firing our projectiles
	private Vector3 playerPos;
	private Vector3 ourPos;
	private PlayerController thePlayer;
	public GameObject Projectile;
	public int NumToShoot;
	public int TimeBetweenProjectiles;
	//Variables to control movement
	Face DirectionFacing;
	public float TimeUntilNextMove;
	public float MoveInterval;
	public Vector3 Dir;
	public float moveSpeed;
	private Rigidbody2D myRigidbody;
	//Variables to control which sprite, simple psuedo animation
	public Sprite SprDown;
	public Sprite SprUp;
	public Sprite SprRight;
	public Sprite SprLeft;
	public SpriteRenderer theSR;

	public bool floating;
	public float floatspeed;

	Transform myTrans;
	//how long to keep moving up or down until switching direction
	public float verticalTime = 1f;
	//how fast to move vertically
	public float verticalSpeed = 5f;
	//how fast to move forward

	public Sight ourSight;
	public float distanceToWall;
	public void Start()
	{
		thePlayer = FindObjectOfType<PlayerController>();
		myRigidbody = GetComponent<Rigidbody2D>();
		theSR = GetComponent<SpriteRenderer>();
		ourSight = GetComponent<Sight>();
		StartCoroutine(FixedShoot());
		TimeUntilNextMove = MoveInterval;
		//floatTimer = maxFloatTimer;

		myTrans = this.transform;
		StartCoroutine(Rise());
		Dir = -myTrans.up;
	}

	private void FixedUpdate()
	{
		TimeUntilNextMove -= Time.deltaTime;
		if (TimeUntilNextMove <= 0)
		{
			TimeUntilNextMove = MoveInterval;
			if (distanceToWall < 2)
			{
				Dir = Dir * -1;
				if(theSR.sprite == SprUp)
				{
					theSR.sprite = SprDown;
				}
				else if (theSR.sprite == SprDown)
				{
					theSR.sprite = SprUp;
				}
				else if (theSR.sprite == SprRight)
				{
					theSR.sprite = SprLeft;
				}
				else 
				{
					theSR.sprite = SprRight;
				}
			}
			else
			{
				DirectionFacing = (Face)Random.Range(0, 3);
				switch (DirectionFacing)
				{
					case Face.Up:
						Dir = myTrans.up;
						theSR.sprite = SprUp;
						break;
					case Face.Left:
						Dir = -myTrans.right;
						theSR.sprite = SprLeft;
						break;
					case Face.Right:
						Dir = myTrans.right;
						theSR.sprite = SprRight;
						break;
					case Face.Down:
						Dir = -myTrans.up;
						theSR.sprite = SprDown;
						break;
				}
			}
			
			//ourSight.SwitchDir();
		}
		Move();
	}
	
	void Move()
	{
		myTrans.Translate(Dir * moveSpeed * Time.deltaTime);
	}

	IEnumerator Rise()
	{
		float t = verticalTime;
		while (t > 0f)
		{
			myTrans.Translate(myTrans.up * verticalSpeed * Time.deltaTime);
			t -= Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		StartCoroutine(Fall());
	}

	IEnumerator Fall()
	{
		float t = verticalTime;
		while (t > 0f)
		{
			myTrans.Translate(-myTrans.up * verticalSpeed * Time.deltaTime);
			t -= Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		StartCoroutine(Rise());
	}

	//Shooter Code. Should make this a class if reused enough for inheritence
	public IEnumerator FixedShoot()
	{
		for(int i = 0; i < NumToShoot; i++)
		{
			spawnAtPlayerFace(Projectile, 1);
			yield return new WaitForSeconds(TimeBetweenProjectiles);
		}
	}

	public GameObject spawnAtPlayerFace(GameObject ToSpawn, float distanceFromUs)
	{
		ourPos = transform.position;
		playerPos = thePlayer.transform.position;

		//Calculate a point between us and the player that is distanceFromUs away
		Vector3 PointBetween = LerpByDistance(ourPos, playerPos, distanceFromUs);
		//Create the object and assign it as a child
		Transform myInstance = PoolManager.Pools["EnemyPool"].Spawn(ToSpawn.transform, PointBetween, Quaternion.identity);
		Vector3 Target = PlayerTracker.Instance.PlayerLocation;
		myInstance.gameObject.GetComponent<BoomerangController>().TargetPosition = Target;
		myInstance.parent = transform;

		GameObject spawn = myInstance.gameObject;
		return spawn;
	}

	public Vector3 LerpByDistance(Vector3 A, Vector3 B, float x)
	{
		Vector3 P = x * Vector3.Normalize(B - A) + A;
		return P;
	}}
