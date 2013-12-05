    using UnityEngine;
using System.Collections;

public class PosionGun : Weapon {


    public GameObject prefabPoisonGas;
    private GameObject currentPoisonGas;

    public float[] poisonTimeLevels = 
    {
        0,
        3f,
        5f,
        7f,
        9f,
        11f
    };

    private float poisonTime;

    //轉向如果是轉到方位角(正東0度)的180度，等於毒氣的圖片轉270度 = 180 - originalFaceDegree
    private float originalFaceDegree = -90 *( Mathf.PI / 180);



	// Use this for initialization
	void Start () {
        base.Start();
        poisonTime = poisonTimeLevels[level];
        shootPeriod = 0.001f;
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();


        if ( !enabled )
            return;

        Debug.Log( "poisonGun detect num: " + GetWeaponDetector().enemyDetectedList.Count );
	}


    public override void Attack()
    {
        if ( currentPoisonGas == null ) {
            currentPoisonGas = (GameObject) Instantiate( prefabPoisonGas, myTrfm.transform.position, Quaternion.identity );
        }

        Debug.Log( "PoisonGun Attack()" );
        SetRotation();

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
        poisonTime = poisonTimeLevels[level];
        shootPeriod = 0.001f;
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


    protected void SetRotation()
    {
        Vector2 direction = currentTarget.transform.position - myTrfm.position;
        float   orientation;
        if ( direction.x == 0 ) {
            orientation = (float) (0.5*Mathf.PI + ( direction.y > 0 ? 0 : Mathf.PI ) );
        }
        else {
            orientation = ( direction.x >= 0 ? 
                        Mathf.Atan( direction.y / direction.x ):
                        Mathf.Atan( direction.y / direction.x ) + Mathf.PI );
        }

        orientation -= originalFaceDegree;
        Quaternion rotation = Quaternion.identity;
        rotation.eulerAngles = new Vector3( 0, 0, orientation * 180 / Mathf.PI );
        myTrfm.rotation = rotation;
        GetWeaponDetector().transform.rotation = Quaternion.identity;
    }

}
