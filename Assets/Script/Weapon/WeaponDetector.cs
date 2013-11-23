using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponDetector : MonoBehaviour {

    public Vector2 position;
    public Vector2 scale;

    public List<GameObject> enemyDetectedList = new List<GameObject>();
    public GameObject enemyNearest = null;
    
    //the square of nearest distance
    private float nearestDistSqrt;




	// Use this for initialization
	void Start () {
        renderer.sortingLayerName = "weapondetector";
	}
	
	// Update is called once per frame
	void Update () {

        //always update the nearest enemy
        SetNearestEnemy();

    }



    void OnTriggerEnter2D(Collider2D collider)
    {
        // Enemy enter detected region
        if (collider.gameObject.CompareTag("Enemy")) {
            Debug.Log("Detect enemy");

            enemyDetectedList.Add(collider.gameObject);

            // detect the first enemy, immediately set it as the nearest
            if (enemyDetectedList.Count == 1) {
                SetNearestEnemy();
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy")) {
            Debug.Log("Enemy leaves");

            enemyDetectedList.Remove(collider.gameObject);

            // detect the first enemy, immediately set it as the nearest
            if (enemyNearest == collider.gameObject) {
                SetNearestEnemy();
            }
        }
    }



    public void SetupTransform()
    {
        transform.position = position;
        transform.localScale = scale;
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
		nearestDistSqrt = 9000000;
        if (enemyDetectedList.Count == 0) {
            enemyNearest = null;
            nearestDistSqrt = 0;
            return;
        }

        // only 1 enemy detected
        if (enemyDetectedList.Count == 1) {
            enemyNearest = enemyDetectedList[0];

            //killed by some bullet
            if (enemyNearest == null) {
                enemyDetectedList.Remove(enemyNearest);
                return;
            }

            nearestDistSqrt = SqrDistToEnemy2D(enemyNearest);
            return;
        }


        // some enemies detected
        for (int i = enemyDetectedList.Count - 1; i >= 0; i--){
            GameObject obj = enemyDetectedList[i];
            
            //killed by some bullet
            if (obj == null) {
                enemyDetectedList.Remove(obj);
                continue;
            }

            float dist = SqrDistToEnemy2D(obj);
            if (dist < nearestDistSqrt) {
                Debug.Log("Setting nearest enemy");
                enemyNearest = obj;
                nearestDistSqrt = dist;
            }
        }
    
    
    }


    //return the square value of dist to enemy
    private float SqrDistToEnemy2D(GameObject enemyGObj)
    {
        Transform enemyTrfm = enemyGObj.transform;
        Vector2   enemyPos  = new Vector2(enemyTrfm.position.x, enemyTrfm.position.y);
        return Vector2.SqrMagnitude(new Vector2( (enemyPos.x - position.x) , (enemyPos.y - position.y) ));
    }


}
