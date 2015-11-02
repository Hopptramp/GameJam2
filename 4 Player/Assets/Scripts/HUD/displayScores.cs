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
				if (playerDeaths[i] == 1)
				{
					displayDeaths[i].text = "P" + (i+1) + " - Died " + numberOfDeaths[i] + " time!";
				}
				else if (playerDeaths[i] > 0)
				{
					displayDeaths[i].text = "P" + (i+1) + " - Died " + numberOfDeaths[i] + " times!";
				}
				else 
				{
					displayDeaths[i].text = "P" + (i+1) + " Survived";
				}
				//assign the player's colour
				displayDeaths[i].color = GameObject.FindGameObjectWithTag("GlobalConstant").GetComponent<ConstantData>().playerColours[playerController[i] - 1];
			}
		}

		int maxValue = 0;
		int maxIndex = 20;

		int numOfPlayers = 0;
		for (int i = 0; i < MAX_PLAYERS; ++i) 
		{
			if (playerController[i] != 5)
			{
				++numOfPlayers;
			}
		}

		int[] temp = new int[numOfPlayers];

		int tempCounter = 0;
		for (int i = 0; i < MAX_PLAYERS; ++i) 
		{
			if (playerController[i] != 5)
			{
				temp[tempCounter] = numberOfDeaths[i];
				++tempCounter;
			}
		}


		for (int i = 0; i < numberOfDeaths.Length; ++i) 
		{
			if (playerController[i] != 5)
			{
				maxValue = Mathf.Min(temp);

				if(numberOfDeaths[i] == maxValue)
				{
					maxIndex = i;
				}
			}
		}

		Winner.text = "P" + (maxIndex + 1) + " WINS";
		Winner.color = GameObject.FindGameObjectWithTag("GlobalConstant").GetComponent<ConstantData>().playerColours[playerController[maxIndex] - 1];


	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
