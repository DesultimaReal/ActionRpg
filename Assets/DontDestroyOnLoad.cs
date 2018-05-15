using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour {
	private static bool Exists;
	// Use this for initialization
	void Start () {
		//This singleton Design murders the rest of our start functions
		if (!Exists)
		{
			Exists = true;
			DontDestroyOnLoad(transform.gameObject);///Dont destroy our Audio
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
