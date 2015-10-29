using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float inputScale = 5;
	public float chargeRate = 2;
	public float bubbleCooldown = 0.2f;

	private int playerID = 1;
	private int playerInput = 0;
	private Rigidbody rb;
	public GameObject directorPrefab;
	private GameObject director;
	public GameObject bubblePrefab;
	private ProgressBar bar;

	private bool rightTriggerLastFrame = false;

	public bool applyJumpOnImpact;
	public float jumpTime;

	//private bool playerActive = false;


	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		bar = GetComponent<ProgressBar> ();
		bar.ProgressOverTime (0, 0);
		director = Instantiate(directorPrefab, transform.position, Quaternion.identity) as GameObject;
		director.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (jumpTime > 0.0f) 
		{
			jumpTime = jumpTime - Time.deltaTime;
		} 
		else 
		{
			applyJumpOnImpact = false;
		}
		string input = null;
		if(playerInput != 0)
		{
			input += playerInput;
		}

		//Managing the aim direction using right thumbsticks
		float moveHorizontal2 = Input.GetAxis ("HorizontalRight" + input);
		float moveVertical2 = Input.GetAxis ("VerticalRight" + input);
		if(moveHorizontal2 != 0 || moveVertical2 != 0)
		{
			float angle = Mathf.Atan2 (-moveHorizontal2, moveVertical2) * Mathf.Rad2Deg;
			director.transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
		}

		float rightTrigger = Input.GetAxis ("RightTrigger" + input);
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
				bar.ProgressOverTime(0, bubbleCooldown); //The progress bar is sorta managing the cooldown...ah well
			}
			rightTriggerLastFrame = false;
		}

		float leftTrigger = Input.GetAxis ("LeftTrigger" + input);
		if (leftTrigger > 0) 
		{

		}
	}

	void FixedUpdate()
	{
		string input = null;
		if(playerInput != 0)
		{
			input += playerInput;
		}
        
		float moveHorizontal = Input.GetAxis ("Horizontal" + input);

		GetComponent<bubbleInteraction>().addAcceleration(new Vector3(moveHorizontal*inputScale, 0 , 0));

		bool canJump = GetComponentInChildren<JumpReset> ().GetCanJump();
		if (Input.GetButton ("Jump" + input) && canJump == true)
		{
			applyJumpOnImpact =true;
			jumpTime = 	0.25f;
		}

		//Vector3 movement = new Vector3 (moveHorizontal, 0.0f, 0.0f);
		//rb.velocity = movement * speed; //may need a delta.time
	}

	void OnCollisionEnter(Collision col)
	{
		if (applyJumpOnImpact == true) 
		{
			GetComponent<bubbleInteraction> ().addAcceleration (new Vector3 (0, 60, 0));
			applyJumpOnImpact = false;
		}
	}

	void OnCollisionStay (Collision col)
	{
		if (applyJumpOnImpact == true) 
		{
			GetComponent<bubbleInteraction> ().addAcceleration (new Vector3 (0, 60, 0));
			applyJumpOnImpact = false;
		}
	}



	public void SetupPlayerID(int ID, int input)
	{
		playerID = ID;
		playerInput = input;
	}
}
