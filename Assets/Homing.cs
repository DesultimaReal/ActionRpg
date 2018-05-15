using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Homing : MonoBehaviour {
	private Rigidbody2D rb;
	public float speed = 5f;
	public float NormalSpeed;
	public float CurrentRotateSpeed;
	public float maxRotateSpeed = 200f;
	public float NormalRotateSpeed;
	public float homingTime;
	public float MaxHomingTime;
	public bool isHoming;
	public bool doneEmerging;
	public GameObject TargetObject;
	public bool HomingMode;
	// Use this for initialization
	void OnSpawned () {
		rb = GetComponent<Rigidbody2D>();
	}
	void StartEmerge()
	{
		doneEmerging = false;
		CurrentRotateSpeed = maxRotateSpeed;
		speed = 0;
	}
	// Update is called once per frame
	void FinishEmerge()
	{
		doneEmerging = true;
		isHoming = true;
		CurrentRotateSpeed = NormalRotateSpeed;
		speed = NormalSpeed;
		homingTime = MaxHomingTime;
	}
	void FixedUpdate () {

			Vector2 direction = (Vector2)TargetObject.transform.position - rb.position;
			direction.Normalize();
			float RotateAmount = Vector3.Cross(direction, transform.up).z;
			//Debug.Log(RotateAmount + "RotateAmount");
			rb.angularVelocity = -RotateAmount * CurrentRotateSpeed;
			rb.velocity = transform.up * speed;
			if (doneEmerging && isHoming)
			{
				homingTime -= Time.deltaTime;
				if (homingTime <= 0)
				{
					isHoming = false;
					CurrentRotateSpeed = 0;
				}
			}
	}
}
