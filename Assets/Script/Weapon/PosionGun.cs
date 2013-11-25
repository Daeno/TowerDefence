using UnityEngine;
using System.Collections;

public class PosionGun : Weapon {


    public GameObject prefabPoisonGas;
    private GameObject currentPoisonGas;

    public float[] poisonTimeList = 
    {
        0,
        3f,
        5f,
        7f,
        9f,
        11f
    };

    private float poisonTime;




	// Use this for initialization
	void Start () {
        base.Start();
        poisonTime = poisonTimeList[1];
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();

        
	}


    public override void Attack()
    {
        if ( currentPoisonGas == null ) {
            currentPoisonGas = (GameObject) Instantiate( prefabPoisonGas, myTrfm.transform.position, Quaternion.identity );
        }
        
        PoisonGas poisonGas = (PoisonGas) currentPoisonGas.GetComponent("PoisonGas");

        poisonGas.poisonDamage         = attackDamage;
        poisonGas.targetGObj           = currentTarget;
        poisonGas.attackRadius         = detectRadius;
        poisonGas.enemyDetectedList    = GetWeaponDetector().enemyDetectedList;
        poisonGas.poisonTime           = poisonTime;
        poisonGas.SetupTransform();

    }

    public new void LevelUp()
    {
        poisonTime = poisonTimeList[level];
    }



    protected override void SetCurrentTarget()
    {
         
        MostForwardEnemyDetector detector = (MostForwardEnemyDetector) GetWeaponDetector();

        if (detector.enemyMostForward != null) {
            targetList = detector.enemyDetectedList;
            currentTarget = detector.enemyMostForward;
        }
        else {
            currentTarget = null;
        }
 
    }


}
