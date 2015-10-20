using UnityEngine;
using System.Collections;

public class playerReady : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		// Get references to players
		GameObject player1 = GameObject.Find ("Player1");
		GameObject player2 = GameObject.Find ("Player2");
		GameObject player3 = GameObject.Find ("Player3");
		GameObject player4 = GameObject.Find ("Player4");

	}
	
	// Update is called once per frame
	void Update () 
	{
		// if player1 presses start
			// change playerOneStart text to say playerOneReady
			// add player1 to list/array with players
			// set bool for player1 in true

		// if player1 is in (bool)
		//  	if player1 presses back
		//			change text back to playerOneStart
		//			remove player1 from list/array
		//			set bool for player1 in false

		// repeat for each player

		// if array.length >= 2 
		//		make start button visible
		//
	}
}
