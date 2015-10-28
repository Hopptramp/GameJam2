using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBar : MonoBehaviour 
{
	public GameObject barPrefab;
	private GameObject bar;
	private Image barImage;

	private bool progressOverTime = false;
	private float overTimeValue;
	private float overTimeSeconds;
	private float overTimeStart;

	// Use this for initialization
	void Start () 
	{
		bar = Instantiate(barPrefab, transform.position, Quaternion.identity) as GameObject;
		bar.transform.parent = transform;
		barImage = bar.transform.GetChild(0).GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Manages the movement to a certain point over time
		if (progressOverTime == true) 
		{
			//Value of 0 can be used to set to any point instantly
			if(overTimeSeconds <= 0)
			{
				barImage.fillAmount = overTimeValue;
				progressOverTime = false;
			}
			else
			{
				float time = (Time.realtimeSinceStartup - overTimeStart) / overTimeSeconds;
				barImage.fillAmount = Mathf.Lerp(barImage.fillAmount,overTimeValue,time); //The start point may have to be fixed to make lerp properly
				if(time >= 1.0)
				{
					progressOverTime = false;
				}
			}
		}

	
	}

	public bool DecreaseProgress(float rate)
	{
		//Decrease bar my float amount (May be changed to percent?)
		barImage.fillAmount = Mathf.Clamp01(barImage.fillAmount - rate);
		//When bar decreases and hits 0 return true
		if (barImage.fillAmount == 0) 
		{
			return true;
		} 
		else 
		{
			return false;
		}
	}

	//Will decrease progress to a value over seconds (Maybe a coroutine could be used?)
	public void ProgressOverTime(float toValue, float seconds)
	{
		//If value hasn't finished changing yet
		if(progressOverTime == true)
		{
			return;
		}
		progressOverTime = true;
		overTimeValue = Mathf.Clamp01(toValue);
		overTimeSeconds = seconds;
		overTimeStart = Time.realtimeSinceStartup;
	}

	//Getter function to determine when changing over time finishes
	public bool ProgressOvertimeFinished()
	{
		if(progressOverTime == true)
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	public bool IncreaseProgress(float rate)
	{
		//Increase bar my float amount (May be changed to percent?)
		barImage.fillAmount = Mathf.Clamp01(barImage.fillAmount + rate);
		//When bar increases and hits 1 return true (Likely won't be needed)
		if (barImage.fillAmount == 1) 
		{
			return true;
		} 
		else 
		{
			return false;
		}
	}
}
