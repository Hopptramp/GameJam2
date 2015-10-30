using UnityEngine;
using System.Collections;

public class playerDeath : MonoBehaviour {
	
	float height;
	int timesDied = 0;
    public GameObject HUD;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
        // if the height drops below certain point - reset the height and increment the death
		height = gameObject.transform.position.y;
		if (height <= 1.8)
		{
			transform.position = new Vector3(0, 30, 0);
			++timesDied;

            int playerID = GetComponent<Player>().returnPlayerID();
            
            HUD.GetComponent<HuD>().updateDeaths(playerID, timesDied);
		}
		
	}
}
