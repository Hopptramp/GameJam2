using UnityEngine;
using System.Collections;

public class bubbleInteraction : MonoBehaviour 
{

	// physics parameters

	float bounceFactor = 1.0f;
    public float bounceDivisor = 2.0f;
	public Vector3 vel = Vector3.zero;
	public Vector3 acc = Vector3.zero;
	public float drag = 0.1f; 
	float dt;
	public float speed = 1.0f;
	Vector3 grav = new Vector3(0, -1, 0);
	public float gravMod = 1.0f;
   
   

    public float dragX = 0.5f;
    public bool inputRecieved = false;

    public bool disableDrag = false;
    public float dragDisableTime;
           

    public float MAX_X_SPEED = 30.0f;
    public float MAX_Y_SPEED = 45.0f;

	float prevY;

	private bool movementIsPaused = false;

	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (disableDrag)
        {
            if(dragDisableTime< 0.5f)
            {
                dragDisableTime += Time.deltaTime;
            }
            else
            {
                disableDrag = false;
            }
        }
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.transform.gameObject.tag == "Bubble") 
		{
			ContactPoint contact = col.contacts [0];
            Bubble bubble = col.transform.gameObject.GetComponent<Bubble>();
            if (bubble.size != 0.5f)
            {
                bounceFactor = bubble.size / bounceDivisor;
                vel = findBounceVel(contact);
                bounceFactor = 1;
                disableDrag = true;
                dragDisableTime = 0.0f;
            }
            

		} 
		else 
		{
			ContactPoint contact = col.contacts [0];
            bounceFactor = 0.5f;
			vel = findBounceVel (contact);
            bounceFactor = 1.0f;
		}
	}
	void OnCollisionStay(Collision col)
	{
		if (col.transform.gameObject.tag == "Bubble")
        {
            ContactPoint contact = col.contacts[0];
            float objectSize = col.transform.gameObject.GetComponent<Bubble>().size;
            bounceFactor = (objectSize /bounceDivisor) + 1.0f;
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

		if (movementIsPaused == false) 
		{
       
			if (!inputRecieved && !disableDrag)
            {
				addAcceleration (-(dragX * (new Vector3 (vel.x, 0, 0))));
                
			}

			addAcceleration (gravMod * grav);

			acc = acc * speed;
			UpdatePos ();
		}

	}
	
	void UpdatePos()
	{

        
        Vector3 newPos = transform.position + dt * vel;
        Vector3 newVel = vel + dt * acc;
        transform.position = newPos;
        vel = newVel;


        vel.x = Mathf.Clamp(vel.x, -MAX_X_SPEED, MAX_X_SPEED);
        vel.y = Mathf.Clamp(vel.y, -MAX_Y_SPEED, MAX_Y_SPEED);


        acc = Vector3.zero;

	}
	public void addAcceleration(Vector3 addition)
	{
		acc = acc + addition;
	}
    public void counterGrav ()
    {
        addAcceleration(-gravMod * grav);
    }

	public Vector3 GetAccel()
	{
		return acc;
	}

	public void SetMovementIsPaused(bool _b)
	{
		movementIsPaused = _b;
	}

    public void resetMovement()
    {
        vel = new Vector3(0, 0, 0);
        acc = new Vector3(0, 0, 0);
    }


}
