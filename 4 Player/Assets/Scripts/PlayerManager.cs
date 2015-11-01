using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour 
{
	private int MAX_PLAYERS; //This needs setting back to private, I presume it was changed for testing but it breaks stuff
	private GameObject[] players;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void SetupPlayers()
	{
		MAX_PLAYERS = GameObject.FindGameObjectWithTag("GlobalConstant").GetComponent<ConstantData>().MAX_PLAYERS;
		players = new GameObject[MAX_PLAYERS];
		
		GameObject prefab = GameObject.FindGameObjectWithTag ("GlobalConstant").GetComponent<ConstantData> ().playerPrefab;
		for (int i = 0; i < MAX_PLAYERS; ++i) 
		{
			int playerController = GameObject.FindGameObjectWithTag ("GlobalConstant").GetComponent<ConstantData> ().playerController[i];
			if(playerController <= MAX_PLAYERS)
			{
				Vector3 spawnPoint = new Vector3(-30+(i*20),30,0);
				players[i] = Instantiate(prefab, spawnPoint, Quaternion.identity) as GameObject;
				players[i].GetComponent<Player>().SetupPlayerID(i + 1, playerController);
				players[i].GetComponent<Player>().SetMovementIsPaused(true);
				GetComponent<LevelStart>().SpawnStartBubble(spawnPoint, i);
			}
		}
	}

	//false allows everything to move, true prevents all from moving
	public void SetAllPlayerMovement(bool _b)
	{
		//Start the bubbles decay timer
		for(int i = 0; i < MAX_PLAYERS; ++i)
		{
			if(	players[i] != null)
			{
				players[i].GetComponent<Player>().SetMovementIsPaused(_b);
			}
		}
	}
}
