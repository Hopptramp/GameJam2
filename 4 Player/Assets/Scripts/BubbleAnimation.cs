using UnityEngine;
using System.Collections;

public class BubbleAnimation : MonoBehaviour {

	public float speed = 10f;

	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
		Vector3 theScale = transform.localScale;
		float newScale = 1.0f;
		//newScale += Mathf.Cos(Time.fixedDeltaTime);
		theScale.x *= newScale;
		transform.localScale = theScale;
	}
}
