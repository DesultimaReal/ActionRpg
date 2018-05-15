using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;
public class LockOnToEnemyInArea : MonoBehaviour {
	public float TimeBetweenChecks;
	public float moveSpeed;
	public float CheckRadius;
	public int LayerToScan;
	public void FixedUpdate()
	{
		MoveForward();
	}
	public void MoveForward()
	{
		transform.position += transform.up * Time.deltaTime * moveSpeed;
	}
	public void OnSpawned()
	{
		StartCoroutine(CheckForEnemies());
		LayerToScan = LayerMask.NameToLayer("ENEMYLAYER");
	}
	public IEnumerator CheckForEnemies()
	{
		for(; ; )
		{
			Debug.Log("Checking!");
			Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, CheckRadius, 1 << LayerToScan);
			if(colliders.Length > 0)
			{
				Debug.Log("Hit " + colliders[0].gameObject);
				GetComponent<Homing>().HomingMode = true;
				GetComponent<Homing>().TargetObject = colliders[0].gameObject;
				GetComponent<LockOnToEnemyInArea>().enabled = false;
			}
			yield return new WaitForSeconds(TimeBetweenChecks);
		}
	}
	public static void PrintEnemysInDir(GameObject g, float Radius, int layerMask, float minDepth, Vector2 Direction)
	{
		Debug.Log("Checking!");
		Physics2D.OverlapCircleAll(new Vector2(g.transform.position.x, g.transform.position.y), Radius, layerMask, minDepth);
		RaycastHit2D[] hits = Physics2D.CircleCastAll(g.transform.position, Radius, Direction);
		foreach(RaycastHit2D hit in hits)
		{
			if(hit.collider.gameObject.tag == "Enemy")
			{
				Debug.Log(hit.collider.gameObject.name);
			}
		}
	}
}
