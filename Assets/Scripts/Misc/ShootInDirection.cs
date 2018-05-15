using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootInDirection : MonoBehaviour {
	public PlayerController thePlayer;
	Vector3 playerPos;
	Rigidbody2D MyRB;
	public float speedMultiplier;
	// Update is called once per frame
	void Update()
	{
		// Move the projectile forward towards the player's last known direction;
		//transform.position += transform.forward * speed * Time.deltaTime;
	}
	void Awake()
	{
		MyRB = GetComponent<Rigidbody2D>();
		thePlayer = FindObjectOfType<PlayerController>();
		playerPos = new Vector3(thePlayer.transform.position.x, thePlayer.transform.position.y, thePlayer.transform.position.z);
		var Direction = playerPos - this.transform.position;
		MyRB.AddForce(Direction * speedMultiplier, ForceMode2D.Impulse);

	}
}
