using UnityEngine;
using System.Collections;

public class startLevel : MonoBehaviour {

	public void ChangeToScene (string sceneToChangeTo)
	{
		Application.LoadLevel (sceneToChangeTo);
	}
}