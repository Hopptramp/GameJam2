using UnityEngine;
using System.Collections;

public class LevelStart : MonoBehaviour 
{
	private int MAX_PLAYERS;
	private GameObject[] startBubbles;

	public GameObject bubblePrefab;

	private bool hasGameBegun = false;
	private bool isIntialSetupFinished = false;
	private float timeOnSetupFinished;
	public float startTimer = 3.0f;

	private bool isLevelScrolling = false;

	// Use this for initialization
	void Start () 
	{
		MAX_PLAYERS = GameObject.FindGameObjectWithTag("GlobalConstant").GetComponent<ConstantData>().MAX_PLAYERS;
		startBubbles = new GameObject[MAX_PLAYERS];
		GetComponent<PlayerManager> ().SetupPlayers ();
		isIntialSetupFinished = true;
		timeOnSetupFinished = Time.realtimeSinceStartup;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Once the initial positions of the players and bubbles have been set
		if (isIntialSetupFinished == true) 
		{
			//If the playable game has not yet started
			if(hasGameBegun == false)
			{
				//Determine when the starting timer has finished
				if(Time.realtimeSinceStartup - timeOnSetupFinished > startTimer)
				{
					//Start the bubbles decay timer
					for(int i = 0; i < MAX_PLAYERS; ++i)
					{
						if(	startBubbles[i] != null)
						{
							startBubbles[i].GetComponent<Bubble> ().SetLifetimeIsPaused (false);
							startBubbles[i].GetComponent<Bubble> ().SetMovementIsPaused (false);
						}
					}
					GetComponent<PlayerManager> ().SetAllPlayerMovement(false);
					string name = "HUDCanvas";
					GameObject.Find (name).GetComponent<HUDClock>().SetTimerPaused(false);
					hasGameBegun = true;
				}
			}
		}

		//If level hasn't begun scrolling yet
		if (isLevelScrolling == false) 
		{
			//And game has started
			if(hasGameBegun == true)
			{
				//When all starting bubbles have decayed start level scrolling
				bool isStartingBubble = false;
				for(int i = 0; i < MAX_PLAYERS; ++i)
				{
					if(	startBubbles[i] != null)
					{
						isStartingBubble = true;
						break;
					}
				}
				if(isStartingBubble == false)
				{
					isLevelScrolling = true;
					GameObject.Find ("Scroller").GetComponent<LevelScroller>().SetScrolling(true);
				}
			}
		}
	}

	public void SpawnStartBubble(Vector3 _location, int _player)
	{
		startBubbles[_player] = Instantiate (bubblePrefab, Vector3.zero, Quaternion.identity) as GameObject;
		Bubble bubble = startBubbles [_player].GetComponent<Bubble> ();
		bubble.AssignParameters (0.5f, _location,-Vector3.up);
		bubble.SetLifetimeIsPaused (true);
		bubble.SetMovementIsPaused (true);
	}


}
