using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball2 : MonoBehaviour {
	public GameObject Target;
	public Transform TargetTransform;
	public GameObject cannonBall;
	public float shootAngle;
	public Transform Source;
	private void Start()
	{
		TargetTransform = Target.transform;
		Source = transform;
	}
	Vector3 calcBallisticVelocityVector(Transform source, Transform target, float angle)
	{
		Vector3 direction = target.position - source.position;            // get target direction
		float h = direction.y;                                            // get height difference
		direction.y = 0;                                                // remove height
		float distance = direction.magnitude;                            // get horizontal distance
		float a = angle * Mathf.Deg2Rad;                                // Convert angle to radians
		direction.y = distance * Mathf.Tan(a);                            // Set direction to elevation angle
		distance += h / Mathf.Tan(a);                                        // Correction for small height differences

		// calculate velocity
		float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * a));
		return velocity * direction.normalized;

	}


	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("b"))
		{  // press b to shoot
			GameObject ball = Instantiate(cannonBall, transform.position, Quaternion.identity);
			cannonBall.GetComponent<Rigidbody2D>().velocity = calcBallisticVelocityVector(Source,TargetTransform, shootAngle);
			//Destroy(ball, 10);
		}
	}
}
