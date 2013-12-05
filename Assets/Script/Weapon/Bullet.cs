using UnityEngine;
using System.Collections;

// 12.1晚餐討論結果
// (1) 子彈可以射無限遠
// (2) 不確定會不會跟蹤，要根據調敵人速度和子彈速度的結果決定

public class Bullet : MonoBehaviour {

    public GameObject targetGObj;
    public float      speed = 50f;
    public float      attackRadius;

    //set by the initiating weapon
    public float attackDamage;

    protected Transform myTrfm;

    //always change
    protected Vector2 origPos;

    //doesnt change after initially set
    protected Vector2 enemyPos;

    protected Vector2 direction;


	// Use this for initialization
	protected void Start () {
        //setup initial position
        myTrfm           = transform;
        origPos          = myTrfm.position;
        enemyPos         = targetGObj.transform.position;

        direction        = enemyPos - origPos;
        direction.Normalize();


        Quaternion rot  = Quaternion.FromToRotation( Vector2.up, direction );
        myTrfm.rotation = rot;


        //set the sorting layer
        renderer.sortingLayerName = "bullet";

        //set tag
        gameObject.tag = "Bullet";
    }
	
	// Update is called once per frame
	protected void Update () {
        myTrfm.Translate( Vector2.up * speed * Time.deltaTime );
        if ( Vector2.Distance( myTrfm.position,  origPos) > 100) {
            DestroyObject( gameObject );
        }

	}



    protected void OnTriggerEnter2D(Collider2D collider)
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
