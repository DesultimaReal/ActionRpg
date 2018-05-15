using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using UnityEngine;

public class ChargeAndShoot : MonoBehaviour {
	public enum Face
	{
		Up,
		Down,
		Left,
		Right
	}
	public int NumberToShoot;

	public Vector3 Axis;
	public Face FaceDirection;
	public Vector3 Dir;

	public Sprite ChargingSprite;
	public Sprite IdleSprite;
	private SpriteRenderer ourRenderer;

	public float ChargeTimeCounter;
	public float ChargeTimeTotal;
	public GameObject ThingToShoot;
	public int burstSize;
	public float rateOfFire;
	public bool CanShoot;	

	private SinosoidalMotion SM;
	public SFXManager sfxMan;
	// Use this for initialization
	void Start () {
		sfxMan = FindObjectOfType<SFXManager>();
		ourRenderer = GetComponent<SpriteRenderer>();
		ChargeTimeCounter = ChargeTimeTotal;
		SetUpDirection();
	}
	void SetUpDirection()
	{
		switch (FaceDirection)
		{
			case Face.Up:
				Dir = new Vector3(0, 1, 0);
				break;
			case Face.Left:
				Dir = new Vector3(-1, 0, 0);
				break;
			case Face.Right:
				Dir = new Vector3(1, 0, 0);
				break;
			case Face.Down:
				Dir = new Vector3(0, -1, 0);
				break;
		}
		Axis = new Vector3(1 - Mathf.Abs(Dir.x), 1 - Mathf.Abs(Dir.y), 0);
	}
	// Update is called once per frame
	void Update () {
		if(ChargeTimeCounter > 0)
		{
			ChargeTimeCounter -= Time.deltaTime;
		}
		else
		{
			ChargeTimeCounter = ChargeTimeTotal;
			StartCoroutine(FireBurst( ));
		}
	}
	public IEnumerator FireBurst()
	{
		float bulletDelay = 60 / rateOfFire;
		// rate of fire in weapons is in rounds per minute (RPM), therefore we should calculate how much time passes before firing a new round in the same burst.
		for (int i = 0; i < burstSize; i++)
		{
			MakeBullet();
			yield return new WaitForSeconds(bulletDelay); // wait till the next round
		}
	}
	void MakeBullet()
	{
		sfxMan.Lazer.Play();
		Transform myInstance = PoolManager.Pools["CoinTextPool"].Spawn(ThingToShoot.transform, new Vector3(transform.position.x + Dir.x, transform.position.y + Dir.y, transform.position.z), transform.rotation);
		SM = myInstance.gameObject.GetComponent<SinosoidalMotion>();
		SM.Dir = Dir;
		SM.axis = Axis;
		myInstance.parent = transform;
	}
}
