using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HuD : MonoBehaviour 
{
    public int MAX_PLAYERS;

    public Text counterText;
    int[] players;

   public  float seconds, minutes;

    public GameObject prefab;

    public Text[] playerDeaths;

    float totalTime = 2;

    // Use this for initialization
    void Start()
    {
        MAX_PLAYERS = GameObject.FindGameObjectWithTag("GlobalConstant").GetComponent<ConstantData>().MAX_PLAYERS;
        for (int i = 0; i < MAX_PLAYERS; ++i)
        {
            players[i] = GameObject.FindGameObjectWithTag("GlobalConstant").GetComponent<ConstantData>().playerController[i];
        }

    }

    // Update is called once per frame
    void Update()
    {
        timer();
	
    }

    // adds the death to the counter shown at the top
    public void updateDeaths(int playerNumber, int deathNumber)
    {
        playerDeaths[playerNumber-1].text = "DeathCount P" + playerNumber + " - " + deathNumber;
    }

    // clock countdown
    void timer()
    {
        minutes = Mathf.Floor(totalTime - (Time.timeSinceLevelLoad / 60f));

        seconds = (int)60 - (Time.timeSinceLevelLoad % 60f);

        if (seconds >= 59)
        {
           counterText.text = (minutes+1).ToString("00") + ":" + ("00");
        }
        else
        {
            counterText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
        if (seconds <= 0)
        {
            seconds += 59f;
        }
    }
}