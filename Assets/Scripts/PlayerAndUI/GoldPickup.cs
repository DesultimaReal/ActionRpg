using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickup : MonoBehaviour {
	public int Value;
	public MoneyManager theMM;
	public int PointWorth;
	// Use this for initialization
	void Start () {
		theMM = FindObjectOfType<MoneyManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.name == "Player")
		{
			theMM.AddMoney(Value);
			ScoreManager.globalScoreManager.AddToScore(PointWorth);
			Destroy(gameObject);
		}
	}
}
