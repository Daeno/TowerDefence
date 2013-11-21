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
	
	}
	
	// Update is called once per frame
	void Update () {

        //always update the nearest enemy
        SetNearestEnemy();
	}



    public void OnTrigger2DEnter(Collider2D collider)
    {
        // Enemy enter detected region
        if (collider.gameObject.CompareTag("Enemy")) {
            enemyDetectedList.Add(collider.gameObject);

            // detect the first enemy, immediately set it as the nearest
            if (enemyDetectedList.Count == 1) {
                SetNearestEnemy();
            }
        }
    }



    public void setupTransform()
    {
        transform.position = position;
        transform.localScale = scale;
    }

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

        // some enemies detected
        foreach (GameObject obj in enemyDetectedList) {
            Transform enemyTrfm = obj.transform;
            Vector2   enemyPos  = new Vector2(enemyTrfm.position.x, enemyTrfm.position.y);
            float     dist      = Vector2.Distance(enemyPos, position);
            if (dist < nearestDist) {
                enemyNearest = obj;
                nearestDist  = dist;
            }
        }
    
    
    }


}
