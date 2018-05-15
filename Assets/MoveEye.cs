using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using UnityEngine;

public class MoveEye : MonoBehaviour
{
	//So we can get the players position and store it in a vector playerPos at all times.
	private PlayerController thePlayer;
	private Vector3 playerPos;
	private Vector3 OccPos;
	
	
	//For our EYEs rotating and position
	Vector3 PreviousPoint;
	public float SpeedOfRotation;
	//The distance from our pupil to the center of the occulus.
	public float DistanceFromCenter;
	//To store our direction between player and occulus for our lazer
	Vector3 DirectionBetweenPoints;

	//Our lazers necessary components
	private LineRenderer lineRenderer;
	public Transform LazerHit;
	public int layerMask;
	private float lazerDistance;
	GameObject HitObject;
	
	//For hitting the player
	private PlayerHealthManager thePHM;
	public int LazerDamage;

	//To make the lazer look and sound better
	public ParticleSystem LazerFx;
	public SFXManager sfxMan;

	void Start()
	{
		
		//Get necessary Components from the world
		thePlayer = FindObjectOfType<PlayerController>();
		lineRenderer = GetComponent<LineRenderer>();
		thePHM = FindObjectOfType<PlayerHealthManager>();
		sfxMan = FindObjectOfType<SFXManager>();
		//Want to make this loop in the future
		sfxMan.ContinuousLazer.Play();
		//Get our occulus and eyes position setup for movement
		OccPos = transform.parent.position;
		transform.position = OccPos;
		PreviousPoint = transform.position;

		//Setup Lazer aspects
		lineRenderer.enabled = true;//Change this when we want the lazer on or off
		lineRenderer.useWorldSpace = true;//This means that the lineRenderer uses the world rather than the object
		//Setup the layer we want to ignore in a bitmask for our raycast. Then we not it so we can ignore that layer in the Raycast call.
		layerMask = 1 << 14;
		layerMask = ~layerMask; 
		//Setup the distance the ray can go.
		lazerDistance = Mathf.Infinity;
	}

	// Update is called once per frame
	void Update()
	{
		TrackingLazer();
		
	}
	
	void TrackingLazer()
	{
		//ROTATE THE EYE AROUND THE OCCULUS
		//Get a point between us and a player a distance away to move our eye to
		playerPos = thePlayer.transform.position;
		//Get a point between the player and occulus and move our eye to it. This is probably better done with a blend tree using a skeleton, but i'm not an animator lol.
		Vector3 DesiredPoint = LerpByDistance(OccPos, playerPos, DistanceFromCenter);
		
		
		//Slowly rotate our eye so it doesn't automatically track the player. 
		transform.position = Vector3.Slerp(PreviousPoint, DesiredPoint, SpeedOfRotation);
		PreviousPoint = transform.position;//Save our CurrentPoint as a previous to use for new linear extrapolation

		//Save the direction our eye is pointing to shoot our lazer
		DirectionBetweenPoints = transform.position - OccPos;
		//Physics2D.raycastsHitTriggers = false;
		playerPos = thePlayer.transform.position;
		
		//Handle the LAZER
		RaycastHit2D hit = Physics2D.Raycast(transform.position, DirectionBetweenPoints, lazerDistance, layerMask);
		if(hit.collider != null)
		{
			
			//Render our lazer from its beginning to end
			LazerHit.position = hit.point;
			lineRenderer.SetPosition(0, transform.position);
			lineRenderer.SetPosition(1, LazerHit.position);
			//Want to make a lazer particle in the future 
			//For making the lazer create a particle
			/*if (LazerFx != null)
			{
				Transform LazerBurst = PoolManager.Pools["CoinTextPool"].Spawn(LazerFx.transform, LazerHit.position, Quaternion.LookRotation(DirectionBetweenPoints));
			}*/

			HitObject = hit.transform.gameObject;
			if (HitObject.tag == "Player")
			{
				thePHM.HurtPlayer(LazerDamage);
			}
		}

		
	}

	public Vector3 LerpByDistance(Vector3 A, Vector3 B, float x)
	{
		Vector3 P = x * Vector3.Normalize(B - A) + A;
		return P;
	}
}
