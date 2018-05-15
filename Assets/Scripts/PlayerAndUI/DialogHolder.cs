using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogHolder : MonoBehaviour {
	public string dialogue;
	public string[] dialogueLines;
	private DialogManager dMan;
	// Use this for initialization
	void Start () {
		dMan = FindObjectOfType<DialogManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.name == "Player")
		{
			if (Input.GetKeyUp(KeyCode.Space))
			{
				if (!dMan.dialogActive)
				{
					dMan.dialogLines = dialogueLines;
					dMan.currentLine = 0;
					dMan.ShowDialogue();//Activate and show out dialogue ui
				}
				if (transform.parent.GetComponent<VillagerMovement>() != null)
				{
					transform.parent.GetComponent<VillagerMovement>().canMove = false;
				}
			}
		}
	}
}
