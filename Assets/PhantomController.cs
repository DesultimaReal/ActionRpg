using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;
public class PhantomController : Controller
{
	public enum State
	{
		Wandering,
		Phase1,
		Phase2
	}
	Vector3 TeleportLocation;

	public float BerserkTimer;
	public float MaxBerserkTimer;

	public State CurrentState;

	public float MaxTeleportDistance;
	public float MinTeleportDistance;

	private int layerMask;
	private bool Attacking;
	Vector3[] DirArray = new[] { new Vector3(0f, 1f, 0f), new Vector3(0f, -1f, 0f), new Vector3(1f, 0f, 0f), new Vector3(-1f, 0f, 0f) };
	private LineOfSight LOS;

	public int OriginalTimeBetweenThoughts;
	public int TimeBetweenThoughts;

	public ParticleSystem TeleportParticle;
	public GameObject ThingToShoot;
	public GameObject Rocket;

	private Vector3 Temp;

	public float rateOfFire = 180;
	public int burstSize = 3;
	
	private SinosoidalMotion SM;
	private Vector3 FaceDir;
	private Animator Anim;
	bool Berserking;
	public GameObject ProjectilePool;
	private Transform OurProjectilePool;
	private void Start()
	{
		LOS = GetComponent<LineOfSight>();
		Attacking = false;
		CurrentState = State.Wandering;
		layerMask = 1 << LayerMask.NameToLayer("SOLIDSTUFF") | ~(1 << LayerMask.NameToLayer("Player"));
		Anim = GetComponent<Animator>();
		FaceDir = new Vector3(0f, -1f, 0f);
		Berserking = false;
		TimeBetweenThoughts = OriginalTimeBetweenThoughts;
		OurProjectilePool = PoolManager.Pools["CoinTextPool"].Spawn(ProjectilePool.transform, transform.position, transform.rotation);
		OurProjectilePool.GetComponent<Dad>().MyDad = gameObject.name;
		//OurProjectilePool.parent = transform;
	}
	// Update is called once per frame
	void Update () {
		Anim.SetFloat("DirX",FaceDir.x);
		Anim.SetFloat("DirY", FaceDir.y);
		if (Berserking)
		{
			BerserkTimer-= Time.deltaTime;
			if (BerserkTimer <= 0)
			{
				EndBerserk();
			}
		}
		
		if ((LOS.hasLineOfSight || base.Aggro) && !Attacking)
		{
			Attacking = true;
			CurrentState = State.Phase1;
			StartCoroutine(TeleportAndAttack());
		}
	}


	Vector3[] ShuffleArray()
	{
		for (int i = 0; i < 4; i++)
		{
			int rand = Random.Range(0, 4);
			Temp = DirArray[rand];
			DirArray[rand] = DirArray[i];
			DirArray[i] = Temp;
		}
		return DirArray;
	}

	IEnumerator TeleportAndAttack()
	{
		while (Attacking)
		{
			//Run our teleporting code

			//Randomly shuffle our direction array
			DirArray = ShuffleArray();
			//Look in each direction using raycasting from the player for a place to teleport to
			foreach (Vector3 Dir in DirArray)
			{
				RaycastHit2D hit = Physics2D.Raycast(PlayerTracker.Instance.PlayerLocation, Dir, Mathf.Infinity, layerMask);
				if (hit.collider != null)
				{
					//Debug.Log(hit.transform.gameObject.name);
					//Debug.DrawLine (PlayerTracker.Instance.PlayerLocation, hit.point, Color.red, 7, false);
					//We know all of the points left right up and down from the player
					if(Vector3.Distance( PlayerTracker.Instance.PlayerLocation, hit.point) > MinTeleportDistance)
					{
						TeleportLocation = PlayerTracker.Instance.PlayerLocation + (Dir.normalized * Random.Range(MinTeleportDistance, MaxTeleportDistance));
						Teleport();
						yield return new WaitForSeconds(0.5f);
						FaceDir = -Dir;
					}
				}
			}

			//Run our attacking code
			float bulletDelay = 60f / rateOfFire;
			//Debug.Log(bulletDelay.ToString("F4") + " Between making bullets!");
			// rate of fire in weapons is in rounds per minute (RPM), therefore we should calculate how much time passes before firing a new round in the same burst.
			/*if (CurrentState == State.Phase1)
			{
				for (int i = 0; i < burstSize; i++)
				{
					MakeBullet();
					yield return new WaitForSeconds(bulletDelay); // wait till the next round
				}
			}
			else if (CurrentState == State.Phase2)
			{*/

				for (int i = 0; i < 3; i++)
				{

					MakeRocket(i);
					yield return new WaitForSeconds(bulletDelay / 10); // wait till the next round
				}
			//}

			CurrentState =  (State)Random.Range(1,3);
			yield return new WaitForSeconds(TimeBetweenThoughts);//This whole function will be run every TimeBetweenThoughts seconds

			if (Random.Range(0, 10) > 5)
			{
				Berserk();
			}
		}
	}

	public void Berserk()
	{
		TimeBetweenThoughts = 1;
		BerserkTimer = MaxBerserkTimer;
		Berserking = true;
	}

	void EndBerserk()
	{
		TimeBetweenThoughts = OriginalTimeBetweenThoughts;
		Berserking = false;
	}

	void Teleport()
	{
		//Spawn teleport sprite that plays sound
		Anim.SetBool("Teleport", true);//Trigger our animation for our bow
	}
	/*void Teleport()
	{
		Transform ParticleBurst = PoolManager.Pools["CoinTextPool"].Spawn(TeleportParticle.transform, transform.position, transform.rotation);
		transform.position = TeleportLocation;
	}*/

	void EndTeleport()
	{
		Anim.SetBool("Teleport", false);
		Transform ParticleBurst = PoolManager.Pools["CoinTextPool"].Spawn(TeleportParticle.transform, transform.position, transform.rotation);
		transform.position = TeleportLocation;
	}

	void MakeBullet()
	{
		SFXManager.Instance.Lazer.Play();
		Transform myInstance = PoolManager.Pools["CoinTextPool"].Spawn(ThingToShoot.transform, new Vector3(transform.position.x + (FaceDir.x / 2), transform.position.y + (FaceDir.y / 2), transform.position.z), transform.rotation);
		SM = myInstance.gameObject.GetComponent<SinosoidalMotion>();
		SM.Dir = FaceDir;
		SM.axis = new Vector3(1 - Mathf.Abs(FaceDir.x), 1 - Mathf.Abs(FaceDir.y), 0);
		SM.magnitude = Random.Range(1,5);
		SM.frequency = Random.Range(2, 10);

		myInstance.parent = OurProjectilePool;
	}
	float DirToZDir()
	{
		float zrot;
		if (FaceDir.x == 1)
		{
			zrot = 180;
		}
		else if (FaceDir.x == -1)
		{
			zrot = 0;
		}
		else if (FaceDir.y == 1)
		{
			zrot = 270;
		}
		else
		{
			zrot = 90;
		}
		return zrot;
	}
	void MakeRocket(float i)
	{
		//Calculated the math so our rocket will spawn like so
		//								x
		//		PhantomFacingThisWay->	x
		//								x
		float Neg = Mathf.Pow(-1, (i + 1));
		float offset = (Mathf.Ceil(i * 0.5f));
		float TotalOffset = Neg* offset;

		//Calculate a z direction to give our rockets based off our direction vector
		float RocketRot = DirToZDir();
		Debug.Log(RocketRot + "is our spawned rockets z.");

		Transform myInstance = PoolManager.Pools["CoinTextPool"].Spawn(Rocket.transform, new Vector3
		   (transform.position.x + (FaceDir.x * 2) + TotalOffset * (1-Mathf.Abs(FaceDir.x)), 
			transform.position.y + (FaceDir.y * 2) + TotalOffset * (1 -Mathf.Abs(FaceDir.y)), 
			transform.position.z), new Quaternion(0,0,RocketRot,0));
		myInstance.gameObject.GetComponent<Homing>().TargetObject = FindObjectOfType<PlayerController>().gameObject;
		myInstance.parent = OurProjectilePool;
	}
}
