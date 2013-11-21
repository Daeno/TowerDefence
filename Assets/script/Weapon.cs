using UnityEngine;
using System.Collections;

public class Weapon: MonoBehaviour {


    public float DetectRadius;
    public GameObject Prefabbullet;
    public GameObject WeaponDetectorGObj;
    public float ShootPeriod;

    public int Level = 1;
    public float AttackDamage = 10;


    //timer for shooting periodically
    private float shootTimer = 0f;

    private Transform myTrfm;
    private Transform currentTarget;



	// Use this for initialization
	void Start () {
        shootTimer = Time.time;
        myTrfm     =transform;
        setupWeaponDetector();
	}
	
	// Update is called once per frame
	void Update () {

        // dunamically choose a target
        setCurrentTarget();
	}


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
        currentTarget = detector.enemyNearest.transform;
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
