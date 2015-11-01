using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

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
	private float startLifetime;
	public float size = 1;
	public float chargeMultiplier = 20.0f; //Max lifetime/scale by default is 1sec/0.5scale (value of 20 makes max 20sec/10scale)



    [SerializeField]float maxVelLifeReductionFactor = 40.0f;
    

	private ProgressBar bar;

	private bool frameSpawned = true;
	private int frameCounter = 0;

	private bool lifetimeIsPaused = false;
	private bool movementIsPaused = false;


	// Use this for initialization
	void Awake ()
	{
		bar = GetComponent<ProgressBar> ();
		startLifetime = lifetime;

		//Prevent bubbles from colliding with walls
		for (int i = 0; i < 3; ++i)
		{
			string name = "Scroller/Borders/Boundary" + (i + 1);
			Physics.IgnoreCollision (GameObject.Find (name).GetComponent<BoxCollider> (), GetComponent<SphereCollider> ());
		}
	}

	void Start()
	{
		GameObject.Find ("Sound").GetComponent<Sounds> ().bubblePop (size);
	}

	void Update()
	{
		reduceLifetime (Time.deltaTime);

		// if the lifetime runs out
		if (lifetime <= 0.0f)
		{
			GameObject.Find ("Sound").GetComponent<Sounds> ().bubblePop (size);
			destroyBubble();
		}
		velReversed = false;
	}

	// ----------------------------------------------------------------------------------------
	// Spawning code (below)

	// use this function to assign all parameters of bubble from player
	public void AssignParameters(float _charge, Vector3 _origin, Vector3 _direction)
	{
		//Scaled charge uses power of 3 graph to detrmine lifetime and size
		float scaledCharge = (((Mathf.Pow ((_charge-0.5f),3.0f)*5)+0.5f)*chargeMultiplier);

		float smallBubbleMultiplier = 1.0f;
		//Perhaps hard coded unique setup for the bullet balloons?
		if (_charge < 0.2) 
		{
			scaledCharge = 1;
			smallBubbleMultiplier = 20.0f;
            
		}

		lifetime = scaledCharge;
		startLifetime = lifetime;

		size = scaledCharge / 2;
		transform.localScale = transform.localScale * size;

		//Set up spawn distance from player based on size
		transform.position = _origin + _direction * Mathf.Lerp(2.0f, 8.0f, scaledCharge/chargeMultiplier);
		vel += (_direction * smallBubbleMultiplier);
		
		bar.ChangePosition (new Vector3 (transform.position.x, transform.position.y, -size / 2));
	
	
	}


    // Overload called for when the player is blowing at the same time.
    public void AssignParameters(float _charge, Vector3 _origin, Vector3 _direction, bool _blow)
    {
        //Scaled charge uses power of 3 graph to detrmine lifetime and size
        float scaledCharge = (((Mathf.Pow((_charge - 0.5f), 3.0f) * 5) + 0.5f) * chargeMultiplier);

        float smallBubbleMultiplier = 1.0f;
        //Perhaps hard coded unique setup for the bullet balloons?
        if (_charge < 0.2)
        {
            scaledCharge = 1;
            smallBubbleMultiplier = 20.0f;
            if (_blow)
            {
                smallBubbleMultiplier = 50.0f;
            }
        }

        lifetime = scaledCharge;
        startLifetime = lifetime;

        size = scaledCharge / 2;
        transform.localScale = transform.localScale * size;

        //Set up spawn distance from player based on size
        transform.position = _origin + _direction * Mathf.Lerp(2.0f, 8.0f, scaledCharge / chargeMultiplier);
        vel += (_direction * smallBubbleMultiplier);

        bar.ChangePosition(new Vector3(transform.position.x, transform.position.y, -size / 2));


    }







    // ----------------------------------------------------------------------------------------
    // Decay/death code (below)

    // destroy the object
    public void destroyBubble()
	{
		//GameObject.Find ("Sound").GetComponent<Sounds> ().bubblePop ();
		DestroyObject (gameObject);
	}

	void reduceLifetime(float reduction)
	{
		if (lifetimeIsPaused == false) 
		{
			lifetime = lifetime - reduction;
			bar.SetProgress (lifetime / startLifetime);
		}
	}

	// ----------------------------------------------------------------------------------------
	// physics code (below)


	void OnCollisionEnter (Collision col)
	{
		if (frameSpawned == true) 
		{
			GameObject other = col.transform.gameObject;
			if(other.tag == "Bubble")
			{
				if(size > other.GetComponent<Bubble>().size)
				{
					//other.GetComponent<Bubble>().destroyBubble();
					return;
				}
			}
		}

		if (col.transform.gameObject.tag == "Player") 
		{
			reduceLifetime (2.0f);
            ContactPoint contact = col.contacts[0];
            bubbleInteraction playersMovement = col.transform.gameObject.GetComponent<bubbleInteraction>();
            vel = playersMovement.vel / size;
            //vel = findBounceVel(contact);

        } 
		else
		{
			if(col.transform.gameObject.tag == "Bubble")
			{
				float testbounce;
                Bubble colScript = col.transform.gameObject.GetComponent<Bubble>();

                if (colScript.size == 0.5f)
                {
                    {
                        float max; 
                        if (Vector3.Distance(Vector3.zero, colScript.vel)> 40.0f)
                        {
                            max = maxVelLifeReductionFactor;
                        }
                        else
                        {
                            max = Vector3.Distance(Vector3.zero, colScript.vel);
                        }
                        float reduction = max / (maxVelLifeReductionFactor/5);
                        reduceLifetime(reduction);
                        colScript.destroyBubble();
                    }
                }

                testbounce = colScript.size / size;

				ContactPoint contact = col.contacts [0];
				vel =  testbounce * findBounceVel (contact);

				if (Vector3.Distance(Vector3.zero, vel) <0.5f)				
				{
					if (!colScript.velReversed)
					{
						vel = colScript.vel * testbounce;
					}
					else
					{
						vel = colScript.vel * - testbounce;
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

		//Down apply forces and movement while object is paused
		if (movementIsPaused == false) 
		{
			acc = testAcc;

			addAcceleration (-(drag * vel));

			acc = acc * speed;
			UpdatePos ();
		}

		//Determines whether it is the frame that an object just spawned
		if (frameSpawned == true) 
		{
			if(frameCounter > 1)
			{
				frameSpawned = false;
			}
			++frameCounter;
		}
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

	public void SetLifetimeIsPaused(bool _b)
	{
		lifetimeIsPaused = _b;
	}

	public void SetMovementIsPaused(bool _b)
	{
		movementIsPaused = _b;
	}


}
