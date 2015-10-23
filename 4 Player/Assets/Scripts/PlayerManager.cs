using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour 
{
	private const int MAX_PLAYERS = 4;
	private bool[] activeControllers;
	private int[] playerController;
	private GameObject[] players;
	public GameObject playerPrefab;

	// Use this for initialization
	void Start () 
	{
		//Setting up initial arrays
		activeControllers = new bool[MAX_PLAYERS];
		players = new GameObject[MAX_PLAYERS];
		playerController = new int[MAX_PLAYERS];
		for(int i = 0; i < MAX_PLAYERS; ++i)
		{
			playerController[i] = MAX_PLAYERS + 1;
		}

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

					//players[i] = Instantiate(playerPrefab, new Vector3(i*2, 0, i*2), Quaternion.identity) as GameObject;
					int playerNumber = GetNextFreePlayerSlot ();
					if(playerNumber != MAX_PLAYERS)
					{
						playerController[playerNumber] = i;
						//players[i].GetComponent<PlayerMovement>().SetupPlayerID(playerNumber+1);
						string text = "Player " + (playerNumber + 1) + "\nReady";
						GetComponent<ReadyMenu>().ChangeText(playerNumber, text);
						activeControllers[i] = true;
					}
					else
					{
						Debug.Log ("Max Players Reached");
					}
				}
			}
			//Else if player presses back then destroy player
			else if (Input.GetButton ("Back" + (i + 1))) 
			{
				if(activeControllers[i] == true)
				{
					//Destroy(players[i]);
					int player = FindPlayerFromConroller(i);
					if(player != MAX_PLAYERS)
					{
						playerController[player] = MAX_PLAYERS + 1;
						string text = "Player " + (player + 1) + "\nPress Start";
						GetComponent<ReadyMenu>().ChangeText(player, text);
						activeControllers[i] = false;
					}
				}
			}
		}
	}

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

	int FindPlayerFromConroller(int controller)
	{
		for(int i = 0; i < MAX_PLAYERS; ++i)
		{
			if(playerController[i] == controller)
			{
				return i;
			}
		}
		return MAX_PLAYERS;
	}
}
