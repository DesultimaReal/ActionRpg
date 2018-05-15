using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Depth2 : MonoBehaviour {
	public SpriteRenderer tempRend;
	// Update is called once per frame
	void Update () {
		tempRend = gameObject.GetComponent<SpriteRenderer>();
		//tempRend.sortingOrder = Mathf.Abs((int)transform.position.y) * -1;
		tempRend.sortingOrder = (int)Camera.main.WorldToScreenPoint(tempRend.bounds.min).y * -1;
	}

}
