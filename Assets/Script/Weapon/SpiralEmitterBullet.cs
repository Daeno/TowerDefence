using UnityEngine;
using System.Collections;

public class SpiralEmitterBullet : Bullet {

   
    public float waitTime;
    private float startTime;


	// Use this for initialization
	void Start () {
        base.Start();
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if ( Time.time - startTime > waitTime ) {
            myTrfm.Translate( Vector2.up * speed * Time.deltaTime );

            if ( Vector2.Distance( myTrfm.position, origPos ) > attackRadius) {
                DestroyObject( gameObject );
            }
        }
	}
}
