using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Weapon: MonoBehaviour {



    public GameObject weaponDetectorGObj;

    public int        maxLevel     = 5;

    
    public float[] attackDamageLevels = 
    {
        0, 10, 20, 30, 40, 50
    };

    public float[] detectRadiusLevels = 
    {
        0, 10f, 15f, 20f, 25f, 35f
    };

    public float[] shootPeriodLevels  = 
    { 
        1<<10, 1f, 0.9f, 0.8f, 0.6f, 0.4f
    };


    public bool       enabled      = false;
    public bool       selected     = false;
	public bool		  placing	   = false;
    public int        cost         = 10;
    public int        level        = 1;

    protected float   detectRadius;
    protected float   shootPeriod;
    protected float   attackDamage = 10;


    //timer for shooting periodically
    protected float   shootTimer   = 0f;
    protected Transform        myTrfm;
    protected GameObject       currentTarget;
    protected List<GameObject> targetList;

    protected WeaponDetector weaponDetector;


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
        shootTimer   = -1; // for the first beat
        myTrfm       = transform;
        level        = 1;
        attackDamage = attackDamageLevels[level];
        shootPeriod  = shootPeriodLevels[level];
        detectRadius = detectRadiusLevels[level];

        renderer.sortingLayerName = "weapon";

        SetupWeaponDetector();
	}
	
	// Update is called once per frame
	protected void Update () {

        //if Selected, show the detector
        if (!placing) {
				if (selected) {
						ShowDetector (true);
				} else {
						ShowDetector (false);
				}
		}


        //if not enebled, dont do anything
        if ( !enabled ) {
            weaponDetector.enabled = false;
            return;
        }
        else {
            weaponDetector.enabled = true;
        }


        // dynamically choose a target
        SetCurrentTarget();

        // periodically shoot ; shootTime = -1 is the initial state
        if ( (Time.time - shootTimer >= shootPeriod || shootTimer == -1) 
           && currentTarget != null ) {
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
		attackDamage = attackDamageLevels[level];
        shootPeriod  = shootPeriodLevels[level];
        detectRadius = detectRadiusLevels[level];
		SetupWeaponDetector();
    }

    //----------------------------------



    public void KillEnemy(GameObject enemy)
    {
        weaponDetector.KillEnemy(enemy);
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
        weaponDetector = GetWeaponDetector();
        weaponDetector.position   = myTrfm.position;
        weaponDetector.radius     = detectRadius;
        weaponDetector.SetupTransform();
    }


    protected virtual void SetCurrentTarget()
    {
        currentTarget = weaponDetector.GetCurrentTarget();
        if ( currentTarget != null ) {
            targetList = weaponDetector.enemyDetectedList;
        }

    }




    protected WeaponDetector GetWeaponDetector()
    {
        if ( weaponDetector == null ) {
            weaponDetector =  weaponDetectorGObj.GetComponent<WeaponDetector>();
            if ( weaponDetector == null ) {
                Debug.Log( "Error: WeaponDetector Not Found!" );
            }
        }
        return weaponDetector ;
    }


    public void ShowDetector( bool show )
    {
        WeaponDetector detector = weaponDetector;

        detector.show = show;
    }

}
