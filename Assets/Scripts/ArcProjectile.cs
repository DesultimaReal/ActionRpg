using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

public class ArcProjectile : MonoBehaviour {

	public float height;
	public float startingHeight;

	public Rigidbody2D ourRB;

	public Vector3 Dir;
	public float ForceAmount;
	public PlayerController PC;

	public float CurrentDuration;
	public float Duration;
	private Vector3 startPos;
	private Vector3 endPos;
	public float targetHeight;
	public bool falling;
	private HomeToPlayer HomingScript;
	private void Start()
	{
		HomingScript = GetComponent<HomeToPlayer>();
		HomingScript.enabled = false;
		falling = false;
		Physics2D.IgnoreCollision(transform.parent.GetComponent<Collider2D>(), GetComponent<Collider2D>());
		startingHeight = transform.position.y;
		Dir = new Vector3(Random.Range(-2.25f, 2.25f), 3.0f, 0.0f);
		ourRB = GetComponent<Rigidbody2D>();
		
		ourRB.gravityScale = 1;
		ourRB.AddForce(Dir * ForceAmount, ForceMode2D.Impulse);
		targetHeight = startingHeight - Random.Range(0.5f, 1.5f);
		CurrentDuration = 0.0f;
		startPos = transform.position;
		endPos = new Vector3(startPos.x + Random.Range(1, -1), startPos.y - 1, startPos.z);
	}
	private void Update()
	{
		if(transform.position.y < height)
		{
			falling = true;
		}
		height = transform.position.y;
		
		if (height <= targetHeight && falling)
		{//Our arc is finished
			HomingScript.enabled = true;
			//ourRB.gravityScale = 0;
			//ourRB.velocity = Vector2.zero;
			//ourRB.isKinematic = true;
			Destroy(ourRB);
			//ourRB.detectCollisions = false;
			//ourRB.collisionDetectionMode = 

		}
	}
	
}
