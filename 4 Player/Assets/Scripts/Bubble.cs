using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour 
{
	// physics parameters
	public float bounceFactor = 1.0f;
	public Vector3 vel = Vector3.zero;
	public Vector3 testAcc = Vector3.zero;
	Vector3 acc = Vector3.zero;
	public float drag = 0.1f; 
	float dt;
	public float speed = 1.0f;
	public bool velReversed; 

	// spawn/decay param
	public float lifetime = 10.0f;
	public float size = 1;




	// Use this for initialization
	void Start ()
	{
		
	}

	void Update()
	{
		reduceLifetime (Time.deltaTime);
		// if the lifetime runs out
		if (lifetime <= 0.0f)
		{
			destroyBubble();
		}
		velReversed = false;
	}

	// ----------------------------------------------------------------------------------------
	// Spawning code (below)

	// use this function to assign all parameters of bubble from player
	public void assignParameters(float _lifetime, float _size)
	{
		size = _size;
		lifetime = _lifetime;
		transform.localScale = transform.localScale * size;
	}




	// ----------------------------------------------------------------------------------------
	// Decay/death code (below)

	// destroy the object
	void destroyBubble()
	{
		DestroyObject (gameObject);

		// sound effect of bubble popping?
	}

	void reduceLifetime(float reduction)
	{
		lifetime = lifetime - reduction;
	}

	// ----------------------------------------------------------------------------------------
	// physics code (below)


	void OnCollisionEnter (Collision col)
	{
		if (col.transform.gameObject.tag == "Player") 
		{
			reduceLifetime (2.0f);
		} 
		else
		{
			if(col.transform.gameObject.tag == "Bubble")
			{
				float testbounce; 
				testbounce = col.transform.gameObject.GetComponent<Bubble>().size / size;

				ContactPoint contact = col.contacts [0];
				vel =  testbounce * findBounceVel (contact);

				if (Vector3.Distance(Vector3.zero, vel) <0.5f)				
				{
					if (!col.transform.gameObject.GetComponent<Bubble>().velReversed)
					{
						vel = col.transform.gameObject.GetComponent<Bubble>().vel * testbounce;
					}
					else
					{
						vel = col.transform.gameObject.GetComponent<Bubble>().vel * - testbounce;
					}
				}

				velReversed = true; 

			}
			else
			{
				ContactPoint contact = col.contacts [0];
				vel = findBounceVel (contact);
			}

		}
	}

	Vector3 findBounceVel(ContactPoint col)
	{
		Vector3 reflectionVector = bounceFactor * (-2 * (Vector3.Dot (vel, col.normal)) * col.normal + vel);
		return reflectionVector;
	}


	// Update is called once per frame
	void FixedUpdate () 
	{
		dt = Time.deltaTime;
		acc = testAcc;

		addAcceleration (-(drag * vel));

		acc = acc * speed;
		UpdatePos ();
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
