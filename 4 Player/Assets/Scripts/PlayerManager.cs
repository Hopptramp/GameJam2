using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour 
{
	private int MAX_PLAYERS; //This needs setting back to private, I presume it was changed for testing but it breaks stuff
	private GameObject[] players;
	//how many extra bubbles created in level
	public int numberOfBubbles = 2;
	int activePlayers = 0;
	Vector3[] record;
	
	// Use this for initialization
	void Start () 
	{
		MAX_PLAYERS = GameObject.FindGameObjectWithTag("GlobalConstant").GetComponent<ConstantData>().MAX_PLAYERS;

		// find how many actuve players are there
		for (int i = 0; i < MAX_PLAYERS; ++i) 
		{
			if ( GameObject.FindGameObjectWithTag("GlobalConstant").GetComponent<ConstantData>().playerController[i] != MAX_PLAYERS+1)
			{
				++activePlayers;
			}
		}

		record = new Vector3[numberOfBubbles * activePlayers];
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void SetupPlayers()
	{

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
				GetComponent<LevelStart>().SetupLevelColours(i + 1, playerController - 1);
				RuntimeAnimatorController aniControl = GameObject.FindGameObjectWithTag ("GlobalConstant").GetComponent<ConstantData> ().animatorControllers [playerController - 1];
				players [i].transform.Find ("SpriteObject").GetComponent<Animator> ().runtimeAnimatorController = aniControl;
			}
		}
		for (int j = 0; j < numberOfBubbles * activePlayers; ++j)
		{
			Vector3 position;
			if(j == 0)
			{
				position = new Vector3(Random.Range(-40, 40), Random.Range(30, 15), 0);
			}
			else
			{
				do 
				{
					position = new Vector3(Random.Range(-40, 40), Random.Range(30, 15));
				} while (checkCollision(position) == false);
			}
			record[j] = position;
			
			GetComponent<LevelStart>().SpawnStartBubblenotPlayer(position, j);
		}
	}

	bool checkCollision(Vector3 position)
	{
		for(int i = 0; i < record.Length; ++i)
		{
			if( Vector3.Distance(record[i], position) < 10)
			{
				return false;
			}
		}
		return true;
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
