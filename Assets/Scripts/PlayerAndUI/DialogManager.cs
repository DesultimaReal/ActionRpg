using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

	public GameObject dBox;
	public Text dText;

	public bool dialogActive;

	public string[] dialogLines;
	public int currentLine;

	private PlayerController thePlayer;

	void Start()
	{
		thePlayer = FindObjectOfType<PlayerController>();
	}
	private void Update()
	{
		if(dialogActive && Input.GetKeyUp(KeyCode.Space))
		{
			currentLine++;
		}
		if(currentLine >= dialogLines.Length)
		{
			dBox.SetActive(false);
			dialogActive = false;

			currentLine = 0;
			thePlayer.canMove = true;
		}
		dText.text = dialogLines[currentLine];
	}
	public void ShowBox(string dialogue)
	{
		dialogActive = true;
		dBox.SetActive(true);
		dText.text = dialogue;
	}
	public void ShowDialogue()
	{
		thePlayer.canMove = false;
		dialogActive = true;
		dBox.SetActive(true);
	}
}
