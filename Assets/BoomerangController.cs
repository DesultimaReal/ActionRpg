using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using UnityEngine;

public class BoomerangController : MonoBehaviour {
	public Vector3 TargetPosition;

	public int speed;

	public bool Returning;
	public Vector3 OurParentPos;
	public Vector3 OurPos;
	public Transform OurParent;
	public float MaxDist;
	public Vector3 OriginalPos;
	public PlayerProjectile ourDamageCont;
	// Use this for initialization
	void OnSpawned () {
		
		OriginalPos = transform.position;
		TargetPosition = new Vector3(0,0,0);
		Returning = false;
		Physics2D.IgnoreLayerCollision(17, 15);
		ourDamageCont = GetComponent<PlayerProjectile>();
		ourDamageCont.damageMult = 1;
		if (transform.parent)
		{
			OurParent = transform.parent;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Make our boomerang spin as it goes
		transform.Rotate(Vector3.back * 10.0f);
		//Track our and our parent position for coming back
		OurParentPos = transform.parent.position;
		OurPos = transform.position;
		if (Vector3.Distance(OurPos, TargetPosition) > 0.1f && Returning == false && Vector3.Distance(OurPos, OriginalPos) < MaxDist)
		{
			transform.position = Vector3.MoveTowards(transform.position, TargetPosition, speed * Time.deltaTime);
		}
		else
		{
			ourDamageCont.damageMult = 2;
			Returning = true;
			transform.position = Vector3.MoveTowards(transform.position, OurParentPos, speed * Time.deltaTime);
		}
	}
	public void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.transform == transform.parent && Returning == true )
		{
			if(OurParent = transform.parent)
			{
				this.transform.parent = null;
				PoolManager.Pools["EnemyPool"].Despawn(this.transform);//Get rid of the object
			}
		}
	}
}
