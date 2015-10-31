using UnityEngine;
using System.Collections;

public class BlowScript : MonoBehaviour {

    public bool blowActive = false;

	// Use this for initialization
	void Start ()
    {

    }    
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerStay(Collider col)
    {
       if (col.transform.gameObject.tag == "Bubble" && blowActive)
        {
            //Debug.Log("Collided");
            Bubble colScript = col.transform.gameObject.GetComponent<Bubble>();
            Vector3 dpos = ( col.transform.gameObject.transform.position - transform.parent.position);
            dpos = Vector3.Normalize(dpos); 
            //Debug.DrawLine(transform.parent.position, col.transform.gameObject.transform.position);
            //Debug.Log(dpos);

            colScript.vel += (dpos);
        }
    }
}
