using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReadyMenu : MonoBehaviour 
{
	private const int MAX_PLAYERS = 4;
	public Text[] startText;
	// Use this for initialization
	void Start () 
	{
		startText = new Text[MAX_PLAYERS];

		for (int i = 0; i < MAX_PLAYERS; ++i) 
		{
			string name = "Canvas/player" + (i + 1) + "StartText";
			startText[i] = GameObject.Find (name).GetComponent<Text>();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void ChangeText(int player, string text)
	{
		startText [player].text = text;
	}

	public void ChangeToScene (string sceneToChangeTo)
	{
		GetComponent<ControllerManager>().UpdateConstantData();
		Application.LoadLevel (sceneToChangeTo);
	}

}
