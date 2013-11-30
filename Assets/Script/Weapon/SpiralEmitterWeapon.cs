using UnityEngine;
using System.Collections;

public class SpiralEmitterWeapon : Weapon {
    public int[] emitNumLevels = { 0, 1, 2, 3, 4, 5 };
    
    public float emitPeriod;
    public float bulletSpeed;
    public GameObject prefabEmittedBullet;


    private GameObject[] emittedBulletGObjs;
    private SpiralEmitterBullet[]     emittedBullets;

    private float shootStartTime = 0;
    private int   emitNum;
    private int   shotNum = 0;
  

	// Use this for initialization
	void Start () {
        base.Start();
        emitNum = emitNumLevels[level];
        emittedBulletGObjs = new GameObject[emitNum];
        emittedBullets     = new SpiralEmitterBullet[emitNum];
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();

        // the next shot must be after the current shot
        if ( IsShooting() && (Time.time - shootStartTime >= ( (emitNum) * emitPeriod ) ) ) {
            shootStartTime = 0;
        }

	}


    public override void Attack()
    {
        //first bullet

        if ( !IsShooting() ) {
            shootStartTime = Time.time;

            for ( int i = 0; i < emitNum; i++ ) {
                emittedBulletGObjs[i] = (GameObject) Instantiate( prefabEmittedBullet, myTrfm.position, Quaternion.identity );
                emittedBullets[i]     = (SpiralEmitterBullet) emittedBulletGObjs[i].GetComponent( "SpiralEmitterBullet" );
                SpiralEmitterBullet bullet = emittedBullets[i];

                bullet.targetGObj     = currentTarget;
                bullet.attackDamage   = attackDamage;
                bullet.speed          = bulletSpeed;
                bullet.waitTime       = emitPeriod * i ;
                bullet.attackRadius   = detectRadius;
            }
            
        }

    }

    public new void LevelUp()
    {
        emitNum = emitNumLevels[level];
        emittedBulletGObjs = new GameObject[emitNum];
        emittedBullets     = new SpiralEmitterBullet[emitNum];
    }

    private bool IsShooting()
    {
        return (shootStartTime != 0);
    }
    

}
