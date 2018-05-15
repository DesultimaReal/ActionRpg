using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour {
	public string levelToLoad;
	// Use this for initialization
	private PlayerController thePlayer;
	public string exitPoint;
	void Start () {
		thePlayer = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

     void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.name == "Player")
		{
			SceneManager.LoadScene(levelToLoad);///Load a new Scene
			thePlayer.startPoint = exitPoint;
		}
	}
}
