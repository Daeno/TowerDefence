using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Weapon: MonoBehaviour {

    public float[] attackDamageList = 
    {
        0, 10, 20, 30, 40, 50
    };

    public float[] shootPeriodList  = 
    { 
        1<<10, 1f, 0.9f, 0.8f, 0.6f, 0.4f
    } ;

    public float[] detectRadiusList = 
    {
        0, 10f, 15f, 20f, 25f, 35f
    };


    public float detectRadius;
    public GameObject weaponDetectorGObj;
    public float shootPeriod;

    public int maxLevel = 1;
    public int level = 1;
    public float attackDamage = 10;


    //timer for shooting periodically
    protected float shootTimer = 0f;

    protected Transform myTrfm;
    protected GameObject currentTarget;
    protected List<GameObject> targetList;



    // getter setters
    public float DetectRadius 
    {
        get { return detectRadius; }
    }

    public float ShootPeriod 
    {
        get { return shootPeriod; } 
    }

    public int   MaxLevel       
    {
        set { maxLevel = value; }
        get { return maxLevel; }
    }

    public int   Level          
    {
        set { level = value; }
        get { return level; }
    }

    public float AttackDamage  
    {
        get { return attackDamage; }
    }

    public Transform MyTransform
    {
        set { myTrfm = value; }
        get { return myTrfm; }
    }




	// Use this for initialization
	protected void Start () {
        shootTimer = Time.time;
        myTrfm     =transform;
        SetupWeaponDetector();
	}
	
	// Update is called once per frame
	protected void Update () {

        // dynamically choose a target
        SetCurrentTarget();

        // periodically shoot
        if (Time.time - shootTimer >= shootPeriod && currentTarget != null) {
            shootTimer = Time.time;
            Attack();
        }



	}


    //=============public functions==================

    //--------abstract functions--------
    
    public abstract void Attack();
    public virtual void LevelUp()
    {
        if (level < maxLevel) level++;
    }

    //----------------------------------



    public void KillEnemy(GameObject enemy)
    {
        WeaponDetector detector = GetWeaponDetector();
        detector.KillEnemy(enemy);
        SetCurrentTarget();
    }

    public void DestroyGameObject()
    {
        Destroy( gameObject );
    }

    public void MoveTo( Vector2 pos )
    {
        myTrfm.position = pos;
    }

    public void Translate( float x, float y )
    {
        myTrfm.Translate( x, y, 0 );
    }

    public void Translate( Vector2 trans )
    {
        this.Translate( trans.x, trans.y );
    }

    public void Rotate( Quaternion rot )  //直接將transform.rotation設為rot
    {
        myTrfm.rotation = rot;
    }


    public void Rotate( float degAntiCW )  //逆時針旋轉角度
    {
        Quaternion rotation = myTrfm.rotation;
        rotation.SetEulerAngles( rotation.x, rotation.y, rotation.z + degAntiCW );
    }


    //=============private functions===================

    // setup the gun detector
    private void SetupWeaponDetector()
    {
        WeaponDetector detector = GetWeaponDetector();
        detector.position   = myTrfm.position;
        detector.radius     = detectRadius;
        detector.SetupTransform();
    }


    protected virtual void SetCurrentTarget()
    {
        WeaponDetector detector = GetWeaponDetector();

        if (detector.enemyNearest != null) {
            targetList = detector.enemyDetectedList;
            currentTarget = detector.enemyNearest;
        }
        else {
            currentTarget = null;
        }
    }


    protected WeaponDetector GetWeaponDetector()
    {
        WeaponDetector detector = (WeaponDetector)weaponDetectorGObj.GetComponent("WeaponDetector");
        if (detector == null) {
            Debug.Log("Error: WeaponDetector Not Found!");
        }
        return detector;
    }

}
