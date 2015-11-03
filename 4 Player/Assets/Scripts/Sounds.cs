using UnityEngine;
using System.Collections;

public class Sounds : MonoBehaviour 
{
	//audio
	public AudioClip pop;
	public AudioClip pop2;
	public AudioClip pop3;
	public AudioClip pop4;
	public AudioClip pop5;
	public AudioClip pop6;
	public AudioClip pop7;
	public AudioClip pop8;
	public AudioClip pop9;
	public AudioClip pop10;
	public AudioClip pop11;
	public AudioClip pop12;
	public AudioClip pop13;
	public AudioClip pop14;
	
	public AudioClip bubbleBounce;
	public AudioClip bubbleBounce2;
	public AudioClip bubbleBounce3;
	public AudioClip bubbleBounce4;

	public AudioClip playerDead;
	AudioSource sound;


	// Use this for initialization
	void Start () 
	{
		sound = GetComponent<AudioSource> ();


	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.W)) 
		{
			bubblePop(1.0f);
			Debug.Log("pop");

		}
	}
	public void playerDeath()
	{
		sound.PlayOneShot (playerDead);
	}

	public void bubbleCollide(float scale)
	{
		AudioClip bounceSet = bubbleBounce;
		float volume = (scale/10) ;
		
		
		var randomInt = Random.Range(0,3);
		
		if (randomInt == 0) 
		{
			bounceSet = bubbleBounce;
		}
		else if (randomInt == 1) 
		{
			bounceSet = bubbleBounce2;
		}
		else if (randomInt == 2) 
		{
			bounceSet = bubbleBounce3;
		}
		else if (randomInt == 3) 
		{
			bounceSet = bubbleBounce4;
		}
		
		sound.PlayOneShot (bounceSet, volume);
	}

	public void bubblePop(float scale)
	{
		AudioClip popSet = pop;
		float volume = (scale/10) ;
		var randomInt = Random.Range(0,13);
		
		if (randomInt == 0) 
		{
			popSet = pop;
		}
		else if (randomInt == 1) 
		{
			popSet = pop2;
		}
		else if (randomInt == 2) 
		{
			popSet = pop3;
		}
		else if (randomInt == 3) 
		{
			popSet = pop4;
		}
		else if (randomInt == 4) 
		{
			popSet = pop5;
		}
		else if (randomInt == 5) 
		{
			popSet = pop6;
		}
		else if (randomInt == 6) 
		{
			popSet = pop7;
		}
		else if (randomInt == 7) 
		{
			popSet = pop8;
		}
		else if (randomInt == 8) 
		{
			popSet = pop9;
		}
		else if (randomInt == 9) 
		{
			popSet = pop10;
		}
		else if (randomInt == 10) 
		{
			popSet = pop11;
		}
		else if (randomInt == 11) 
		{
			popSet = pop12;
		}
		else if (randomInt == 12) 
		{
			popSet = pop13;
		}
		else if (randomInt == 13) 
		{
			popSet = pop14;
		}
		
		sound.PlayOneShot (popSet, volume);
	}
}
