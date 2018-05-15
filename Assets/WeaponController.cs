using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour {
	public static WeaponController Instance { get; private set; }
	public enum RangedWeapon
	{
		Bow = 0,
		Boomerang = 1,
		Rocket = 2
	}

	RangedWeapon[] Weapons = {RangedWeapon.Bow, RangedWeapon.Boomerang, RangedWeapon.Rocket};
	RangedWeapon CurrentRangedWeapon;

	public Image RangedWeaponImage;
	public Sprite[] WeaponSprites;
	public int WeaponNumber = 0;
	private void Awake()
	{
		if (Instance == null) { Instance = this; }
		else { Destroy(gameObject); }
	}
	// Use this for initialization
	void Start () {
		CurrentRangedWeapon = Weapons[0];
		RangedWeaponImage.sprite = WeaponSprites[WeaponNumber];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetWeapon(int WeaponNumber)
	{
		CurrentRangedWeapon = Weapons[WeaponNumber];
		RangedWeaponImage.sprite = WeaponSprites[WeaponNumber];
		
	}
}
