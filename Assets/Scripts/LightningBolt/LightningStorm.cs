using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStorm : MonoBehaviour {
	/// <summary>
	/// private Camera theCamera;
	/// theCamera = GetComponent<Camera>();
	///theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, theCamera.transform.position.z);
	///Need to get a random X value in the range of the camera, and the Y value at the top of the camera
	///Choose that for our LightningBolt Start
	///Choose a value in a range of Y downwards.
	///Spawn a lightning bolt using these points
	/// </summary>
	
	//Prefabs to be assigned in Editor
	public GameObject BoltPrefab;
	public GameObject BranchPrefab;
	//For pooling
	List<GameObject> activeBoltsObj;
	List<GameObject> inactiveBoltsObj;
	int maxBolts = 1000;

	float scaleText;
	Vector2 positionText;

	public float bolttimer;
	public float timebetweenbolts;

	//Will contain all of the pieces for the branches
	List<GameObject> branchesObj;

	Vector2 pos1, pos2;

	//Our cameras width and height
	private float halfHeight;
	private float halfWidth;
	private CameraController theCameraController;
	private Camera theCamera;
	private Vector3 StartP;
	private Vector3 EndP;
	private float cameraCenterOffsetX;
	private float cameraCenterOffsetY;

	private SFXManager sfxMan;

	void Start()
	{
		sfxMan = FindObjectOfType<SFXManager>();
		///Use theCamera to find out our sizes and such.
		theCameraController = FindObjectOfType<CameraController>();
		theCamera = theCameraController.GetComponent<Camera>();
		halfHeight = theCamera.orthographicSize;
		halfWidth = halfHeight * Screen.width / Screen.height;

		bolttimer = timebetweenbolts;
		//Initialize lists
		activeBoltsObj = new List<GameObject>();
		inactiveBoltsObj = new List<GameObject>();
		branchesObj = new List<GameObject>();

		//Grab the parent we'll be assigning to our bolt pool
		GameObject p = GameObject.Find("LightningPoolHolder");

		//For however many bolts we've specified
		for (int i = 0; i < maxBolts; i++)
		{
			//create from our prefab
			GameObject bolt = (GameObject)Instantiate(BoltPrefab);

			//Assign parent
			bolt.transform.parent = p.transform;

			//Initialize our lightning with a preset number of max sexments
			bolt.GetComponent<LightningBolt>().Initialize(25);

			//Set inactive to start
			bolt.SetActive(false);

			//Store in our inactive list
			inactiveBoltsObj.Add(bolt);
		}
	}

	void Update()
	{
		bolttimer -= Time.deltaTime;
		//Declare variables for use later
		GameObject boltObj;
		LightningBolt boltComponent;

		//store off the count for effeciency
		int activeLineCount = activeBoltsObj.Count;

		//loop through active lines (backwards because we'll be removing from the list)
		for (int i = activeLineCount - 1; i >= 0; i--)
		{
			//pull GameObject
			boltObj = activeBoltsObj[i];

			//get the LightningBolt component
			boltComponent = boltObj.GetComponent<LightningBolt>();

			//if the bolt has faded out
			if (boltComponent.IsComplete)
			{
				//deactive the segments it contains
				boltComponent.DeactivateSegments();

				//set it inactive
				boltObj.SetActive(false);

				//move it to the inactive list
				activeBoltsObj.RemoveAt(i);
				inactiveBoltsObj.Add(boltObj);
			}
		}
		
		//If left mouse button pressed
		if (bolttimer<=0)
		{
			sfxMan.Lightning.Play();
			bolttimer = timebetweenbolts * Random.Range(1.0f,2.0f);
			///Here we need to say if our timer is up we get our camera locations and set up our start/end
			///THen add it to our active branches
			/////////////////////////////////////////////////////////////////////////////////////////////////
			///Under our update
			cameraCenterOffsetX = Random.Range(-1.0f, 1.0f);
			StartP = new Vector3
				(theCameraController.transform.position.x + (halfWidth * cameraCenterOffsetX),
				theCameraController.transform.position.y + halfHeight,
				theCameraController.transform.position.z);
			cameraCenterOffsetY = Random.Range(0.5f, 1.0f);
			EndP = new Vector3
				(theCameraController.transform.position.x + (halfWidth * cameraCenterOffsetX),
				theCameraController.transform.position.y * cameraCenterOffsetY,
				theCameraController.transform.position.z);
				//Store Start pos
				pos1 = new Vector2(StartP.x, StartP.y);
				//Store End pos
				pos2 = new Vector2(EndP.x, EndP.y);

						//instantiate from our branch prefab
						GameObject branchObj = (GameObject)GameObject.Instantiate(BranchPrefab);

						//get the branch component
						BranchLightning branchComponent = branchObj.GetComponent<BranchLightning>();

						//initialize the branch component using our position data
						branchComponent.Initialize(pos1, pos2, BoltPrefab);

						//add it to the list of active branches
						branchesObj.Add(branchObj);
						
	
		}

		//loop through any active branches
		for (int i = branchesObj.Count - 1; i >= 0; i--)
		{
			//pull the branch lightning component
			BranchLightning branchComponent = branchesObj[i].GetComponent<BranchLightning>();

			//If it's faded out already
			if (branchComponent.IsComplete)
			{
				//destroy it
				Destroy(branchesObj[i]);

				//take it out of our list
				branchesObj.RemoveAt(i);

				//move on to the next branch
				continue;
			}

			//draw and update the branch
			branchComponent.UpdateBranch();
			branchComponent.Draw();
		}

		//update and draw active bolts
		for (int i = 0; i < activeBoltsObj.Count; i++)
		{
			activeBoltsObj[i].GetComponent<LightningBolt>().UpdateBolt();
			activeBoltsObj[i].GetComponent<LightningBolt>().Draw();
		}
	}

	//calculate distance squared (no square root = performance boost)
	public float DistanceSquared(Vector2 a, Vector2 b)
	{
		return ((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
	}

}
