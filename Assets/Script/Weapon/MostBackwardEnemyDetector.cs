using UnityEngine;
using System.Collections;

public class MostBackwardEnemyDetector : WeaponDetector
{


    public GameObject enemyMostBackward;



    /*--------------Inherited----------------
    
    public Vector2 position;
    public Vector2 scale;

    public List<GameObject> enemyDetectedList = new List<GameObject>();
    public GameObject enemyNearest = null;
    
    //the square of nearest distance
    private float nearestDistSqrt;

    -------------------------------------------*/



    // Use this for initialization
    void Start()
    {
        renderer.sortingLayerName = "weapondetector";
    }

    // Update is called once per frame
    void Update()
    {
        SetMostBackwardEnemy();
    }


    public override GameObject GetCurrentTarget()
    {
        return enemyMostBackward;
    }


    private void SetMostBackwardEnemy()
    {
        int        minTargetNum     = 99999;
        float      maxDistToTarget  = -1;
        GameObject MostBackwardEnemy = null;


        for ( int i = enemyDetectedList.Count - 1; i >= 0; i-- ) {

            GameObject enemyGObj = enemyDetectedList[i];

            // enemy already killed
            if ( enemyGObj == null ) {
                enemyDetectedList.Remove( enemyGObj );
                continue;
            }


            MyNavigation myNav = (MyNavigation) enemyGObj.GetComponent( "MyNavigation" );

            //must backward
            if ( myNav.TargetNum > minTargetNum ) {
                continue;
            }

            //may sbe forward
            else if ( myNav.TargetNum == minTargetNum ) {

                //actually backward because it is further to the same target
                if ( myNav.DistToCurrTarget < maxDistToTarget ) {
                    continue;
                }

                //actually forward
                else if ( myNav.DistToCurrTarget > maxDistToTarget ) {
                    maxDistToTarget = myNav.DistToCurrTarget;
                    MostBackwardEnemy = enemyGObj;
                    continue;
                }
            }

            //must be more forward
            else if ( myNav.TargetNum < minTargetNum ) {
                minTargetNum = myNav.TargetNum;
                maxDistToTarget = myNav.DistToCurrTarget;
                MostBackwardEnemy = enemyGObj;
                continue;
            }
        }

        enemyMostBackward = MostBackwardEnemy;
    }


}
