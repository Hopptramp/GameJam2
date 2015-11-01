using UnityEngine;
using System.Collections;

public class Sounds : MonoBehaviour 
{
	//audio
	public AudioClip pop;
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

	public void bubblePop(float scale)
	{
		float volume = (scale/10) ;
		sound.PlayOneShot (pop, volume);
	}

	public void playerDeath()
	{
		sound.PlayOneShot (playerDead);
	}

}
