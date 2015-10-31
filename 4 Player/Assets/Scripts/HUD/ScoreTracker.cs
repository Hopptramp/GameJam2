using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ScoreTracker : MonoBehaviour 
{
    //int MAX_PLAYERS;
    //public Text[] scoreboard;
   // int[] ingamePlayers;
   // int[] players;

	// Use this for initialization
	void Start () 
    {
        
       /* MAX_PLAYERS = GameObject.FindGameObjectWithTag("GlobalConstant").GetComponent<ConstantData>().MAX_PLAYERS;
        ingamePlayers = new int[MAX_PLAYERS];

        for (int i = 0; i < MAX_PLAYERS; ++i)
        {
            players[i] = GameObject.FindGameObjectWithTag("GlobalConstant").GetComponent<ConstantData>().playerController[i];
            ingamePlayers[i] = 0;
        }*/

        

	}
	
	// Update is called once per frame
	void Update () 
    {
        //iterate through the players 
       /* for (int i = 0; i < MAX_PLAYERS; ++i)
        {
            if (players[i] != 0) // i think that means inactive?
            {
                ingamePlayers[i] = players[i];
            }
        }
        Array.Sort(ingamePlayers);

        for (int i = 0; i < MAX_PLAYERS; ++i)
        {
            // write out the scores in game
            scoreboard[i].text = (i + 1) + "\n" + "P" + ingamePlayers[i];
        }*/
	}

	public void UpdatePlayerDeath(int _player, int _death)
	{
		string name = "HUDCanvas/DeathCount" + (_player);
		Text deathText = GameObject.Find (name).GetComponent<Text>();
		deathText.text = _death.ToString();

	}
}
