using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoisonGas : MonoBehaviour {


    //damage per second(original)
    public GameObject targetGObj;
    public float attackRadius;
    public float poisonDamage;
    public float poisonTime;

    // for saving computation effort.
    // if this doesn't change, than it needn't recalculate the scale.
    private float lastAttackRadius = 0;

    // detected list by detector
    public List<GameObject> enemyDetectedList;


	// Use this for initialization
    void Start()
    {
        renderer.sortingLayerName = "bullet"; 
	}
	
	// Update is called once per frame
    void Update()
    {
        if ( targetGObj == null ) {
            DestroyObject( gameObject );
            return;
        }

        if ( !PoisonEnemies() ) {
            DestroyObject( gameObject );
        }
	}


    public void SetupTransform()
    {
        if ( targetGObj == null )
            return;

        //set scale
        if ( attackRadius!=lastAttackRadius ) {
            transform.rotation = Quaternion.identity;
            transform.localScale = new Vector2( transform.localScale.x * ( attackRadius * 2 ) / transform.renderer.bounds.size.x,
                                                transform.localScale.y * ( attackRadius * 2 ) / transform.renderer.bounds.size.y );
        }
        lastAttackRadius = attackRadius;

        //set rotation
        Vector3 enemyPos = targetGObj.transform.position;
        Vector3 myPos    = transform.position;

        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = new Vector3( 0, 0, -90 + 57.2958f * Mathf.Atan2( ( enemyPos.y - myPos.y ), ( enemyPos.x - myPos.x ) ) );
        transform.rotation = rot;

        
    }


    //return value is whether any enemy in the poisoned region
    private bool PoisonEnemies()
    {
        bool poisonedEnemy = false;
        foreach ( GameObject enemyGObj in enemyDetectedList ) {
            Vector2 enemyPos = enemyGObj.transform.position;
            PolygonCollider2D collider = (PolygonCollider2D) GetComponent<PolygonCollider2D>();

            //enemy is in the attacked region
            if ( collider.OverlapPoint( enemyPos ) ) {
                Enemy enemy = (Enemy) enemyGObj.GetComponent( "Enemy" );
                if ( !enemy.IsPoisoned() ) {
                    enemy.Poisoned( poisonTime, poisonDamage );
                }
                poisonedEnemy = true;
            }
        }

        return poisonedEnemy;
    }


}
