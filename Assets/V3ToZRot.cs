using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V3ToZRot : MonoBehaviour {
	public Vector3 DirVec;
	// Use this for initialization
	void Start () {
		DirVec = new Vector3(0, 1, 0);
	}
	
	// Update is called once per frame
	void Update () {
		float angle = Vector3.Angle(DirVec, transform.up);
		float RotateAmount = Vector3.Cross(DirVec, transform.up).z;
		//transform.rotation.z = angle;
		Debug.Log(angle);

		/*
		transform.rotation = transform.rotation * DirVec;
		var rot = Quaternion.Euler(0, 45, 0); // rot = 45 degrees rotation around Y
		var v45 = rot * Vector3.forward; // rotate vector forward 45 degrees around Y*/
	}
}
