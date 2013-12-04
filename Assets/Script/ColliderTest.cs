using UnityEngine;
using System.Collections;

public class ColliderTest : MonoBehaviour {

    CircleCollider2D collider;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void OnTriggerEnter2D( Collider2D coll )
    {
        DestroyObject( coll.gameObject );
		GameStatics.lives --;
    }



}
