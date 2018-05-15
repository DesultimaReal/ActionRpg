using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisible : MonoBehaviour {
	private SpriteRenderer ourRen;
	// Use this for initialization
	void Start () {
		ourRen = GetComponent<SpriteRenderer>();
		ourRen.enabled = false;
	}
}
