using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkTo : MonoBehaviour {
	private PlayerHealthManager thePHMan;
	public SFXManager theSFXMan;
	public float HealPercent;
	// Use this for initialization
	void Start () {
		thePHMan = FindObjectOfType<PlayerHealthManager>();
		theSFXMan = FindObjectOfType<SFXManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (Input.GetKeyDown(KeyCode.J))
		{
			theSFXMan.HealingStatue.Play();
			thePHMan.Regen(HealPercent);
			//Debug.Log("hey there");
			//LockOnToEnemyInArea.PrintEnemysInDir(this.gameObject, 10, 10, 0, transform.position - FindObjectOfType<PlayerController>().gameObject.transform.position);
		}
	}
}
