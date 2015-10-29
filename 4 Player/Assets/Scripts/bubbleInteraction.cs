using UnityEngine;
using System.Collections;

public class bubbleInteraction : MonoBehaviour 
{

	// physics parameters

	public float bounceFactor = 1.0f;
    public float bounceDivisor = 2.0f;
	public Vector3 vel = Vector3.zero;
	Vector3 acc = Vector3.zero;
	public float drag = 0.1f; 
	float dt;
	public float speed = 1.0f;
	Vector3 grav = new Vector3(0, -1, 0);
	public float gravMod = 1.0f;
    public float dragX = 0.5f;

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
            bounceFactor = col.transform.gameObject.GetComponent<Bubble>().size / bounceDivisor;
            vel = findBounceVel (contact);
            bounceFactor = 1;
		} 
		else 
		{
			ContactPoint contact = col.contacts [0];
            bounceFactor = 0.1f;
			vel = findBounceVel (contact);
            bounceFactor = 1.0f;
		}
	}
	void OnCollisionStay(Collision col)
	{
		if ( col.transform.gameObject.tag != "Bubble") 
		{
			addAcceleration(-gravMod * grav);
		}
        else
        {
            ContactPoint contact = col.contacts[0];
            bounceFactor = col.transform.gameObject.GetComponent<Bubble>().size / bounceDivisor;
            vel = findBounceVel(contact);
            bounceFactor = 1;
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


        //addAcceleration (-(drag * vel));
        addAcceleration(-(dragX * (new Vector3(vel.x, 0 , 0))));

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
