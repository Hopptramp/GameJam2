using UnityEngine;
using System.Collections;

public class backingTOONS : MonoBehaviour 
{
	//audio
	public AudioClip Track;
	AudioSource sound;
	
	
	// Use this for initialization
	void Start () 
	{
		sound = GetComponent<AudioSource> ();
		sound.clip = Track;
		sound.loop = true;
		sound.Play ();



	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
