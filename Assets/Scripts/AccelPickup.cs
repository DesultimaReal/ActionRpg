using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelPickup : Powerup {
	// Use this for initialization
	public Animator MyAnim;
	public override void Start () {
		base.Start();
		MyAnim = GetComponent<Animator>();
	}
	public override void Update()
	{
		base.Update();
	}
	public override void StartPower()
	{
		sfxMan.TimeUpSound.Play();
		base.StartPower();
		StartCoroutine(Accelerate());

	}
	public IEnumerator Accelerate()
	{
		while (base.Timer >= 0)
		{
			MyAnim.speed += 0.01f;
			base.thePC.MoveSpeedMod = Mathf.Clamp(base.thePC.MoveSpeedMod * 1.01f, 1, 5);
			base.thePC.anim.speed = 3.5f;
			yield return new WaitForSeconds(0.1f);
		}
	}
	public override void EndPower()
	{
		base.EndPower();
		base.thePC.MoveSpeedMod = 1;
		base.thePC.anim.speed = 1.0f;
	}
	public override void MovementOption()
	{
		base.MovementOption();
		transform.position = new Vector3(_centre.x, _centre.y+1, transform.position.z);
	}
}
