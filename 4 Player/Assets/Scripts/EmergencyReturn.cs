using UnityEngine;
using System.Collections;

public class EmergencyReturn : MonoBehaviour 
{
	public GameObject bubblePrefab;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	void OnTriggerStay(Collider _col)
	{
		if (_col.gameObject.tag == "Player") 
		{
			Vector3 pos = GameObject.Find("Scroller/Borders").transform.position;
			_col.transform.position = pos;
			GameObject bubble = Instantiate (bubblePrefab, Vector3.zero, Quaternion.identity) as GameObject;
			bubble.GetComponent<Bubble> ().AssignParameters (0.5f, pos,-Vector3.up);
		}
		else if (_col.gameObject.tag == "Bubble") 
		{
			_col.GetComponent<Bubble>().destroyBubble(true);
		}
	}

}
