using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{

	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.
	public AudioClip[] jumpClips;	        // Array of clips for when the player jumps.
	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	//private bool grounded = false;			// Whether or not the player is grounded.
	private Animator anim;					// Reference to the player's animator component.
	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.		
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.




	public float inputScale = 5;
	public float chargeRate = 2;
	public float bubbleCooldown = 0.2f;

	private int playerID = 1;
	private int playerInput = 0;
	//private Rigidbody rb;

	public GameObject directorPrefab;
	private GameObject director;
	public GameObject bubblePrefab;
	private ProgressBar bar;

	private bool rightTriggerLastFrame = false;

	public bool applyJumpOnImpact;
	public float jumpTime;

	//private bool playerActive = false;

	void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("groundCheck");
		anim = transform.Find("SpriteObject").GetComponent<Animator>();
	}

	// Use this for initialization
	void Start () 
	{
		//rb = GetComponent<Rigidbody> ();
		bar = GetComponent<ProgressBar> ();
		bar.ProgressOverTime (0, 0);
		director = Instantiate(directorPrefab, transform.position, Quaternion.identity) as GameObject;
		director.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		//grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));  
		
		// If the jump button is pressed and the player is grounded then the player should jump.
		//if (Input.GetButtonDown ("Jump") && grounded)
		/*if (Input.GetButtonDown("Jump")) 
		{
			jump = true;
		}*/
		
		string input = null;
		if(playerInput != 0)
		{
			input += playerInput;
		}

		InputRotate (input);

		InputChargeBubble (input);

		InputBlowBubble (input);

	}

	void FixedUpdate()
	{
		string input = null;
		if(playerInput != 0)
		{
			input += playerInput;
		}
        
		InputMove (input);

		InputJump (input);

		

		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
	/*	if (moveHorizontal * rb.velocity.x < maxSpeed) {
			// ... add a force to the player.
			rb.AddForce (Vector2.right * moveHorizontal * moveForce);
		}
		
		// If the player's horizontal velocity is greater than the maxSpeed...
		if (Mathf.Abs (rb.velocity.x) > maxSpeed) {
			// ... set the player's velocity to the maxSpeed in the x axis.
			rb.velocity = new Vector2 (Mathf.Sign (rb.velocity.x) * maxSpeed, rb.velocity.y);
			anim.SetTrigger ("playerRun");
		
		}*/

		// If the input is moving the player right and the player is facing left...
		/*if (moveHorizontal > 0 && !facingRight) {
			// ... flip the player.
			Flip ();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (moveHorizontal < 0 && facingRight) {
			// ... flip the player.
			Flip ();
		}*/
		//float moveVertical = Input.GetAxis ("Vertical" + input);
		//GetComponent<bubbleInteraction>().addAcceleration(new Vector3(moveHorizontal*inputScale, 0 , 0));


       // if (jump)
        //{
           // GetComponent<bubbleInteraction>().addAcceleration(new Vector3(0, 100, 0));
           // jump = false;
       // }
		/*if(jump)
		{
			// Set the Jump animator trigger parameter.
			anim.SetTrigger("playerJump");
			
			// Play a random jump audio clip.
			int i = Random.Range(0, jumpClips.Length);
			AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

			GetComponent<bubbleInteraction>().addAcceleration(new Vector3(0, 100, 0));

			// Add a vertical force to the player.
			rb.AddForce(new Vector2(0f, jumpForce));
			
			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;
		}*/
		//Vector3 movement = new Vector3 (moveHorizontal, 0.0f, 0.0f);
		//rb.velocity = movement * speed; //may need a delta.time
	}

	void InputMove(string _controller)
	{
		float moveHorizontal = Input.GetAxis ("Horizontal" + _controller);
        
        if (moveHorizontal != 0.0f)
        { 
            GetComponent<bubbleInteraction>().addAcceleration(new Vector3(moveHorizontal * inputScale, 0, 0));
            GetComponent<bubbleInteraction>().inputRecieved = true;

        }
        else
        {
            GetComponent<bubbleInteraction>().inputRecieved = false;
        }
		
		//GetComponent<bubbleInteraction>().addAcceleration(new Vector3(moveHorizontal*inputScale, 0 , 0));

		if (moveHorizontal > 0 && !facingRight) 
		{
			// ... flip the player.
			Flip ();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (moveHorizontal < 0 && facingRight) 
		{
			// ... flip the player.
			Flip ();
		}

		// The Speed animator parameter is set to the absolute value of the horizontal input.
		anim.SetFloat("Speed", Mathf.Abs(moveHorizontal));
	}

	void InputRotate(string _controller)
	{
		//Managing the aim direction using right thumbsticks
		float moveHorizontal = Input.GetAxis ("HorizontalRight" + _controller);
		float moveVertical = Input.GetAxis ("VerticalRight" + _controller);

		//This is a fix for the rotater for when the scale causes it to flip
		//if (facingRight == false) 
		//{
			//moveHorizontal *= -1;
		//}

		if(moveHorizontal != 0 || moveVertical != 0)
		{
			float angle = Mathf.Atan2 (-moveHorizontal, moveVertical) * Mathf.Rad2Deg;
			director.transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
		}
	}

	void InputJump(string _controller)
	{
		if (jumpTime > 0.0f) 
		{
			jumpTime = jumpTime - Time.deltaTime;
		} 
		else 
		{
			applyJumpOnImpact = false;
		}

		bool canJump = GetComponentInChildren<JumpReset> ().GetCanJump();
		if (Input.GetButton ("Jump" + _controller) && canJump == true)
		{
			applyJumpOnImpact =true;
			jumpTime = 	0.25f;
		}
	}

	void InputChargeBubble(string _controller)
	{
		float rightTrigger = Input.GetAxis ("RightTrigger" + _controller);
		if (rightTrigger > 0) 
		{
			rightTriggerLastFrame = true;
			//Only fire when not on cooldown
			if(bar.ProgressOvertimeFinished() == true)
			{
				bar.IncreaseProgress (chargeRate * Time.deltaTime);
			}
		} 
		else 
		{
			//If was holding right trigger and now have released (This is axis not button otherwise would have used GetButtonUp)
			if(rightTriggerLastFrame == true)
			{
				//!!!FIRE THE BUBBLE!!!//
				Vector3 direction = director.transform.rotation * Vector3.up;
				SpawnBubble (transform.position, direction);
				bar.ProgressOverTime(0, bubbleCooldown); //The progress bar is sorta managing the cooldown...ah well
			}
			rightTriggerLastFrame = false;
		}

	}

	void InputBlowBubble(string _controller)
	{
		float leftTrigger = Input.GetAxis ("LeftTrigger" + _controller);
		if (leftTrigger > 0) 
		{
			
		}
	}

	void SpawnBubble(Vector3 _origin, Vector3 _direction)
	{
		GameObject bubble = Instantiate (bubblePrefab, Vector3.zero, Quaternion.identity) as GameObject;
		bubble.GetComponent<Bubble> ().AssignParameters (bar.GetProgress(), _origin, _direction);
	}

	void OnCollisionEnter(Collision col)
	{
		if (applyJumpOnImpact == true) 
		{
			GetComponent<bubbleInteraction> ().addAcceleration (new Vector3 (0, 60, 0));

			anim.SetTrigger("playerJump");
			
			// Play a random jump audio clip.
			//int i = Random.Range(0, jumpClips.Length);
			//AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

			applyJumpOnImpact = false;
		}
	}

	void OnCollisionStay (Collision col)
	{
		if (applyJumpOnImpact == true) 
		{
			GetComponent<bubbleInteraction> ().addAcceleration (new Vector3 (0, 60, 0));

			anim.SetTrigger("playerJump");
			
			// Play a random jump audio clip.
			//int i = Random.Range(0, jumpClips.Length);
			//AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

			applyJumpOnImpact = false;
		}
	}

	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.Find("SpriteObject").transform.localScale;
		theScale.x *= -1;
		transform.Find("SpriteObject").transform.localScale = theScale;
	}

	public void SetupPlayerID(int ID, int input)
	{
		playerID = ID;
		playerInput = input;
	}

    // needed to get the playerID for showing no. of deaths (couldn't think of a better way?)
    public int returnPlayerID()
    {
        return playerID;
    }
}
