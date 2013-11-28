using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public Transform target;
    public float     speed = 50f;

    //set by the initiating weapon
    public float attackDamage;

    private Transform myTrfm;

    //always change
    private Vector2 myPos;

    //doesnt change after initially set
    private Vector2 enemyPos;

    private Vector2 direction;


	// Use this for initialization
	protected void Start () {
        //setup initial position
        myTrfm           = transform;
        Vector2 myPos    = myTrfm.position;
        enemyPos         = target.position;

        direction        = enemyPos - myPos;
        direction.Normalize();

        //set the sorting layer
        renderer.sortingLayerName = "bullet";
    }
	
	// Update is called once per frame
	protected void Update () {
        myTrfm.Translate( direction * speed * Time.deltaTime );
        if ( Vector2.Distance( myTrfm.position, enemyPos ) > 50 ) {
            DestroyObject( gameObject );
        }

	}



    void OnTriggerEnter2D(Collider2D collider)
    {
        //if touch an enemy, attack it and destroy self
        if (collider.CompareTag("Enemy")) {
            GameObject enemyGObj = collider.gameObject;
            Enemy      enemy     = (Enemy) enemyGObj.GetComponent("Enemy");
            enemy.Attacked(attackDamage);
           

            DestroyObject(gameObject);
        }
    }
}
