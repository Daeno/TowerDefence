using UnityEngine;
using System.Collections;

public class RaserGun : Weapon {

    public GameObject PrefabRaserBullet;


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


    public override void attack()
    {
        Vector3 enemyPos = currentTarget.transform.position;

        //set position
        Vector3 pos = myTrfm.position;
        Vector3 direction = Vector3.Normalize(enemyPos - pos);
        pos += direction ;

        //set rotation 
        // 180/pi = 57.2958
        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = new Vector3(0, 0, 57.2958f * Mathf.Atan2( (enemyPos.y - pos.y) , (enemyPos.x - pos.x) ) );

        //initiate bullet
        GameObject bulletGObj =
            (GameObject)Instantiate(PrefabRaserBullet, pos, rot);
        Bullet bullet = (Bullet)bulletGObj.GetComponent("Bullet");
        bullet.target = currentTarget.transform;
        bullet.attackDamage = AttackDamage;
    }

    public new void levelUp()
    {
    }


}
