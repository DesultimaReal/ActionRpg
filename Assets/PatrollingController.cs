using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingController : MonoBehaviour {
	public Transform target;
	public float speed;
	public GameObject[] Points;
	public Transform[] PointLocations;
	public int WhichPoint;
	public int numPoints;
	private void Start()
	{
		numPoints = Points.Length;
		PointLocations = new Transform[numPoints];
		//Convert array of gameObjects into transforms
		for (int i = 0; i < Points.Length; ++i)
			PointLocations[i] = Points[i].transform;
		WhichPoint = 0;
		target = PointLocations[WhichPoint];
	}
	void Update()
	{
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target.position, step);
		if(Mathf.Abs(Vector3.Distance(transform.position, target.position)) < 1)
		{
			if(++WhichPoint == numPoints)
			{
				WhichPoint = 0;
			}
			target = PointLocations[WhichPoint];
		}
	}
}
