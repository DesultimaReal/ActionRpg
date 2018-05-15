 using UnityEngine;
 using System.Collections;
 
 public class SinosoidalMotion : MonoBehaviour
{

	public float MoveSpeed;
	public float frequency;  // Speed of sine movement
	public float magnitude;   // Size of sine movement
	public Vector3 axis; //Direction of the wave ups and downs, we modify this depending on the direction our turret is shooting
	public Vector3 Dir; //Direction the fireball is actually going
	private Vector3 pos; //Its current position in the world
	public float SinosoidTracker;



	void OnSpawned()
	{
		Physics2D.IgnoreLayerCollision(17, 15);
		pos = transform.position;//This is consistently next to our facing direction
		SinosoidTracker = 0.0f;//Sets their sinosoids to all start at 0.
	}
	void Update()
	{
		SinosoidTracker+= 0.01f;
		pos += Dir * Time.deltaTime * MoveSpeed;//This will move us in a direction
		transform.position = pos + axis * Mathf.Sin(SinosoidTracker * frequency) * magnitude;//This uses our axis to create sinosoidal motion
	}
}
