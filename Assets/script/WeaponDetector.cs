using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponDetector : MonoBehaviour {

    public Vector2 position;
    public Vector2 scale;

    public List<GameObject> enemyDetectedList = new List<GameObject>();
    public GameObject enemyNearest = null;
    
    private float nearestDist;




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



    public void setupTransform()
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
        if (enemyDetectedList.Count == 0) {
            enemyNearest = null;
            nearestDist = 0;
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

            nearestDist = distToEnemy2D(enemyNearest);
            return;
        }


        // some enemies detected
        foreach (GameObject obj in enemyDetectedList) {
            
            //killed by some bullet
            if (obj == null) {
                enemyDetectedList.Remove(obj);
                continue;
            }

            float dist = distToEnemy2D(obj);
            if (dist < nearestDist) {
                enemyNearest = obj;
                nearestDist = dist;
            }
        }
    
    
    }


    private float distToEnemy2D(GameObject enemyGObj)
    {
        Transform enemyTrfm = enemyGObj.transform;
        Vector2   enemyPos  = new Vector2(enemyTrfm.position.x, enemyTrfm.position.y);
        return Vector2.Distance(enemyPos, position);
    }


}
