using UnityEngine;
using System.Collections;

public class SlowGun : Weapon {

    public GameObject prefabSlowBullet;

    public float bulletSpeed;

    public float[] slowRatioList = 
    { 1f,   // dummy
      0.7f, // level1
      0.6f, // level2
      0.5f, // level3
      0.4f, // level4
      0.3f  // level5
    };

    public float[] slowTimeList = 
    {
        0f,  //dummy
        5f,  // level1
        6f,  // level2
        8f,  // level3
        11f, // level4
        15f  // level5
    };


    private float slowRatio;
    private float slowTime;


    //getter setters
    public float SlowRatio
    {
        get { return slowRatio; }
    }

    public float SlowTime
    {
        get { return slowTime; }
    }




	// Use this for initialization
	void Start () {
        base.Start();

        slowRatio = slowRatioList[level];
        slowTime  = slowTimeList[level];
	}
	
	// Update is called once per frame
	void Update () {

        base.Update();
	}

    public override void Attack()
    {

        // shoot a no function bullet
        Vector3     pos = myTrfm.position;
        Quaternion  rot = Quaternion.identity;

        GameObject bulletGObj =
            (GameObject)Instantiate(prefabSlowBullet, pos, rot);
        SlowBullet bullet   = (SlowBullet)bulletGObj.GetComponent("SlowBullet");
        bullet.targetGObj       = currentTarget;
        bullet.attackDamage = 0;
        bullet.slowRatio    = slowRatio;
        bullet.slowTime     = slowTime;
        bullet.speed        = bulletSpeed;
        bullet.attackRadius = detectRadius;
    }


    public new void LevelUp()
    {
        slowRatio = slowRatioList[level];
        slowTime = slowTimeList[level];
    }

}
