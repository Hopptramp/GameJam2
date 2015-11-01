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
      

	}
	
	// Update is called once per frame
	void Update () 
    {

	}

	public void UpdatePlayerDeath(int _player, int _death)
	{
		string name = "HUDCanvas/DeathCount" + (_player);
		Text deathText = GameObject.Find (name).GetComponent<Text>();
		deathText.text = _death.ToString();

		// apply the player's death count to the Constant data script.
		string constData = "ConstantData";
		GameObject.Find (constData).GetComponent<ConstantData> ().playerDeaths [_player - 1] = _death;

	}
}
