using UnityEngine;
using System.Collections;

public class JumpReset : MonoBehaviour 
{
	public bool canJump = true;

	void OnTriggerStay (Collider col)
	{
		//if(col.gameObject.transform.tag != "Player")
		//{
			canJump = true;
	        transform.parent.gameObject.GetComponent<bubbleInteraction>().counterGrav();
		//}
	}

	void OnTriggerExit (Collider col)
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
