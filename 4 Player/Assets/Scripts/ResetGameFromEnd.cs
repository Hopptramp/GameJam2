using UnityEngine;
using UnityEngine.UI;	
using System.Collections;
using System.Linq;

public class ResetGameFromEnd : MonoBehaviour 
{
	int MAX_PLAYERS;
	private int[] playerController;

	//public int temp = 0;
	public bool[] allReady;
	public Text[] ready;
	int activePlayers = 0;
	bool allPlayersReady = false;
	public Text restartText; 

	private float timeOnReady;

	// Use this for initialization
	void Start () 
	{
		MAX_PLAYERS = GameObject.FindGameObjectWithTag("GlobalConstant").GetComponent<ConstantData>().MAX_PLAYERS;
		playerController = GameObject.FindGameObjectWithTag ("GlobalConstant").GetComponent<ConstantData> ().playerController;


		// find how many actuve players are there
		for (int i = 0; i < MAX_PLAYERS; ++i) 
		{
			if ( playerController[i] != MAX_PLAYERS+1)
			{
				++activePlayers;
			}
		}
		//create a bool array using active players
		//allReady = new bool[activePlayers];
		allReady = new bool[MAX_PLAYERS];

		for (int i = 0; i < allReady.Length; ++i)
		{
			allReady[i] = false;
		}

		// preset the text to blank
		for (int i = 0; i < MAX_PLAYERS; ++i)
		{
			ready[i].text = " ";
		}
	}

	// Update is called once per frame
	void Update () 
	{
		for (int i = 0; i < MAX_PLAYERS; ++i) 
		{
			//If a player presses start
			if (Input.GetButton ("Start" + (i + 1))) 
			{
				int playerID = FindPlayerFromConroller(i);
				if(playerID != 5)
				{
					ready[playerID].text = "ready";
					ready[playerID].color = GameObject.FindGameObjectWithTag("GlobalConstant").GetComponent<ConstantData>().playerColours[i];
					allReady[i] = true;
				}
			}
			else if(Input.GetButton ("Back" + (i + 1))) 
			{
				int playerID = FindPlayerFromConroller(i);
				if(playerID != 5)
				{
					ready[playerID].text = " ";
					allReady[i] = false;
					restartText.text = "play again? press start";
					allPlayersReady = false;
				}
			}
		}

		int temp = 0;
		for (int j = 0; j < MAX_PLAYERS; ++j)
		{
			if(allReady[j])
			{
				++temp;
				if(temp == activePlayers)
				{
					if (!allPlayersReady)
					{
						timeOnReady = Time.realtimeSinceStartup;
						allPlayersReady = true;
					}
					int readyTime = 3;

					//------------------------------------------------------------------------------------------------------
					string name = "game restarting in " + Mathf.Ceil(readyTime - (Time.realtimeSinceStartup - timeOnReady));

					restartText.text = name;

					if(readyTime < (Time.realtimeSinceStartup - timeOnReady))
					{
						resetLevel();
					}
				}
			}
		}
		if ( Input.GetButton ("B"))
		{
			Application.LoadLevel("Menu");
		}
	}


	void resetLevel()
	{
		int[] playerDeaths = GameObject.FindGameObjectWithTag ("GlobalConstant").GetComponent<ConstantData> ().playerDeaths;

		for (int i = 0; i < playerDeaths.Length; ++i) 
		{
			GameObject.FindGameObjectWithTag ("GlobalConstant").GetComponent<ConstantData> ().playerDeaths[i] = 0;
		}

		Application.LoadLevel ("Empty 4 Player Test");
	}

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
}
