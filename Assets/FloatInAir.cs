using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatInAir : MonoBehaviour {
	//private Rigidbody2D theRB;
	public float amplitude;          //Set in Inspector 
	public float speed;                  //Set in Inspector 
	private float tempVal;
	public Vector3 tempPos;
	// Use this for initialization
	void Start () {
		//theRB = GetComponent<Rigidbody2D>();
		tempVal = transform.position.y;
	}


	/*public void floatAutomatic()
	{
		floatTimer -= Time.deltaTime;
		if (floatTimer <= 0) { floating = !floating; floatTimer = maxFloatTimer; }
		if (floating)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + floatspeed, transform.position.z);
		}
		else
		{
			transform.position = new Vector3(transform.position.x, transform.position.y - floatspeed, transform.position.z);
		}
	}

	public void floatCurve()
	{
		transform.position = new Vector3(transform.position.x, myCurve.Evaluate((Time.time % myCurve.length)), transform.position.z);
	}*/

	void Update()
	{
		tempPos.y = tempVal + amplitude * Mathf.Sin(speed * Time.time);
		transform.position = tempPos;
	}
}
