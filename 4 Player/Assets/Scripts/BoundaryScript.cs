using UnityEngine;
using System.Collections;

public class BoundaryScript : MonoBehaviour 
{
	public bool directionUp = true;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player") 
		{
			bubbleInteraction script = col.gameObject.transform.GetComponent<bubbleInteraction> ();
			if(directionUp == true)
			{
				script.vel.y = 0;
				script.acc.y = 0;
			}
			else
			{
				script.vel.x = 0;
				script.acc.x = 0;
			}
		}

	}
}
