using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float movementSpeed = 5;
	private int playerID = 1;
	private int playerInput = 0;

	//private bool playerActive = false;


	// Use this for initialization
	void Start () 
	{
	
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
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		transform.position += (movement * Time.deltaTime * movementSpeed);
	}

	public void SetupPlayerID(int ID, int input)
	{
		playerID = ID;
		playerInput = input;
	}
}
