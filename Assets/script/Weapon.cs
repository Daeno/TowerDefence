using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Weapon: MonoBehaviour {


    public float DetectRadius;
    public GameObject WeaponDetectorGObj;
    public float ShootPeriod;

    public int MaxLevel = 1;
    public int Level = 1;
    public float AttackDamage = 10;


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
        setCurrentTarget();

        // periodically shoot
        if (Time.time - shootTimer >= ShootPeriod && currentTarget != null) {
            shootTimer = Time.time;
            attack();
        }



	}



    //============abstract functions==============
    public abstract void attack();
    public virtual void levelUp()
    {
        if (Level < MaxLevel) Level++;
    }



    //=============public functions==================
    public void KillEnemy(GameObject enemy)
    {
        WeaponDetector detector = getWeaponDetector();
        detector.KillEnemy(enemy);
        setCurrentTarget();
    }




    //=============private functions===================

    // setup the gun detector
    private void setupWeaponDetector()
    {
        WeaponDetector detector = getWeaponDetector();
        detector.position   = myTrfm.position;
        detector.scale      = new Vector2(DetectRadius, DetectRadius);
        detector.setupTransform();
    }


    private void setCurrentTarget()
    {
        WeaponDetector detector = getWeaponDetector();

        if (detector.enemyNearest != null) {
            targetList = detector.enemyDetectedList;
            currentTarget = detector.enemyNearest;
        }
        else {
            currentTarget = null;
        }
    }


    private WeaponDetector getWeaponDetector()
    {
        WeaponDetector detector = (WeaponDetector)WeaponDetectorGObj.GetComponent("WeaponDetector");
        if (detector == null) {
            Debug.Log("Error: WeaponDetector Not Found!");
        }
        return detector;
    }

}
