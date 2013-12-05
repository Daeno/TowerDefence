using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WeaponDetector : MonoBehaviour {

    public Vector2          position;
    public float            radius;

    public List<GameObject>    enemyDetectedList = new List<GameObject>();
    public GameObject       enemyNearest = null;

    public bool             show = false;
    
    //totally controlled by Weapon
    public bool             enabled = false;

    //the square of nearest distance
    private float           nearestDistSqrt;




	// Use this for initialization
	protected void Start () {
        renderer.sortingLayerName = "weapondetector";
	}
	
	// Update is called once per frame
	protected void Update () {

        if ( show ) {
            renderer.enabled = true;
        }
        else {
            renderer.enabled = false;
        }

        if ( enabled ) {
            collider2D.enabled = true;
        }
        else {
            collider2D.enabled = false;
        }

    }



    void OnTriggerEnter2D( Collider2D collider )
    {
        if ( !enabled )
            return;

        // Enemy enter detected region
        if (collider.gameObject.CompareTag("Enemy")) {
    
            if (!DetectingEnemy(collider.gameObject))
                enemyDetectedList.Add(collider.gameObject);

            // detect the first enemy, immediately set it as the nearest
            if (enemyDetectedList.Count == 1) {
                SetNearestEnemy();
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if ( !enabled )
            return;
        
        if (collider.gameObject.CompareTag("Enemy")) {

            while(DetectingEnemy(collider.gameObject))
                enemyDetectedList.Remove(collider.gameObject);

            // detect the first enemy, immediately set it as the nearest
            if (enemyNearest == collider.gameObject) {
                SetNearestEnemy();
            }
        }
    }


    // must OVERRIDE this when inheritting
    public virtual GameObject GetCurrentTarget()
    { 
        //always update the nearest enemy
        SetNearestEnemy();
        return enemyNearest;
    }



    public void SetupTransform()
    {
        transform.position = position;
        //transform.localScale = scale;
        if ( transform.renderer.bounds.size.x != 0 && transform.renderer.bounds.size.y != 0 ) {
            transform.localScale = new Vector2( transform.localScale.x * ( radius * 2 ) / transform.renderer.bounds.size.x,
                                                transform.localScale.y * ( radius * 2 ) / transform.renderer.bounds.size.y );
        }
    }

    public bool DetectingEnemy( GameObject enemyGObj )
    {
        if ( enemyDetectedList.Contains( enemyGObj ) ) {
            return true;
        }
        return false;
    }



    //maybe not needed, 
    //maybe we can just check if some enemy is killed in SetNearesestEnemy() when updeta() called
    //but maybe also we will need this to calculate score, etc
    public void KillEnemy(GameObject enemyObj)
    {
        enemyDetectedList.Remove(enemyObj);
        if (enemyNearest == enemyObj) {
            SetNearestEnemy();
        }
    }

    private void SetNearestEnemy()
    {
        //no enemy detected
        if ( enemyDetectedList.Count == 0 ) {
            enemyNearest = null;
            nearestDistSqrt = 0;
            return;
        }


        // only 1 enemy detected
        if ( enemyDetectedList.Count == 1 ) {
            enemyNearest = enemyDetectedList[0];

            //killed by some bullet
            if ( enemyNearest == null ) {
                enemyDetectedList.Remove( enemyNearest );
                return;
            }

            nearestDistSqrt = SqrDistToEnemy2D( enemyNearest );
            return;
        }


        nearestDistSqrt = 1000000000;
        // some enemies detected
        for ( int i = enemyDetectedList.Count - 1; i >= 0; i-- ) {
            GameObject obj = enemyDetectedList[i];

            //killed by some bullet
            if ( obj == null ) {
                enemyDetectedList.Remove( obj );
                continue;
            }
            float dist = SqrDistToEnemy2D( obj );
            if ( dist < nearestDistSqrt ) {
                enemyNearest = obj;
                nearestDistSqrt = dist;
            }
            
            else if ( dist > radius*radius ) {
                enemyDetectedList.Remove( obj );
                continue;
            }
        }
    }




    //return the square value of dist to enemy
    protected float SqrDistToEnemy2D(GameObject enemyGObj)
    {
        Transform enemyTrfm = enemyGObj.transform;
        Vector2   enemyPos  = new Vector2(enemyTrfm.position.x, enemyTrfm.position.y);
        return Vector2.SqrMagnitude(new Vector2( (enemyPos.x - position.x) , (enemyPos.y - position.y) ));
    }


}
