using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : SeeingEnemy {
	int layerMask;
	public bool hasLineOfSight;
	public static Vector3 UpDir = new Vector3(0,1,0), 
		DownDir = new Vector3(0,-1,0),
		LeftDir= new Vector3(-1,0,0), 
		RightDir= new Vector3(1, 0, 0);
	// Update is called once per frame
	public void Start()
	{
		//layerMask = 1 << 17 | 1 << 10;
		layerMask = 1 << LayerMask.NameToLayer("SOLIDSTUFF") | 1 << LayerMask.NameToLayer("Player");//This will mean our AI see our player and SOLIDSTUFF like trees
		//layerMask = ~layerMask;
		hasLineOfSight = false;
	}
	public override void Update()
	{
		base.Update();
		if (seesPlayer)
		{
			RaycastHit2D hit = Physics2D.Raycast(transform.position, PlayerTracker.Instance.PlayerLocation- transform.position, sightDistance, layerMask);
			
			if (hit.collider != null)
			{
				Debug.DrawLine(transform.position, hit.transform.position);
				if(hit.transform.gameObject.tag == "Player")
				{
					hasLineOfSight = true;
					//Debug.Log(gameObject.name + " SEES " + hit.transform.gameObject);
				}
				
			}
		}
	}
}
