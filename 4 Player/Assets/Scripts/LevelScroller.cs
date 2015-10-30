using UnityEngine;
using System.Collections;

public class LevelScroller : MonoBehaviour 
{
	public float scrollRate = 2.0f;
	public float scrollRateModifier = 1.0f; //In case we want tan effect that reduces/increases or reverses the scrolling

	private bool canScroll = false;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (canScroll == true) 
		{
			Vector3 newPos = transform.position;
			newPos.y += (Time.deltaTime * scrollRate * scrollRateModifier);
			transform.position = newPos;
		}
	}

	public void SetScrolling(bool _b)
	{
		canScroll = _b;
	}
}
