using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayersReadyTut : MonoBehaviour {


    int MAX_PLAYERS;
    private int[] playerController;

    public bool[] allReady;
    public Text[] displayActivePlayers;
    public Text[] ready;
    int activePlayers = 0;
    bool allPlayersReady = false;
    public Text startGameText;


    // Use this for initialization
    void Start ()
    {
        MAX_PLAYERS = GameObject.FindGameObjectWithTag("GlobalConstant").GetComponent<ConstantData>().MAX_PLAYERS;
        playerController = GameObject.FindGameObjectWithTag("GlobalConstant").GetComponent<ConstantData>().playerController;

        // find how many actuve players are there
        for (int i = 0; i < MAX_PLAYERS; ++i)
        {
            if (playerController[i] != MAX_PLAYERS + 1)
            {
                ++activePlayers;
            }
        }
        for (int i = 0; i < MAX_PLAYERS; ++i)
        {
            if (playerController[i] == 5)
            {
                displayActivePlayers[i].text = " ";
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
	void Update () {

        for (int i = 0; i < MAX_PLAYERS; ++i)
        {
            //If a player presses start
            if (Input.GetButton("Start" + (i + 1)))
            {
                int playerID = FindPlayerFromConroller(i);
                if (playerID != 5)
                {
                    ready[playerID].text = "ready";
                    ready[playerID].color = GameObject.FindGameObjectWithTag("GlobalConstant").GetComponent<ConstantData>().playerColours[i];
                    allReady[i] = true;
                }
            }
            else if (Input.GetButton("Back" + (i + 1)))
            {
                int playerID = FindPlayerFromConroller(i);
                if (playerID != 5)
                {
                    ready[playerID].text = " ";
                    allReady[i] = false;
                    //startGameText.text = "play again? press start";
                    allPlayersReady = false;
                }
            }
        }

        int temp = 0;
        for (int j = 0; j < MAX_PLAYERS; ++j)
        {
            if (allReady[j])
            {
                ++temp;
                if (temp == activePlayers)
                {
                    if (!allPlayersReady)
                    {
                        
                        allPlayersReady = true;
                    }
                   
                    //------------------------------------------------------------------------------------------------------
                    
                    if (allPlayersReady)
                    {
                        startGame();
                    }
                }
            }
        }

    }

    int FindPlayerFromConroller(int controller)
    {
        for (int i = 0; i < MAX_PLAYERS; ++i)
        {
            if (playerController[i] == controller + 1)
            {
                return i;
            }
        }
        return MAX_PLAYERS;
    }
    void startGame()
    {
        Application.LoadLevel("Empty 4 Player Test");
    }
}
