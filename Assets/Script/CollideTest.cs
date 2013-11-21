using UnityEngine;
using System.Collections;

public class CollideTest : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButtonDown(0)) {
            transform.Translate(0.2f, 0, 0);
        }
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("triggered");
    }
}
