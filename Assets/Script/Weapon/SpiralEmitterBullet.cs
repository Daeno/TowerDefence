using UnityEngine;
using System.Collections;

public class SpiralEmitterBullet : Bullet {

   
    public float waitTime;
    private float startTime;

    // every bullet reset its own rotation according to the latest position of the target
    private bool resetRotation;

	// Use this for initialization
	void Start () {
        base.Start();
        startTime     = Time.time;
        resetRotation = false;
	}
	
	// Update is called once per frame
	void Update () {
        if ( Time.time - startTime > waitTime ) {
            
            if ( !resetRotation ) {
                resetRotation   = true;

                if (targetGObj != null) {
                    direction       = targetGObj.transform.position - myTrfm.position;
                    direction.Normalize();
                    Quaternion rot  = Quaternion.FromToRotation( Vector2.up, direction );
                    myTrfm.rotation = rot;
                }
            }


            myTrfm.Translate( Vector2.up * speed * Time.deltaTime );

            if ( Vector2.Distance( myTrfm.position, origPos ) > attackRadius) {
                DestroyObject( gameObject );
            }
        }
	}
}
