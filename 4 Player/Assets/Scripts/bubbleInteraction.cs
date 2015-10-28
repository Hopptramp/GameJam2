using UnityEngine;
using System.Collections;

public class bubbleInteraction : MonoBehaviour 
{

	// physics parameters

	public float bounceFactor = 1.0f;
	public Vector3 vel = Vector3.zero;
	Vector3 acc = Vector3.zero;
	public float drag = 0.1f; 
	float dt;
	public float speed = 1.0f;
	Vector3 grav = new Vector3(0, -1, 0);
	public float gravMod = 1.0f;

	float prevY;

	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.transform.gameObject.tag == "Bubble") 
		{
			ContactPoint contact = col.contacts [0];
			vel = findBounceVel (contact);
		} 
		else 
		{
			ContactPoint contact = col.contacts [0];
			vel = findBounceVel (contact);
		}
	}
	void OnCollisionStay(Collision col)
	{
		if ( col.transform.gameObject.tag != "Bubble") 
		{
			gravMod=0.0f;
			vel.y = 0;
		}
	}

	Vector3 findBounceVel(ContactPoint col)
	{
		Vector3 reflectionVector = bounceFactor * (-2 * (Vector3.Dot (vel, col.normal)) * col.normal + vel);
		return reflectionVector;
	}

	void FixedUpdate () 
	{
		dt = Time.deltaTime;
		
		addAcceleration (-(drag * vel));

		addAcceleration (gravMod * grav);

		acc = acc * speed;
		UpdatePos ();
		gravMod = 1.0f;
	

	}
	
	void UpdatePos()
	{
		Vector3 newPos = transform.position + dt * vel;
		Vector3 newVel = vel + dt * acc;
		transform.position = newPos;
		vel = newVel;
		acc = Vector3.zero;

	}
	public void addAcceleration(Vector3 addition)
	{
		acc = acc + addition;
	}
}
