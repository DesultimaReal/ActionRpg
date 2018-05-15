using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFire: Interactable {
	//public bool Interacting;
	private bool Lit;
	public override void Start()
	{
		base.Start();
	}
	public override void Interaction()
	{
		if (!Lit)
		{//If it isnt already lit.
			Lit = true;//It is now lit
			base.sfxMan.LitFire.Play();//Play the sound of when the fire lights

		}
		else
		{//If it is Lit already.
			Lit = false;//It isnt anymore
			base.sfxMan.LitFire.Play();//Play the sound of when the fire lights
		}
		anim.SetBool("Lit", Lit);
	}
}
