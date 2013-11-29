using UnityEngine;
using System.Collections;

public class Gun : Weapon {

    public GameObject prefabBullet;
    public float bulletSpeed;

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
        bullet.speed          = bulletSpeed;
        bullet.attackRadius   = detectRadius;
    }

    public new void LevelUp()
    {
    }

}
