using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public float movementSpeed = 5;
	private int playerID = 1;

	//private bool playerActive = false;


	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//if(playerActive == true)
		//{
		//Basic movement
			float moveHorizontal = Input.GetAxis ("Horizontal" + playerID);
			float moveVertical = Input.GetAxis ("Vertical" + playerID);
			
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			transform.position += (movement * Time.deltaTime * movementSpeed);
		//}
	
	}

	public void SetupPlayerID(int ID)
	{
		playerID = ID;
	}
}
