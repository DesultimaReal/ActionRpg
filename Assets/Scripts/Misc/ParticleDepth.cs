using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDepth : MonoBehaviour {
	public SpriteRenderer tempRend;
	public ParticleSystemRenderer PSR;
	// Update is called once per frame
	void Start() {
		PSR = gameObject.GetComponent<ParticleSystemRenderer>();
		//tempRend.sortingOrder = Mathf.Abs((int)transform.position.y) * -1;
		Invoke("ChangeSortingOrder", 2);
		
	}
	void ChangeSortingOrder()
	{
		PSR.sortingOrder = transform.parent.GetComponent<SpriteRenderer>().sortingOrder + 1;
	}
}
