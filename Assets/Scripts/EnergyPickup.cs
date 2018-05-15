using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPickup : Powerup {
	public PlayerHealthManager thePHM;
	// Use this for initialization
	public override void Start () {
		base.Start();
		thePHM = FindObjectOfType<PlayerHealthManager>();
	}
	public override void Update()
	{
		base.Update();
	}
	public override void StartPower()
	{
		base.StartPower();
		StartCoroutine(base.ShrinkOverTime());
		sfxMan.PowerUpSound.Play();
		thePHM.energyRegenRate *= 4;
		thePC.timeBetweenRolls /= 2;
	}
	public override void EndPower()
	{
		base.EndPower();
		thePC.timeBetweenRolls *= 2;
		thePHM.energyRegenRate /= 4;
	}
	public override void MovementOption()
	{
		base.MovementOption();
		MovingAngle += RotateAroundCircleSpeed * Time.deltaTime;
		var offset = new Vector2(Mathf.Sin(MovingAngle), Mathf.Cos(MovingAngle)) * Radius;
		transform.position = _centre + offset;
	}
}
