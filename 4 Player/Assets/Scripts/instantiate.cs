using UnityEngine;
using System.Collections;

public class instantiate : MonoBehaviour {

	public GameObject bubble;

	// Use this for initialization
	void Start () {
	
	}

	void Awake()
	{
		var prop = Instantiate (bubble, new Vector3 (0, 0, 8), Quaternion.identity) as GameObject;

		prop.GetComponent<Bubble> ().assignParameters (100, 2);
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
