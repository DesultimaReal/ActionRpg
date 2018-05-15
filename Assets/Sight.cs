using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour {
	public MagiController MC;
	public Vector3 Dir;
	private float SightDistance;
	private int layerMask;
	public Transform SightHit;
	private LineRenderer lineRenderer;
	public GameObject HitObject;
	private SpriteRenderer ourRen;
	public Vector3 HeadPos;
	// Use this for initialization
	void Start () {
		MC = GetComponent<MagiController>();
		lineRenderer = GetComponent<LineRenderer>();
		SightDistance = Mathf.Infinity;
		layerMask = 1 << gameObject.layer;
		layerMask = ~layerMask;
		lineRenderer.useWorldSpace = true;//This means that the lineRenderer uses the world rather than the object
		ourRen = GetComponent<SpriteRenderer>();
	}
	
	public void SwitchDir()
	{
		Debug.Log("Switching");
		Dir = MC.Dir;
		if (Dir == -this.transform.up)
		{
			lineRenderer.sortingOrder = ourRen.sortingOrder + 1;
		}
		else
		{
			lineRenderer.sortingOrder = ourRen.sortingOrder - 1;
		}
	}
	// Update is called once per frame
	void Update () {
		HeadPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		TrackInFront();
	}
	void TrackInFront()
	{
		Debug.Log(Dir);
		RaycastHit2D hit = Physics2D.Raycast(HeadPos, Dir, SightDistance, layerMask);
		if (hit.collider != null)
		{

			SightHit.position = hit.point;
			lineRenderer.SetPosition(0, transform.position);
			lineRenderer.SetPosition(1, SightHit.position);
			HitObject = hit.transform.gameObject;
			Debug.DrawLine(transform.position, SightHit.position);
			MC.distanceToWall = Mathf.Abs(Vector3.Distance(SightHit.position, MC.gameObject.transform.position));
		}
	}
}
