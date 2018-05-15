using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interact : MonoBehaviour {
	public bool waitForPress;
	public Interactable OurParent;
	private void Start()
	{
		OurParent = this.transform.parent.GetComponent<Interactable>();
	}
	void Update()
	{
		if (waitForPress && Input.GetKeyDown(KeyCode.J))
		{
			//waitForPress = false;
			OurParent.Activate = true;
		}
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Player")
		{
			waitForPress = true;
		}
	}
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.name == "Player")
		{
			waitForPress = false;
		}
	}
}
