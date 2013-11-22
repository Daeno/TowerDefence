using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Weapon: MonoBehaviour {


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



	// Use this for initialization
	protected void Start () {
        shootTimer = Time.time;
        myTrfm     =transform;
        setupWeaponDetector();
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



    //============abstract functions==============
    public abstract void Attack();
    public virtual void LevelUp()
    {
        if (level < maxLevel) level++;
    }



    //=============public functions==================
    public void KillEnemy(GameObject enemy)
    {
        WeaponDetector detector = GetWeaponDetector();
        detector.KillEnemy(enemy);
        SetCurrentTarget();
    }




    //=============private functions===================

    // setup the gun detector
    private void setupWeaponDetector()
    {
        WeaponDetector detector = GetWeaponDetector();
        detector.position   = myTrfm.position;
        detector.scale      = new Vector2(detectRadius, detectRadius);
        detector.SetupTransform();
    }


    private void SetCurrentTarget()
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


    private WeaponDetector GetWeaponDetector()
    {
        WeaponDetector detector = (WeaponDetector)weaponDetectorGObj.GetComponent("WeaponDetector");
        if (detector == null) {
            Debug.Log("Error: WeaponDetector Not Found!");
        }
        return detector;
    }

}
