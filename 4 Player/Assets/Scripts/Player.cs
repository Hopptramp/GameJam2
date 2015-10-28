using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float speed = 5;
	private int playerID = 1;
	private int playerInput = 0;
	private Rigidbody rb;

	//private bool playerActive = false;


	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		string input = null;
		if(playerInput != 0)
		{
			input += playerInput;
		}

		float moveHorizontal = Input.GetAxis ("Horizontal" + input);
		float moveVertical = Input.GetAxis ("Vertical" + input);
		
		Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0.0f);
		rb.velocity = movement * speed; //may need a delta.time

		//transform.position += (movement * Time.deltaTime * movementSpeed);
		if (Input.GetButtonDown ("Jump" + input))
		{

		}
	}

	public void SetupPlayerID(int ID, int input)
	{
		playerID = ID;
		playerInput = input;
	}
}
