using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour 
{
	private const int MAX_PLAYERS = 4;
	private bool[] activeControllers;
	private GameObject[] players;
	public GameObject playerPrefab;

	// Use this for initialization
	void Start () 
	{
		//Setting up initial arrays
		activeControllers = new bool[MAX_PLAYERS];
		players = new GameObject[MAX_PLAYERS];
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Loop for through the maximum possible players
		for (int i = 0; i < MAX_PLAYERS; ++i) 
		{
			//If a player presses start then spawn player and set id
			if (Input.GetButton ("Start" + (i + 1))) 
			{
				if(activeControllers[i] == false)
				{
					players[i] = Instantiate(playerPrefab, new Vector3(i*2, 0, i*2), Quaternion.identity) as GameObject;
					players[i].GetComponent<PlayerMovement>().SetupPlayerID(i+1);
					activeControllers[i] = true;
				}
			}
			//Else if player presses back then destroy player
			else if (Input.GetButton ("Back" + (i + 1))) 
			{
				if(activeControllers[i] == true)
				{
					Destroy(players[i]);
					activeControllers[i] = false;
				}
			}
		}
	}
}
