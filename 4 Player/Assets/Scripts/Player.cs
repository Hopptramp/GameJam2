using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float speed = 5;
	private int playerID = 1;
	private int playerInput = 0;
	private Rigidbody rb;
	public GameObject directorPrefab;
	private GameObject director;
	public GameObject bubblePrefab;

	//private bool playerActive = false;


	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		director = Instantiate(directorPrefab, transform.position, Quaternion.identity) as GameObject;
		director.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		string input = null;
		if(playerInput != 0)
		{
			input += playerInput;
		}

		float moveHorizontal2 = Input.GetAxis ("HorizontalRight" + input);
		float moveVertical2 = Input.GetAxis ("VerticalRight" + input);
		if(moveHorizontal2 != 0 || moveVertical2 != 0)
		{
			float angle = Mathf.Atan2 (-moveHorizontal2, moveVertical2) * Mathf.Rad2Deg;
			director.transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
		}

		if (Input.GetButtonDown ("Jump" + input))
		{
			
		}

		float rightTrigger = Input.GetAxis ("RightTrigger" + input);
		if (rightTrigger > 0) 
		{

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
		//float moveVertical = Input.GetAxis ("Vertical" + input);
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, 0.0f);
		rb.velocity = movement * speed; //may need a delta.time
	}

	public void SetupPlayerID(int ID, int input)
	{
		playerID = ID;
		playerInput = input;
	}
}
