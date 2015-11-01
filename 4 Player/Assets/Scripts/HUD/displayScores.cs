using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class displayScores : MonoBehaviour 
{
	public Text[] displayDeaths;
	public Text Winner;
	private int[] numberOfDeaths;

	// Use this for initialization
	void Start () 
	{
		int MAX_PLAYERS = GameObject.FindGameObjectWithTag("GlobalConstant").GetComponent<ConstantData>().MAX_PLAYERS;
		numberOfDeaths = new int[MAX_PLAYERS];
		int[] playerDeaths = GameObject.FindGameObjectWithTag("GlobalConstant").GetComponent<ConstantData>().playerDeaths;
		int[] playerController = GameObject.FindGameObjectWithTag("GlobalConstant").GetComponent<ConstantData>().playerController;

		// retrieve number of deaths
		for (int i = 0; i < MAX_PLAYERS; ++i) 
		{
			numberOfDeaths[i] = playerDeaths[i];
		}
		// sort the array in order
		//Array.Sort (numberOfDeaths);

		// apply the deaths to the text
		for (int i = 0; i < MAX_PLAYERS; ++i)
		{
			if ( playerController[i] == 5)
			{
				displayDeaths[i].text = " ";
			}
			else
			{
				displayDeaths[i].text = "P" + (i+1) + " - Died " + numberOfDeaths[i] + " times!";
			}
		}

		int maxValue = Mathf.Min(numberOfDeaths);
		int maxIndex = 0;

		for (int i = 0; i < numberOfDeaths.Length; ++i) 
		{
			if (playerController[i] != 5)
			{
				if(numberOfDeaths[i] == maxValue)
				{
					maxIndex = i;
				}
			}
		}

		Winner.text = "P" + (maxIndex + 1) + " WINNER";


	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
