using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

	public SFXManager sfxMan;
	
	public GameObject textBox;
	public Text theText;

	public TextAsset textFile;
	public string[] textLines;

	public int currentLine;
	public int endAtLine;

	public PlayerController player;
	public bool stopPlayerMovement;

	private bool isTyping = false;
	private bool cancelTyping = false;
	public float typeSpeed;

	public bool isActive;
	// Use this for initialization
	void Start()
	{
		sfxMan = FindObjectOfType<SFXManager>();
		player = FindObjectOfType<PlayerController>();
		if (textFile != null)
		{
			textLines = (textFile.text.Split('\n'));
		}
		if(endAtLine == 0)
		{
			endAtLine = textLines.Length - 1;
		}

		if (isActive)
		{
			EnableTextBox();
		}
		else
		{
			DisableTextBox();
		}
	}

	// Update is called once per frame
	void Update () {
		if(!isActive)
		{
			return;
		}
		//theText.text = textLines[currentLine];
		if (Input.GetKeyDown(KeyCode.J))
		{
			if (!isTyping)
			{
				currentLine += 1;
				if (currentLine > endAtLine)
				{
					DisableTextBox();
				}
				else
				{
					StartCoroutine(TextScroll(textLines[currentLine]));
				}

			}
			else if(isTyping && !cancelTyping)
			{
				cancelTyping = true;
			}
		}
		
	}

	private IEnumerator TextScroll(string lineOfText)
	{
		int letter = 0;
		theText.text = "";
		isTyping = true;
		cancelTyping = false;
		while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
		{
			theText.text += lineOfText[letter];
			letter += 1;
			sfxMan.TextNoise.Play();
			yield return new WaitForSeconds(typeSpeed);
		}
		theText.text = lineOfText;
		isTyping = false;
		cancelTyping = false;
	}

	public void EnableTextBox()
	{
		textBox.SetActive(true);
		isActive = true;
		if (stopPlayerMovement)
		{
			//Debug.Log("HitOne");
			//player.canMove = false;
			player.talking = true;
		}
		StartCoroutine(TextScroll(textLines[currentLine]));
	}
	public void DisableTextBox()
	{
		//Debug.Log("HitTwo");
		isActive = false;
		textBox.SetActive(false);
		//player.canMove = true;
		player.talking = false;
	}
	public void ReloadScript(TextAsset theText)
	{
		if(theText != null)
		{
			textLines = new string[1];
			textLines = (theText.text.Split('\n'));
		}
	}
}
