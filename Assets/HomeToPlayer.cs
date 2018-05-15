using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeToPlayer : MonoBehaviour {
	public float speed = 4;
	void Update()
	{
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, PlayerTracker.Instance.PlayerLocation, step);
	}
}
