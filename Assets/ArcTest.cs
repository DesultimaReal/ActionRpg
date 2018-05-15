using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class ArcTest : MonoBehaviour
{

	[Tooltip("Position we want to hit")]
	public Vector3 targetPos;

	[Tooltip("Horizontal speed, in units/sec")]
	public float speed = 10;

	[Tooltip("How high the arc should be, in units")]
	public float arcHeight = 1;

	//public GameObject Target;
	Vector3 startPos;
	Vector3 nextPos;

	void OnSpawned()
	{
		// Cache our start position, which is really the only thing we need
		// (in addition to our current position, and the target).
		float offset = Random.Range(-1, 1);
		Debug.Log(offset);
		startPos = transform.position;
		targetPos = new Vector3(startPos.x + offset, startPos.y, startPos.z);

	}

	void Update()
	{
		// Compute the next position, with arc added in
		float x0 = startPos.x;
		float x1 = targetPos.x;
		float dist = x1 - x0;
		float nextX = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime);
		float baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextX - x0) / dist);
		float arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
		nextPos = new Vector3(nextX, baseY + arc, transform.position.z);

		// Rotate to face the next position, and then move there
		transform.rotation = LookAt2D(nextPos - transform.position);
		transform.position = nextPos;

		// Do something when we reach the target
		if (nextPos == targetPos) Arrived();
	}

	void Arrived()
	{
		
	}

	/// 
	/// This is a 2D version of Quaternion.LookAt; it returns a quaternion
	/// that makes the local +X axis point in the given forward direction.
	/// 
	/// forward direction
	/// Quaternion that rotates +X to align with forward
	static Quaternion LookAt2D(Vector2 forward)
	{
		return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
	}
}