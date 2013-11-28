using UnityEngine;
using System.Collections;

public class LaserGun : Weapon {

    public GameObject prefabLaserBeam;


    public float[] damageIncreaseRateLevels = 
    {
        0, 3f, 4f, 5f, 6f, 7f
    };

    public float damageIncreaseRate = 3f;

    // Not really affect the total damage, but how long you wait for an attack
    // whose damage(per attack) is propotional to the period
    public float attackPeriod = 1f;



    public float AttackPeriod
    {
        set { attackPeriod = value; }
        get { return attackPeriod; }
    }



    // memorize the current showed 
    // if a new beam is showed, change this as it and destroy the old one
    private GameObject currentLaserBeam;



	// Use this for initialization

	void Start () {
        base.Start();
        damageIncreaseRate = damageIncreaseRateLevels[level];
        shootPeriod = 0.001f;
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();


	}


    public override void Attack()
    {

        //init
        Vector3 enemyPos = currentTarget.transform.position;
        Vector3 pos = myTrfm.position;



        if ( currentLaserBeam == null || currentLaserBeam.transform.position != myTrfm.position ) {
            //initiate bullet
            currentLaserBeam = (GameObject) Instantiate( prefabLaserBeam, pos, Quaternion.identity );

        }
        else {
            // else attacking the same target as the last attack , 
            // just move the same beam without initiating a new one
            currentLaserBeam.transform.rotation = Quaternion.identity;
        }

        float currentLength = currentLaserBeam.renderer.bounds.size.x;

        //set ratation
        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = new Vector3( 0, 0, 57.2958f * Mathf.Atan2( ( enemyPos.y - pos.y ), ( enemyPos.x - pos.x ) ) );
        currentLaserBeam.transform.rotation = rot;

        //set scale
        float   targetLength = Vector2.Distance( myTrfm.position, enemyPos );
        Vector3 scale = currentLaserBeam.transform.localScale;
        float   times = targetLength*2f / currentLength;
        scale.x = scale.x*times + 0.1f ;// targetLength + 0.2f;
        scale.y = 1;
        currentLaserBeam.transform.localScale = scale;



        LaserBeam beam = (LaserBeam) currentLaserBeam.GetComponent<LaserBeam>();
        beam.attackDamageOriginal = attackDamage;
        beam.attackPeriod         = attackPeriod;
        beam.damageIncreaseRate   = damageIncreaseRate;
        beam.targetGObj           = currentTarget;
    }

    public new void LevelUp()
    {
        damageIncreaseRate = damageIncreaseRateLevels[level];
        shootPeriod = 0.001f;
    }


}
