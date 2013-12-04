using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BombFire : MonoBehaviour {

    public HashSet<GameObject> attackSet;

    private bool isCheckedTriggerStay = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter2D( Collider2D collider )
    {
        if ( collider.gameObject.CompareTag( "Enemy" ) ) {
            attackSet.Add( collider.gameObject );
        }
    }

    void OnTriggerExit2D( Collider2D collider )
    {
        attackSet.Remove( collider.gameObject );
    }


    void OnTriggerStay2D( Collider2D collider )
    {
        if ( collider.gameObject.CompareTag( "Enemy" ) ) {
            attackSet.Add( collider.gameObject );
        }
    }

}
