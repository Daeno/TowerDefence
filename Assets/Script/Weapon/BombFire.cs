using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BombFire : MonoBehaviour {

    public List<GameObject> attackList;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D( Collider2D collider )
    {
        if ( collider.gameObject.CompareTag( "Enemy" ) ) {
            attackList.Add( collider.gameObject );
        }
    }

    void OnTriggerEnit2D( Collider2D collider )
    {
        attackList.Remove( collider.gameObject );
    }

}
