using UnityEngine;
using System.Collections;

public class MostForwardEnemyDetector : WeaponDetector {


    public GameObject enemyMostForward;
    


    /*--------------Inherited----------------
    
    public Vector2 position;
    public Vector2 scale;

    public List<GameObject> enemyDetectedList = new List<GameObject>();
    public GameObject enemyNearest = null;
    
    //the square of nearest distance
    private float nearestDistSqrt;

    -------------------------------------------*/



	// Use this for initialization
	void Start () {
        renderer.sortingLayerName = "weapondetector";
	}
	
	// Update is called once per frame
	void Update () {
        SetMostForwardEnemy();
	}



    private void SetMostForwardEnemy()
    {
        int        maxTargetNum     = -1;
        float      minDistToTarget  = 99999;
        GameObject MostForwardEnemy = null;

        foreach ( GameObject enemyGObj in enemyDetectedList ) {

            if ( enemyGObj == null ) {
                continue;
            }


            MyNavigation myNav = (MyNavigation) enemyGObj.GetComponent( "MyNavigation" );

            if ( myNav.TargetNum < maxTargetNum ) {
                continue;
            }

            else if ( myNav.TargetNum == maxTargetNum ) {
                if ( myNav.DistToCurrTarget > minDistToTarget ) {
                    continue;
                }
                else if ( myNav.DistToCurrTarget < minDistToTarget ) {
                    minDistToTarget = myNav.DistToCurrTarget;
                    MostForwardEnemy = enemyGObj;
                    continue;
                }
            }

            else if ( myNav.TargetNum > maxTargetNum ) {
                maxTargetNum = myNav.TargetNum;
                minDistToTarget = myNav.DistToCurrTarget;
                MostForwardEnemy = enemyGObj;
                continue;
            }
        }


        enemyMostForward = MostForwardEnemy;
    }


}
