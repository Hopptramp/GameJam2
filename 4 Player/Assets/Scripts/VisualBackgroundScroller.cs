using UnityEngine;
using System.Collections;

public class VisualBackgroundScroller : MonoBehaviour 
{



	public Transform start;
	//public Transform posObj;
	public Transform end;

	public float speed = 10.0f;
	Transform destination;

	public GameObject background1;
	public GameObject background2;
	public GameObject background3;

	private Material backMaterial1;
	private Material backMaterial2;
	private Material backMaterial3;

	// Use this for initialization
	void Start () 
	{
		backMaterial1 = background1.GetComponent<Renderer> ().material;
		backMaterial2 = background2.GetComponent<Renderer> ().material;
		backMaterial3 = background3.GetComponent<Renderer> ().material;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector2 offset = backMaterial1.GetTextureOffset ("_MainTex");
		offset.y = offset.y - (Time.deltaTime * 0.2f);
		offset.x = Mathf.Sin (Time.realtimeSinceStartup) / 100;
		backMaterial1.SetTextureOffset ("_MainTex",offset);

		offset = backMaterial2.GetTextureOffset ("_MainTex");
		offset.y = offset.y - (Time.deltaTime * 0.3f);
		offset.x = Mathf.Sin (Time.realtimeSinceStartup) / 60;
		backMaterial2.SetTextureOffset ("_MainTex",offset);

		offset = backMaterial3.GetTextureOffset ("_MainTex");
		offset.y = offset.y - (Time.deltaTime * 0.8f);
		offset.x = Mathf.Sin (Time.realtimeSinceStartup) / 80;
		backMaterial3.SetTextureOffset ("_MainTex",offset);

	/*	if(transform.position.y < end.position.y)
		{
			transform.Translate(0,start.position.y,0);
		}

		transform.Translate(0,-speed *Time.deltaTime,0);*/
	}
}