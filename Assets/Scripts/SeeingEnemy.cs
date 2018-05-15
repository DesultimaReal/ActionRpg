using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeingEnemy : MonoBehaviour {
	public float sightDistance = 8;
	protected bool seesPlayer;
	// Use this for initialization
	
	// Update is called once per frame
	public virtual void Update () {
		//Debug.Log(Vector3.Distance(PlayerTracker.Instance.PlayerLocation, transform.position));
		if (Vector3.Distance(PlayerTracker.Instance.PlayerLocation, transform.position) < sightDistance)
		{
			//Debug.Log(this.name + " sees you");
			seesPlayer = true;
		}
		else
		{

			seesPlayer = false;
		}
	}
}
