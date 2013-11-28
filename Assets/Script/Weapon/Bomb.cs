using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bomb : MonoBehaviour {

    public GameObject prefabBombFire;

    public float        attackDamage ;
    public float        speed        ;
    public float        standTime    ;
    public Vector2      target       ;
    public float        bombRadius   ;
    public List<GameObject> detectedList;

    private Transform   myTrfm;
    private Vector2     origPos;
    private Vector2     direction;
    private Vector2     origScale;
    private float       distance;
    private float       startStandTime;
    private float       bombingTime;
    private GameObject  bombFireGObj;
    private BombFire    bombFire;
    private List<GameObject> attackedEnemyList;

	// Use this for initialization
	void Start () {
        myTrfm         = transform;
        origPos        = myTrfm.position;
        origScale      = myTrfm.localScale;
        direction      = ( target - origPos );
        distance       = Vector2.Distance( target, origPos );
        direction.Normalize();
        startStandTime = -1;
        bombingTime    = -1;
        attackedEnemyList = new List<GameObject>( detectedList );
        InitAttackRegion();
    }


	// Update is called once per frame
	void Update () {
        if ( Vector2.Distance( myTrfm.position, origPos ) < bombRadius ) {
            Debug.Log( "HEY, MOVE! + direction:" + direction + " speed:" + speed );
            Debug.Log( "target:" + target + "  myPos:" + myTrfm.position );
            myTrfm.Translate( direction * speed * Time.deltaTime );
            myTrfm.localScale = origScale * ( 1 + (  (    Vector2.Distance( myTrfm.position, origPos ) <= Vector2.Distance( myTrfm.position, target ) ?
                                                                        Vector2.Distance( myTrfm.position, origPos ) :
                                                                        Vector2.Distance( myTrfm.position, target ) ) )/distance * 2 );

            return;
        }

        if ( startStandTime <= 0 ) {
            startStandTime = Time.time;
            myTrfm.position = target;
        }

        if ( Time.time - startStandTime >= standTime ) {
            //just bombing
            if ( bombingTime <= 0 ) {
                bombingTime = Time.time;
                bombFireGObj.renderer.enabled = true;
                attackedEnemyList = GetAttackList(); // updated by bombFire

                foreach ( GameObject enemyGObj in attackedEnemyList ) {
                    Enemy enemy = (Enemy) enemyGObj.GetComponent( "Enemy" );
                    enemy.Attacked( attackDamage );
                    Debug.Log( "Attacking by Bomb" );
                }
            }

            if (Time.time - bombingTime >= 0.1f){
                DestroyObject(bombFireGObj);
                DestroyObject(gameObject);
            }
            

        }

	}


    //need to know which enemies are to attack.
    //that is, initially they are detected by the weaponDetector,
    //then, before the bomb bombing, the list must be updated by the collider trigger.
    //therefore, the collider must be initialized at the beginning.
    private void InitAttackRegion()
    {
        bombFireGObj            = (GameObject) Instantiate( prefabBombFire, target, Quaternion.identity );
        bombFireGObj.renderer.enabled = false;
        //( (SpriteRenderer) bombFireGObj.GetComponent<SpriteRenderer>() ).enabled = true;

        //set scale
        Transform bfTrfm        = bombFireGObj.transform;
        float     diameterScale = bfTrfm.localScale.x * bombRadius / bombFireGObj.renderer.bounds.size.x;
        bfTrfm.localScale       = new Vector2( diameterScale, diameterScale );

        bombFire                = (BombFire) bombFireGObj.GetComponent( "BombFire" );
        bombFire.attackList     = detectedList;
    }

    private List<GameObject> GetAttackList()
    {
        return bombFire.attackList;
    }

}
