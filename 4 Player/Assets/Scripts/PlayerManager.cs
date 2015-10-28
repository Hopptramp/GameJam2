using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour 
{
	public int MAX_PLAYERS; //This needs setting back to private, I presume it was changed for testing but it breaks stuff
	private GameObject[] players;
	
	// Use this for initialization
	void Start () 
	{
		MAX_PLAYERS = GameObject.FindGameObjectWithTag("GlobalConstant").GetComponent<ConstantData>().MAX_PLAYERS;
		players = new GameObject[MAX_PLAYERS];
		
		GameObject prefab = GameObject.FindGameObjectWithTag ("GlobalConstant").GetComponent<ConstantData> ().playerPrefab;
		for (int i = 0; i < MAX_PLAYERS; ++i) 
		{
			int playerController = GameObject.FindGameObjectWithTag ("GlobalConstant").GetComponent<ConstantData> ().playerController[i];
			if(playerController <= MAX_PLAYERS)
			{
				players[i] = Instantiate(prefab, new Vector3((i+2)*2, (i+2)*2,0), Quaternion.identity) as GameObject;
				players[i].GetComponent<Player>().SetupPlayerID(i + 1, playerController);
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
