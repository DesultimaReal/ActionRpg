using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {
	public static SFXManager Instance { get; private set; }
	public AudioSource playerHurt;
	public AudioSource playerDead;
	public AudioSource playerAttack;
	public AudioSource Lightning;
	public AudioSource FireballSpit;
	public AudioSource PlayerPause;
	public AudioSource CollectCoin;
	public AudioSource HealingStatue;
	public AudioSource Lazer;
	public AudioSource ContinuousLazer;
	public AudioSource LitFire;
	public AudioSource TextNoise;
	public AudioSource ChestOpen;
	public AudioSource PowerUpSound;
	public AudioSource TimeUpSound;
	public AudioSource TargetDestroy;
	public AudioSource[] ChickenSounds;

	// Use this for initialization
	void Start () {
		if (Instance == null) { Instance = this; }
		else { Destroy(gameObject); }
	}
}
