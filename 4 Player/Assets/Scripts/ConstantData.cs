﻿using UnityEngine;
using System.Collections;

public class ConstantData : MonoBehaviour 
{
	public int MAX_PLAYERS = 4;
	public bool[] activeControllers;
	public int[] playerController;
	public GameObject playerPrefab;

	public int[] playerDeaths;

	public Color[] playerColours;
	public RuntimeAnimatorController[] animatorControllers;
	
	// Use this for initialization
	void Start () 
	{

	}

	void Awake()
	{
		DontDestroyOnLoad (this);
		activeControllers = new bool[MAX_PLAYERS];
		//players = new GameObject[MAX_PLAYERS];
		playerController = new int[MAX_PLAYERS];
		playerDeaths = new int[MAX_PLAYERS];
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
