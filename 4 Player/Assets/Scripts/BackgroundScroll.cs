using UnityEngine;
using System.Collections;

public class BackgroundScroll : MonoBehaviour {



	public Transform start;
	//public Transform posObj;
	public Transform end;

	public float speed = 10.0f;
	Transform destination;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {


		if(transform.position.y < end.position.y)
		{
			transform.Translate(0,start.position.y,0);
		}

		transform.Translate(0,-speed *Time.deltaTime,0);
}
}