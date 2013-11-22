using UnityEngine;
using System.Collections;

public class Gun : Weapon {

    public GameObject prefabBullet;


    //inheritted fields
    /*--------------------------------------
        public float DetectRadius;
        public GameObject WeaponDetectorGObj;
        public float ShootPeriod;

        public int Level = 1;
        public float AttackDamage = 10;


        //timer for shooting periodically
        protected float shootTimer = 0f;

        protected Transform myTrfm;
        protected Transform currentTarget;
     * ---------------------------------------*/



	// Use this for initialization
	void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();
	}


    public override void Attack()
    {
        Vector3    pos = myTrfm.position;
        Quaternion rot = Quaternion.identity;

        GameObject bulletGObj = 
            (GameObject)Instantiate(prefabBullet, pos, rot);
        Bullet bullet         = (Bullet)bulletGObj.GetComponent("Bullet");
        bullet.target         = currentTarget.transform;
        bullet.attackDamage   = attackDamage;
    }

    public new void LevelUp()
    {
    }

}
