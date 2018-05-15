using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

public class Chest: Interactable {
	public enum ChestType
	{
		Energy,
		Speed,
		Coin
	}
	//public bool Interacting;
	private bool Lit;
	public Sprite open;
	private SpriteRenderer spriteR;
	private bool opened;
	public GameObject[] objects;
	public int numCoins;
	public ChestType Type;
	public float ObjectInterval;
	public override void Start()
	{
		base.Start();
		opened = false;
		spriteR = gameObject.GetComponent<SpriteRenderer>();
		Type = (ChestType)Random.Range(0, 3);
		Physics2D.IgnoreLayerCollision(0, 16);
	}
	public override void Interaction()
	{
		if (!opened)
		{
			opened = true;
			base.sfxMan.ChestOpen.Play();
			spriteR.sprite = open;
			this.StartCoroutine(this.SpawnObjects()); 
		}
		//Sprite = 
		
	}
	public IEnumerator SpawnObjects()
	{
		//The chest will always spawn coins for the player
			GameObject Coin = objects[0];
			//Debug.Log("Hey0");
			for (int i = 0; i < numCoins; i++)
			{
				//Debug.Log("Hey");
				ArcObject(Coin);
				yield return new WaitForSeconds(ObjectInterval);
			}
		//Sometimes it will spawn an energy
		if(Type == ChestType.Energy)
		{
				ArcObject(objects[1]);//Objects of 1 will always be an energy pellet
				yield return new WaitForSeconds(ObjectInterval);
				//Spawn in arc objects in chese
			
		}
		//Sometimes it will spawn an time upgrade
		if(Type == ChestType.Speed)
		{
			ArcObject(objects[2]);
			yield return new WaitForSeconds(ObjectInterval);
		}
	}
	public void ArcObject(GameObject g)
	{
		Transform myInstance = PoolManager.Pools["CoinTextPool"].Spawn(g.transform, new Vector3(transform.position.x,transform.position.y,transform.position.z), transform.rotation);
		myInstance.parent = transform;
	}
}
