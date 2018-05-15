using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
	public bool Activate;
	protected Animator anim;
	protected SFXManager sfxMan;
	public virtual void Start()
	{
		sfxMan = FindObjectOfType<SFXManager>();//So we can play our sfx
		anim = GetComponent<Animator>();//So we can control animation
	}
	void Update()
	{
		if (Activate)
		{//We hit the fire
			Activate = false;
			Interaction();
		}
		
	}
	public virtual void Interaction()
	{

	}
}
