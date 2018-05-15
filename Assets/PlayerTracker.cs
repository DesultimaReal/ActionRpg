using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour {
	public static PlayerTracker Instance { get; private set; }
	private PlayerController thePC;
	public Vector3 PlayerLocation;

	// Use this for initialization
	void Awake () {
		if (Instance == null) { Instance = this; }
		else { Destroy(gameObject); }

		thePC = FindObjectOfType<PlayerController>();
		
	}
	
	// Update is called once per frame
	void Update () {
		PlayerLocation = thePC.gameObject.transform.position;
	}
}
