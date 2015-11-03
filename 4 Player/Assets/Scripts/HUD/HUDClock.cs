using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDClock : MonoBehaviour 
{
    private Text counterText;
    
	private float seconds;
	private int minutes;

	private bool isTimerPaused = true;

    public float totalTime = 60; //start time in seconds
	private float startTime;

	private LevelScroller scrollerScript;

    // Use this for initialization
    void Start()
    {
        //MAX_PLAYERS = GameObject.FindGameObjectWithTag("GlobalConstant").GetComponent<ConstantData>().MAX_PLAYERS;
		//players = new int[MAX_PLAYERS];
        //for (int i = 0; i < MAX_PLAYERS; ++i)
        //{
        //    players[i] = GameObject.FindGameObjectWithTag("GlobalConstant").GetComponent<ConstantData>().playerController[i];
        //}
		scrollerScript = GameObject.Find ("Scroller").GetComponent<LevelScroller>();
		startTime = totalTime;
		counterText = transform.Find ("Clock").GetComponent<Text> ();
		minutes = (int)Mathf.Floor(totalTime / 60f);
		seconds = totalTime % 60f;
		PutTimeToScreen ();


    }

	public void setTime(float time)
	{
		totalTime = time;
	}

    // Update is called once per frame
    void Update()
    {
		if(isTimerPaused == false)
		{
       		timer();
		}
    }

    // adds the death to the counter shown at the top
   // public void updateDeaths(int playerNumber, int deathNumber)
    //{
   //     playerDeaths[playerNumber-1].text = "DeathCount P" + playerNumber + " - " + deathNumber;
    //}

    // clock countdown
    void timer()
    {
		totalTime -= Time.deltaTime;
        //minutes = Mathf.Floor(totalTime - (Time.timeSinceLevelLoad / 60f));
		minutes = (int)Mathf.Floor(totalTime / 60f);

        //seconds = (int)60 - (Time.timeSinceLevelLoad % 60f);
		seconds = totalTime % 60f;

		PutTimeToScreen ();

		scrollerScript.UpdateBackgroundFromTime (totalTime, startTime);
        
        if (seconds <= 0.2 && minutes <= 0)
        {
            Application.LoadLevel("EndGame");
        }
       /* if (seconds <= 0)
        {
            seconds += 59f;
        }*/
    }

	public void SetTimerPaused(bool _b)
	{
		isTimerPaused = _b;
	}

	private void PutTimeToScreen()
	{
		string minutesText = "";
		if (minutes > 9) 
		{
			minutesText += minutes;
		} 
		else 
		{
			minutesText += "0" + minutes;
		}

		string secondsText = "";
		if (seconds > 9) 
		{
			secondsText += (int)Mathf.Floor(seconds);
		} 
		else 
		{
			secondsText += "0" + (int)Mathf.Floor(seconds);
		}
		
		counterText.text = minutesText + ":" + secondsText;
	}
}