﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReadyMenu : MonoBehaviour 
{
	private const int MAX_PLAYERS = 4;
	public Text[] startText;
	public Image[] startImage;
	private Text readyText;
	// Use this for initialization
	void Start () 
	{
		startText = new Text[MAX_PLAYERS];
		startImage = new Image[MAX_PLAYERS];
		readyText = GameObject.Find ("Canvas/GameBegin").GetComponent<Text>();

		for (int i = 0; i < MAX_PLAYERS; ++i) 
		{
			string name = "Canvas/player" + (i + 1) + "StartText";
			startText[i] = GameObject.Find (name).GetComponent<Text>();
			name = "Canvas/player" + (i + 1) + "Bubble";
			startImage[i] = GameObject.Find (name).GetComponent<Image>();
		}

		//Reset constant data (Just in case)
		for(int i = 0; i < MAX_PLAYERS; ++i)
		{
			GameObject.FindGameObjectWithTag ("GlobalConstant").GetComponent<ConstantData> ().activeControllers[i] = false;
			GameObject.FindGameObjectWithTag ("GlobalConstant").GetComponent<ConstantData> ().playerController[i] = MAX_PLAYERS + 1;
			GameObject.FindGameObjectWithTag ("GlobalConstant").GetComponent<ConstantData> ().playerDeaths[i] = 0;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ( Input.GetButton ("B"))
		{
			//Application.Quit();
		}
	}

	public void ChangeText(int player, string text)
	{
		startText [player].text = text;
	}

	public void ChangeImage(int _player, int _controller)
	{
		startImage [_player].color = GameObject.FindGameObjectWithTag ("GlobalConstant").GetComponent<ConstantData> ().playerColours [_controller];
	}
	
	public void ChangeImage(int _player)
	{
		startImage [_player].color = Color.white;
	}

	public void ChangeToScene (string sceneToChangeTo)
	{
		GetComponent<ControllerManager>().UpdateConstantData();
		Application.LoadLevel (sceneToChangeTo);
	}

	public void ChangeGameStartText(string _text)
	{
		readyText.text = _text;
	}

}
