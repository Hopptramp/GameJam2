using UnityEngine;
using System.Collections;

public class ConstantData : MonoBehaviour 
{
	public int MAX_PLAYERS = 4;
	public bool[] activeControllers;
	public int[] playerController;
	public GameObject playerPrefab;
	
	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad (this);
		activeControllers = new bool[MAX_PLAYERS];
		//players = new GameObject[MAX_PLAYERS];
		playerController = new int[MAX_PLAYERS];
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
