using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {
	public SFXManager sfxMan;
	public float MaxTime;
	public float Timer;
	public bool consumed;
	public bool touched;

	public float RotateAroundCircleSpeed = 2.0f;
	public float Radius = 0.5f;
	public Vector2 _centre;
	public float MovingAngle;
	public PlayerController thePC;

	public float ShrinkInterval;
	public float ShrinkAmount;
	public float TotalShrink;
	public virtual void Start()
	{
		sfxMan = FindObjectOfType<SFXManager>();
		thePC = FindObjectOfType<PlayerController>();
		touched = false;
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.name == "Player" && !touched)
		{
			StartPower();
			Debug.Log("GotPower");
		}
	}
	public virtual void StartPower()
	{
		
		gameObject.GetComponent<CircleCollider2D>().enabled = false;
		Timer = MaxTime;
		touched = true;
		
	}
	public IEnumerator ShrinkOverTime()
	{
		for (int i = 0; i < TotalShrink; i++)
		{
			transform.localScale -= new Vector3(ShrinkAmount, ShrinkAmount, 0);
			yield return new WaitForSeconds(ShrinkInterval);
		}
	}
	public virtual void EndPower()
	{
		Debug.Log("EndingPower");
		Destroy(gameObject);
	}
	public virtual void Update()
	{
		if (touched){
			DontDestroyOnLoad(this);
			Timer -= Time.deltaTime;
			_centre = thePC.gameObject.transform.position;
			MovementOption();
			if (Timer <= 0)
			{
				EndPower();
			}
		}
	}
	public virtual void MovementOption()
	{

	}
}
