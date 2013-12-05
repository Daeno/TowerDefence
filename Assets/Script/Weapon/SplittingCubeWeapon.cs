using UnityEngine;
using System.Collections;

public class SplittingCubeWeapon : Weapon {

    public GameObject prefabSplittingCubeBullet;
    public int[] cubeNumLevels      = { 0, 2, 3, 3, 4, 4 };
    public float[] cubeSpeedLevels  = { 0, 3, 5, 7, 9, 11 };

    // time of the cube being ON the outtest circle before back in
    public float cubeCircleTime = 1;

    //speed of the split cubes spiral's getting larger/smaller
    public float spiralGrowSpeed = 2;

    // speed of the split cubes MOVING
    private float cubeSpeed;

    private int   cubeNum; 


    public float SpiralGrowSpeed
    {
        set { spiralGrowSpeed = value; }
        get { return spiralGrowSpeed; }
    }

    public float CubeCircleTime
    {
        set { cubeCircleTime = value; }
        get { return cubeCircleTime; }
    }


	// Use this for initialization
	void Start () 
    {
        base.Start();
	    cubeNum   = cubeNumLevels[level];
        cubeSpeed = cubeSpeedLevels[level];

        //shootPeriodLevels NOT WORKING
        shootPeriod = detectRadius/spiralGrowSpeed * 2 + cubeCircleTime;
    }
	
	// Update is called once per frame
	void Update () 
    {
        base.Update();


        if ( !enabled )
            return;

	}

    // override
    public override void Attack()
    {
        GameObject[] cubeArr = new GameObject[cubeNum];
        Quaternion   rot     = Quaternion.identity;
        
        for ( int i = 0; i < cubeNum; i++ ) {
            cubeArr[i] = (GameObject) Instantiate( prefabSplittingCubeBullet, myTrfm.position, rot );
            
            SplittingCubeBullet cube = (SplittingCubeBullet) cubeArr[i].GetComponent("SplittingCubeBullet");
            cube.attackDamage        = attackDamage;
            cube.attackRadius        = detectRadius;
            cube.speed               = cubeSpeed;
            cube.spiralGrowSpeed     = spiralGrowSpeed;
            cube.circleTime          = cubeCircleTime;

            rot.eulerAngles = new Vector3( rot.eulerAngles.x, rot.eulerAngles.y, rot.eulerAngles.z + 360/cubeNum );
        }
    }

    public new void LevelUp()
    {
        cubeNum = cubeNumLevels[level];
        cubeSpeed = cubeSpeedLevels[level];


        //shootPeriodLevels NOT WORKING
        shootPeriod = detectRadius/spiralGrowSpeed * 2 + cubeCircleTime;
    }

}
