using UnityEngine;
using System.Collections;

public class ControllerManager : MonoBehaviour 
{
	private int MAX_PLAYERS;
	private bool[] activeControllers;
	private int[] playerController;

	// Use this for initialization
	void Start () 
	{
		MAX_PLAYERS = GameObject.FindGameObjectWithTag("GlobalConstant").GetComponent<ConstantData>().MAX_PLAYERS;
		//Setting up initial arrays
		activeControllers = new bool[MAX_PLAYERS];
		playerController = new int[MAX_PLAYERS];
		for(int i = 0; i < MAX_PLAYERS; ++i)
		{
			playerController[i] = MAX_PLAYERS + 1;
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		//Loop for through the maximum possible input methods
		for (int i = 0; i < MAX_PLAYERS; ++i) 
		{
			//If a player presses start
			if (Input.GetButton ("Start" + (i + 1))) 
			{
				if(activeControllers[i] == false)
				{
					//Get next free open player slot
					int playerNumber = GetNextFreePlayerSlot ();
					//If there is a free player slot (MAX_PLAYERS means no free slot in this statement)
					if(playerNumber != MAX_PLAYERS)
					{
						//Setup player as using a specific control method
						playerController[playerNumber] = i + 1;
						string text = "Player " + (playerNumber + 1) + "\nReady";
						GetComponent<ReadyMenu>().ChangeText(playerNumber, text);
						//Set that this control method is now active
						activeControllers[i] = true;
					}
					else
					{
						Debug.Log ("Max Players Reached");
					}
				}
			}
			//Else if player presses back
			else if (Input.GetButton ("Back" + (i + 1))) 
			{
				if(activeControllers[i] == true)
				{
					//Finds the corresponding player from the control method
					int player = FindPlayerFromConroller(i);
					if(player != MAX_PLAYERS)
					{
						//Set that player as having no control method 
						playerController[player] = MAX_PLAYERS + 1;
						string text = "Player " + (player + 1) + "\nPress Start";
						GetComponent<ReadyMenu>().ChangeText(player, text);
						//Set that this control method is not active
						activeControllers[i] = false;
					}
				}
			}
		}
	}

	//Finds next free player slot
	int GetNextFreePlayerSlot()
	{
		for(int i = 0; i < MAX_PLAYERS ; ++i)
		{
			if(playerController[i] == MAX_PLAYERS + 1)
			{
				return i;
			}
		}
		return MAX_PLAYERS;
	}

	//Finds a player from a given control method
	int FindPlayerFromConroller(int controller)
	{
		for(int i = 0; i < MAX_PLAYERS; ++i)
		{
			if(playerController[i] == controller + 1)
			{
				return i;
			}
		}
		return MAX_PLAYERS;
	}

	//Updates constant data before changing scenes
	public void UpdateConstantData()
	{
		for(int i = 0; i < MAX_PLAYERS; ++i)
		{
			GameObject.FindGameObjectWithTag("GlobalConstant").GetComponent<ConstantData>().activeControllers[i] = activeControllers[i];
			GameObject.FindGameObjectWithTag("GlobalConstant").GetComponent<ConstantData>().playerController[i] = playerController[i];
		}
	}
}
