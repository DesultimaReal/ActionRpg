using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInCircle : MonoBehaviour {

	public float RotateAroundCircleSpeed;
	public float Radius = 2.0f;

	public Vector2 _centre;
	public float MovingAngle;

	// Where we will rotate toward every update
	public Transform target;
	//How fast we will rotate
	public float RotateTowardCenterSpeed;

	//To make the radius of our circle grow and shrink
	private bool RadiusGrowing;
	private float SwitchTimer;
	public float fullTimer;
	public float growthRate;

	private void Start()
	{
		RadiusGrowing = false;
		SwitchTimer = fullTimer;
	}

	private void Update()
	{
		_centre = transform.parent.position;
		RotateToCentre();
		MoveInCircle();
		ManageRadius();
	}
	void ManageRadius()
	{
		SwitchTimer-= Time.deltaTime;
		if (SwitchTimer < 0)
		{
			SwitchTimer = fullTimer;
			RadiusGrowing = !RadiusGrowing;
		}
		if (RadiusGrowing)
		{
			Radius += growthRate;
		}
		else
		{
			Radius -= growthRate;
		}
	}
	void MoveInCircle()
	{
		MovingAngle += RotateAroundCircleSpeed * Time.deltaTime;
		var offset = new Vector2(Mathf.Sin(MovingAngle), Mathf.Cos(MovingAngle)) * Radius;
		transform.position = _centre + offset;
	}
	void RotateToCentre()
	{
		Vector3 vectorToTarget = target.position - transform.position;
		float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) + 180;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * RotateTowardCenterSpeed);
	}

}