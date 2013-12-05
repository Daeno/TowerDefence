using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Bomb : MonoBehaviour {

    public GameObject prefabBombFire;

    public float        attackDamage ;
    public float        speed        ;
    public float        standTime    ;
    public GameObject   targetGObj;//added 
    public Vector2      target       ;
    public float        bombRadius   ;
    public List<GameObject> detectedList;

    private Transform   myTrfm;
    private Transform   targetTrfm;  //added
    private Transform   bombFireTrfm;
    private Vector2     origPos;
    private Vector2     origScale;
    private float       startStandTime;
    private float       bombingTime;
    private GameObject  bombFireGObj;
    private BombFire    bombFire;
    public  bool        bombed = false;

	// Use this for initialization
	void Start () {
        myTrfm         = transform;
        targetTrfm     = targetGObj.transform;
        origPos        = myTrfm.position;
        origScale      = myTrfm.localScale;
        startStandTime = Time.time ;
        bombingTime    = -1;
        bombed         = false;
        InitAttackRegion();
    }


    void Update()
    {
        // show bomb fire, set the state "bombed" = true
        if ( Time.time - startStandTime >= standTime && !bombed ) {
            bombingTime = Time.time;
            bombFireGObj.renderer.enabled = true;
            bombed = true;
        }

        // bombed = true and have waited for a period for showing the bomb fire
        if ( Time.time - bombingTime >= 0.1f && bombed ) {
            BombAttack();
            DestroyObject( bombFireGObj );
            DestroyObject( gameObject );
        }

        if ( !bombed ) {
            bombFireTrfm.position    = targetTrfm.position;
            myTrfm.position          = targetTrfm.position;
        }
    }

    private void BombAttack()
    {
        HashSet<GameObject> attackedEnemySet = GetAttackSet();

        foreach ( GameObject enemyGObj in attackedEnemySet ) {
            if ( enemyGObj == null ) {
                continue;
            }
            Enemy enemy = (Enemy) enemyGObj.GetComponent( "Enemy" );
            enemy.Attacked( attackDamage );
        }
    }


	// Update is called once per frame
    /*
	void Update () {
        Debug.Log( "Bomb's Update" );

        if ( Vector2.Distance( myTrfm.position, origPos ) <= bombRadius + 1 ) {
            //Debug.Log( "HEY, MOVE! + direction:" + direction + " speed:" + speed );
            //Debug.Log( "target:" + target + "  myPos:" + myTrfm.position );

            myTrfm.Translate( direction * speed * Time.deltaTime );
            myTrfm.localScale = origScale * ( 1 + (  (  Vector2.Distance( myTrfm.position, origPos ) <= Vector2.Distance( myTrfm.position, target ) ?
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
                    //Debug.Log( "Attacking by Bomb" );
                }
            }

            if (Time.time - bombingTime >= 0.1f){
                DestroyObject(bombFireGObj);
                DestroyObject(gameObject);
            }
            

        }

	}*/


    public void CancelAttack()
    {
        if (bombFireGObj != null)
            DestroyObject( bombFireGObj );
        if (gameObject != null)
            DestroyObject( gameObject );
    }


    //need to know which enemies are to attack.
    //that is, initially they are detected by the weaponDetector,
    //then, before the bomb bombing, the list must be updated by the collider trigger.
    //therefore, the collider must be initialized at the beginning.
    private void InitAttackRegion()
    {
        bombFireGObj            = (GameObject) Instantiate( prefabBombFire, target, Quaternion.identity );
        bombFireGObj.renderer.enabled = false;
        bombFireTrfm            = bombFireGObj.transform;
        
        //set scale
        Transform bfTrfm        = bombFireGObj.transform;
        float     diameterScale = bfTrfm.localScale.x * bombRadius / bombFireGObj.renderer.bounds.size.x;
        bfTrfm.localScale       = new Vector2( diameterScale, diameterScale );
        Transform bfcolliderTrfm= bombFireGObj.GetComponent<Collider2D>().transform;
        bfcolliderTrfm.localScale = bfTrfm.localScale;

        //initialize attacklist
        bombFire                = (BombFire) bombFireGObj.GetComponent( "BombFire" );
        bombFire.attackSet      = new HashSet<GameObject>();
        bombFire.attackSet.Add( targetGObj );
    }

    private HashSet<GameObject> GetAttackSet()
    {
        return bombFire.attackSet;
    }

}
