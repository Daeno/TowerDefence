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
	void Start () 
    {
        base.Start();
	}
	
	// Update is called once per frame
    void Update()
    {
        base.Update();
        SetMostForwardEnemy();
	}


    public override GameObject GetCurrentTarget()
    {
        return enemyMostForward;
    }


    private void SetMostForwardEnemy()
    {
        int        maxTargetNum     = -1;
        float      minDistToTarget  = 99999;
        GameObject MostForwardEnemy = null;


        for (int i = enemyDetectedList.Count - 1; i >= 0; i--) {

            GameObject enemyGObj = enemyDetectedList[i];

            // enemy already killed
            if ( enemyGObj == null ) {
                enemyDetectedList.Remove( enemyGObj );
                continue;
            }


            MyNavigation myNav = (MyNavigation) enemyGObj.GetComponent( "MyNavigation" );

            //must backward
            if ( myNav.TargetNum < maxTargetNum ) {
                continue;
            }

            //may sbe forward
            else if ( myNav.TargetNum == maxTargetNum ) {

                //actually backward because it is further to the same target
                if ( myNav.DistToCurrTarget > minDistToTarget ) {
                    continue;
                }

                //actually forward
                else if ( myNav.DistToCurrTarget < minDistToTarget ) {
                    minDistToTarget = myNav.DistToCurrTarget;
                    MostForwardEnemy = enemyGObj;
                    continue;
                }
            }

            //must be more forward
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
