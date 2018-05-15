using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomHealthManager : NaturalHealthManager {
	private PhantomController OurPC;
	public void Start()
	{
		OurPC = GetComponent<PhantomController>();
	}
	public override void HurtEnemy(int damageToGive)
	{
		base.HurtEnemy(damageToGive);
		Debug.Log("Aggroing!");
		if (ourController.Aggro == false)
		{
			Debug.Log("Aggroing");
			ourController.Aggro = true;
		}
		if (CurrentHealth < MaxHealth / 2)
		{
			OurPC.Berserk();
		}
	}
}
