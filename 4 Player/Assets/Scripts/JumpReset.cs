﻿using UnityEngine;
using System.Collections;

public class JumpReset : MonoBehaviour 
{
	public bool canJump = true;

	void OnCollisionStay (Collision col)
	{
		canJump = true;
        transform.parent.gameObject.GetComponent<bubbleInteraction>().counterGrav();
	}

	void OnCollisionExit (Collision col)
	{
		canJump = false;
	}

	public bool GetCanJump()
	{
		return canJump;
	}

	public void SetCanJump(bool _b)
	{
		canJump = _b;
	}
}
