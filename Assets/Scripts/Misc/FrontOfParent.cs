using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontOfParent : MonoBehaviour {
	public SpriteRenderer tempRendUs;
	public float MaxTimer;
	public float Timer;
	public bool DynamicDepth;

	// Update is called once per frame
	void Start () {
		DynamicDepth = false;
		tempRendUs = gameObject.GetComponent<SpriteRenderer>();
		//tempRend.sortingOrder = Mathf.Abs((int)transform.position.y) * -1;
		tempRendUs.sortingOrder = transform.GetComponentInParent<SpriteRenderer>().sortingOrder + 1;
		Timer = MaxTimer;
	}
	void Update()
	{
		Timer-=Time.deltaTime;
		if(Timer <= 0 && !DynamicDepth)
		{
			DynamicDepth = true;
			tempRendUs.sortingOrder = (int)Camera.main.WorldToScreenPoint(tempRendUs.bounds.min).y * -1;
		}
	}


}
