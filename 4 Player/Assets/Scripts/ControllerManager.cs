using UnityEngine;
using System.Collections;

public class ControllerManager : MonoBehaviour 
{
	private int MAX_PLAYERS;
	private bool[] activeControllers;
	private int[] playerController;
	private bool[] playerReady;

	public int readyTime = 5;
	private float timeOnReady;
	private bool allPlayersReady = false;

	// Use this for initialization
	void Start () 
	{
		MAX_PLAYERS = GameObject.FindGameObjectWithTag("GlobalConstant").GetComponent<ConstantData>().MAX_PLAYERS;
		//Setting up initial arrays
		activeControllers = new bool[MAX_PLAYERS];
		playerController = new int[MAX_PLAYERS];
		playerReady = new bool[MAX_PLAYERS];
		for(int i = 0; i < MAX_PLAYERS; ++i)
		{
			playerController[i] = MAX_PLAYERS + 1;
			activeControllers[i] = false;
			playerReady[i] = false;
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		//Loop for through the maximum possible input methods
		for (int i = 0; i < MAX_PLAYERS; ++i) 
		{
			//If a player presses start
			if (Input.GetButtonDown ("Start" + (i + 1))) 
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
						string text = "player " + (playerNumber + 1) + "\nnot ready \n press 'a'";
						GetComponent<ReadyMenu>().ChangeText(playerNumber, text);
						GetComponent<ReadyMenu>().ChangeImage(playerNumber, i);
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
			else if (Input.GetButtonDown ("Back" + (i + 1))) 
			{
				if(activeControllers[i] == true)
				{
					if(playerReady[i] == true)
					{
						playerReady[i] = false;
						/////UpdateText/////
						int player = FindPlayerFromConroller(i);
						if(player != MAX_PLAYERS)
						{
							string text = "player " + (player + 1) + "\nnot ready \n press 'a'";
							GetComponent<ReadyMenu>().ChangeText(player, text);
						}
					}
					else
					{
						//Finds the corresponding player from the control method
						int player = FindPlayerFromConroller(i);
						if(player != MAX_PLAYERS)
						{
							//Set that player as having no control method 
							playerController[player] = MAX_PLAYERS + 1;
							string text = "player " + (player + 1) + "\npress start";
							GetComponent<ReadyMenu>().ChangeText(player, text);
							GetComponent<ReadyMenu>().ChangeImage(player);
							//Set that this control method is not active
							activeControllers[i] = false;
						}
					}
				}
			}
			else if (Input.GetButtonDown ("A" + (i + 1))) 
			{
				if(activeControllers[i] == true)
				{
					if(playerReady[i] == false)
					{
						playerReady[i] = true;
						int player = FindPlayerFromConroller(i);
						if(player != MAX_PLAYERS)
						{
							string text = "player " + (player + 1) + "\nready";
							GetComponent<ReadyMenu>().ChangeText(player, text);
						}
					}
				}
			}
		}

		CheckPlayersReady ();
	}

	void CheckPlayersReady ()
	{
		bool ready = true;
		int numOfPlayers = 0;
		for (int i = 0; i < MAX_PLAYERS; ++i) 
		{
			if(activeControllers[i] == true)
			{
				if(playerReady[i] != true)
				{
					ready = false;
					GetComponent<ReadyMenu>().ChangeGameStartText("waiting for all players to ready up");
				}
			}
			else
			{
				++numOfPlayers;
			}
		}
		if (numOfPlayers == MAX_PLAYERS) 
		{
			ready = false;
			GetComponent<ReadyMenu>().ChangeGameStartText("waiting for all players to ready up");
		}

		if (ready == true) 
		{
			string name = "game starting in " + Mathf.Ceil(readyTime - (Time.realtimeSinceStartup - timeOnReady));
			GetComponent<ReadyMenu>().ChangeGameStartText(name);
			if(allPlayersReady == false)
			{
				timeOnReady = Time.realtimeSinceStartup;
				allPlayersReady = true;
			}
			else
			{
				if(readyTime < (Time.realtimeSinceStartup - timeOnReady))
				{
					///Countdown text
					GetComponent<ReadyMenu>().ChangeToScene("Tutorial");
				}
			}
		} 
		else 
		{
			allPlayersReady = false;
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
