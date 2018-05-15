using UnityEngine;
using System.Collections;
using PathologicalGames;
 public class SpawnInCircle : MonoBehaviour
{

	public int numObjects = 11;
	public GameObject prefab;



	void Start()
	{
		Vector3 center = transform.position;
		float ang = 360 / numObjects;
		for (int i = 0; i < numObjects; i++)
		{
			
			Vector3 pos = CirclePoint(center, 2.0f, ang);
			Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
			Transform T = PoolManager.Pools["EnemyPool"].Spawn(prefab.transform, pos, rot);
			T.parent = transform;
			T.gameObject.GetComponent<RotateInCircle>()._centre = new Vector2(transform.position.x, transform.position.y);
			T.gameObject.GetComponent<RotateInCircle>().MovingAngle = ang % 360;
			T.gameObject.GetComponent<RotateInCircle>().target = transform;
			ang += 360 / numObjects;
		}
	}
	private void Update()
	{
		
	}
	Vector3 CirclePoint(Vector3 center, float radius, float ang)
	{
		Vector3 pos;
		pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
		pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
		pos.z = center.z;
		return pos;
	}
}
