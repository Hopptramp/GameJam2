using UnityEngine;
using System.Collections;

public class ShowControls : MonoBehaviour {


    
    public bool showControls = false;
    public bool canvasHidden = false;
    public GameObject canvas;
    public GameObject controlPlane;

	// Use this for initialization
	void Start ()
    {
    	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (showControls)
        {
            if (!canvasHidden)
            {
                canvas.SetActive(false);
                controlPlane.SetActive(true);
                canvasHidden = true;
                

            }
            else
            {
                if (Input.GetButtonDown("Controls"))
                {
                    Debug.Log("Input Happened!");
                    canvas.SetActive(true);
                    controlPlane.SetActive(false);
                    canvasHidden = false;
                    showControls = false;
                    
                }
            }
        }
        else
        {
            if (Input.GetButtonDown("Controls"))
            {
                Debug.Log("Input Happened!");
                showControls = true;

            }
        }
	
	}
}
