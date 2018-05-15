using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public enum WeaponType
	{
		Bow,
		Boomerang,
		Rocket
	}
	public WeaponType Weapon;

	public int CostToRoll;
	public float timeBetweenRolls = 0.5f;
	public float RollTimer;

	public int CostToBoomerang;
	public float timeBetweenBoomerangs = 0.5f;
	public float BoomerangTimer;

	public GameObject Bow;
	public BowController OurBow;
	public float timeBetweenShots;
	public float ShootTimer;
	public int CostToShoot;

	public GameObject Boomerang;
	public float BoomerangDistance;

	public GameObject Rocket;
	public int CostToRocket = 5;
	public float rocketRate = 0.1f;
	public float timeBetweenRockets = 0.5f;
	public float RocketTimer;

	public float moveSpeed;
	public float MoveSpeedMod = 1.0f;
	public bool canMove;

	private SFXManager sfxMan;

	public Animator anim;
	public Rigidbody2D myRigidbody;
	public Vector2 moveInput;
	private bool playerMoving;
	public Vector2 lastMove;
	/// <summary>
	/// Static means all of the PlayerController scripts 
	/// will use the same bool.
	/// </summary>
	private static bool playerExists;
	public ParticleSystem Smoke;


	public float attackTime;
	public bool talking;
	private float attackTimeCounter;
	public GameObject PauseCanvas;
	// Use this for initialization
	private PlayerHealthManager thePHM;

	public string startPoint;
	void Start() {
		//Instantiate required object components
		sfxMan = FindObjectOfType<SFXManager>();//So our player can play sound effects
		anim = GetComponent<Animator>();//So our player can animate
		anim.speed = 2;
		myRigidbody = GetComponent<Rigidbody2D>();//So our player can use physics
		thePHM = GetComponent<PlayerHealthManager>();//So our player can get hurt or healed or die
		//Make sure our player canMove if for some reason they can't at the start of the scene
		canMove = true;
		//Default to having our player face down
		lastMove = new Vector2(0, -1f);//Have our player spawn in facing down
		talking = false;
		OurBow = Bow.GetComponent<BowController>();
		Weapon = WeaponType.Bow;
	}
	void PauseGame()
	{
		//If our game is unpaused
		if (Time.timeScale == 1.0f)
		{
			PauseCanvas.SetActive(true);//Pause energy and health
			canMove = false;//Make our player unable to move
			Time.timeScale = 0.0f;//Turn off "Time"
			sfxMan.PlayerPause.Play();//Play our Pause sound effect
		}
		else
		{
			canMove = true;
			PauseCanvas.SetActive(false);
			Time.timeScale = 1.0f;
			sfxMan.PlayerPause.Play();
		}
	}

	void Update() {
		//First of all handle Pausing so that
		if (Input.GetKeyDown(KeyCode.P))
		{
			PauseGame();
		}
		//Default to have our PlayerMoving be false unless it changes each frame for animating
		playerMoving = false;
		//If we are set to not be able to move Default to Zero velocity and return until something else changes this.
		//This is in case we are stunned, paused, or interacting with dialogue.
		if ((!canMove) || (talking))//If something is stopping us from moving or we are talking to something
		{
			myRigidbody.velocity = Vector2.zero;
			moveInput = Vector2.zero;
			HandleAnim();
			return;
		}
		///Normalize our vector of movement to create smooth diagonals rather than faster diagonals
		moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
		HandleInput(moveInput);
		HandleTimers();
		HandleAnim();
	}

	void HandleInput(Vector2 moveInput)
	{
		//If not attacking, we can do other things? RN we can do other things while the attack timer counts down
		//We are moving
		if (moveInput != Vector2.zero){
			SetMovement();
		}
		//We aren't moving
		if(moveInput == Vector2.zero)
		{
			SetIdle();
		}
		if (Input.GetKeyDown(KeyCode.J))///RUN ATTACK CODE
		{
			Attack();
		}
		if (Input.GetButton("Fire1"))
		{
			UseRangedWeapon();
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Roll();
		}
	}

	void CheckInFront()
	{
		//RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 2, lastMove);
		//Debug.Log(hits[0].collider.gameObject.name);
	}

	void SetIdle()
	{
		myRigidbody.velocity = Vector2.zero;
		playerMoving = false;
	}

	void SetMovement()
	{
		//Set our velocity to our MoveInput while it is something, and lastMove for our animations
		myRigidbody.velocity = new Vector2(moveInput.x * moveSpeed * MoveSpeedMod, moveInput.y * moveSpeed * MoveSpeedMod);
		playerMoving = true;
		lastMove = moveInput;
	}

	void Roll()
	{
		if (RollTimer <= 0)
		{
			RollTimer = timeBetweenRolls;//Reset the amount of time until the next roll is doable
			if (thePHM.SpendEnergy(CostToRoll))//Attempt to spend 40 energy. If we can...
			{
				//Sprint or something
				Transform ParticleBurst = PoolManager.Pools["CoinTextPool"].Spawn(Smoke.transform, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
				myRigidbody.AddForce(lastMove * 3000.0f *Time.deltaTime, ForceMode2D.Impulse);
				//myRigidbody.velocity = lastMove * 100;

			}
		}
	}
	public void SetWeapon(int WeaponTypeNum)
	{
		Weapon = (WeaponType)WeaponTypeNum;
	}
	void UseRangedWeapon()
	{
		switch (Weapon)
		{
			case WeaponType.Bow:
				Shoot();
				break;
			case WeaponType.Boomerang:
				ThrowBoomerang();
				break;
			case WeaponType.Rocket:
				FireRocket();
				break;
		}
	}
	void ThrowBoomerang()
	{
		if (BoomerangTimer <= 0 && thePHM.SpendEnergy(CostToBoomerang))
		{
			canMove = false;
			//Reset the amount of time until the next roll is doable
			BoomerangTimer = timeBetweenBoomerangs;
			Transform myInstance = PoolManager.Pools["EnemyPool"].Spawn(Boomerang.transform, new Vector3(transform.position.x + lastMove.x, transform.position.y + lastMove.y, transform.position.z), Quaternion.Euler(Vector3.zero));
			myInstance.parent = transform;
			myInstance.gameObject.GetComponent<BoomerangController>().TargetPosition = new Vector3(transform.position.x + lastMove.x * BoomerangDistance, transform.position.y + lastMove.y * BoomerangDistance, transform.position.z);
			canMove = true;
		}
	}
	void FireRocket()
	{
		if (RocketTimer <= 0 && thePHM.SpendEnergy(CostToRocket))
		{
			canMove = false;
			RocketTimer = timeBetweenRockets;
			//Reset the amount of time until the next roll is doable
			float zrot = (Mathf.Abs(lastMove.x) * (lastMove.x * 90 + 90)) + (Mathf.Abs(lastMove.y) * (lastMove.y * 90 + 180)) + 90;
			Transform myInstance = PoolManager.Pools["EnemyPool"].Spawn(Rocket.transform, new Vector3(transform.position.x + lastMove.x, transform.position.y + lastMove.y, transform.position.z), Quaternion.Euler(0, 0, zrot));
			canMove = true;
		}
	}
	void Shoot()
	{
		if(ShootTimer <= 0 && thePHM.SpendEnergy(CostToShoot))
		{
			ShootTimer = timeBetweenShots;
			//Stop the player and make it unreceptive to movement
			//canMove = false;

			anim.SetBool("Shoot", true);//Trigger our animation for our bow
			sfxMan.playerAttack.Play();

			//Cause the bow we always carry to be active and spawn an arrow
			Bow.SetActive(true);
			OurBow.SpawnArrow();
		}
	}

	void FinishShoot()
	{
		
		//canMove = true;
		anim.SetBool("Shoot", false);
		Bow.SetActive(false);
	}

	void Attack()
	{
		canMove = false;//Make our player unreceptive to movement
		anim.SetBool("Attack", true);//Call our animation which creates the hurtbox for our sword
		sfxMan.playerAttack.Play();//Play the sound of our sword swing.
	}

	void FinishAttack()
	{
		canMove = true;
		anim.SetBool("Attack", false);
	}

	void HandleAnim()
	{
		//Process our movement and decide which way to animate our blend tree
		anim.SetFloat("MoveX", moveInput.x);
		anim.SetFloat("MoveY", moveInput.y);
		anim.SetBool("PlayerMoving", playerMoving);
		anim.SetFloat("LastMoveX", lastMove.x);
		anim.SetFloat("LastMoveY", lastMove.y);
	}

	void HandleTimers()
	{ //Because our roll does not have an animation we must process the time it takes to roll manually.
		if (RollTimer > 0)
		{
			RollTimer -= Time.deltaTime;
		}
		if(BoomerangTimer > 0)
		{
			BoomerangTimer -= Time.deltaTime;
		}
		if(RocketTimer > 0)
		{
			RocketTimer -= Time.deltaTime;
		}
		if(ShootTimer > 0)
		{
			ShootTimer -= Time.deltaTime;
		}
	}

}
