using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour {
	
	//float height;
	//int timesDied = 0;
   // public GameObject HUD;
	public GameObject bubblePrefab;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
        // if the height drops below certain point - reset the height and increment the death
		/*height = gameObject.transform.position.y;
		if (height <= 2)
		{
			transform.position = new Vector3(0, 30, 0);
			++timesDied;

            int playerID = GetComponent<Player>().returnPlayerID();
            
           // HUD.GetComponent<HuD>().updateDeaths(playerID, timesDied);
		}*/
		
	}

	void OnTriggerEnter(Collider _col)
	{
		if (_col.transform.gameObject.tag == "Player") 
		{
			Vector3 position = _col.transform.position;
			position.y = transform.parent.position.y;
			_col.transform.position = position;
            _col.GetComponent<bubbleInteraction>().resetMovement();

			Player playerScript = _col.GetComponent<Player>();
			int player = playerScript.returnPlayerID();
			playerScript.EditPlayerDeath(1);
			string name = "HUDCanvas";
			GameObject.Find (name).GetComponent<ScoreTracker>().UpdatePlayerDeath(player, playerScript.GetPlayerDeath());

			//Create a bubble to respawn onto
			GameObject bubble = Instantiate (bubblePrefab, Vector3.zero, Quaternion.identity) as GameObject;
			bubble.GetComponent<Bubble> ().AssignParameters (0.5f, position,-Vector3.up);


			///////////TELL PLAYER THEY HAVE DIED///////////UPDATE ANIMATION///////////RESPAWN ON BUBBLE///////////
			///////////CHANGE SCORE///////////
		}
	}
}
