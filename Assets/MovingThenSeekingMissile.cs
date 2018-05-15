using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingThenSeekingMissile : MonoBehaviour {
	public float TimeBetweenChecks;
	public float NormalMoveSpeed;
	public float moveSpeed;

	public float CheckRadius;
	public int LayerToScan;
	public Rigidbody2D rb;
	public bool Homing;
	public GameObject Target;
	public float CurrentRotateSpeed;

	public void FixedUpdate()
	{
		if(Target == null)
		{
			Homing = false;
		}
		if (!Homing)
		{
			MoveForward();
			//Debug.Log("Manually moving transform");
		}
		else
		{
			//Debug.Log("Homing to" + Target.name);
			Vector2 direction = (Vector2)Target.transform.position - rb.position;
			direction.Normalize();
			float RotateAmount = Vector3.Cross(direction, transform.up).z;
			//Debug.Log(RotateAmount + " is our RotateAmount");
			rb.angularVelocity = -RotateAmount * CurrentRotateSpeed;
			rb.velocity = transform.up * moveSpeed;
		}
	}
	public void MoveForward()
	{
		transform.position += transform.up * Time.deltaTime * moveSpeed;
	}
	public void OnSpawned()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.freezeRotation = true;
		StartCoroutine(CheckForEnemies());
		LayerToScan = LayerMask.NameToLayer("ENEMYLAYER");
		Homing = false;
	}
	public IEnumerator CheckForEnemies()
	{
		while (!Homing)
		{
			//Debug.Log("Checking!");
			Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, CheckRadius, 1 << LayerToScan);
			if (colliders.Length > 0)
			{
				//Debug.Log("Hit " + colliders[0].gameObject); Debug.Log("This rocket is homing");
				Target = colliders[0].gameObject;
				Homing = true;
				rb.freezeRotation = false;
			}
			yield return new WaitForSeconds(TimeBetweenChecks);
		}
	}
	void StartEmerge()
	{
		moveSpeed = 0;
	}
	void FinishEmerge()
	{
		moveSpeed = NormalMoveSpeed;
	}
}
