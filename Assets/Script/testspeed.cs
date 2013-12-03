using UnityEngine;
using System.Collections;

public class testspeed : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate( Vector2.right * 1 * Time.deltaTime );
	}
}
