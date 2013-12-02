using UnityEngine;
using System.Collections;

public class SlowBullet : Bullet {

    public float slowTime;
    public float slowRatio;


	// Use this for initialization
	void Start () {
        base.Start();

        //set tag
        gameObject.tag = "Bullet";
    }
	
	// Update is called once per frame
	void Update () {
        base.Update();
	}



    void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log( "SlowBullet Collides" + collider.gameObject.ToString() );


        //if touch an enemy, attack it and destroy self
        if (collider.CompareTag("Enemy")) {
            GameObject enemyGObj = collider.gameObject;
            Enemy enemy = (Enemy)enemyGObj.GetComponent("Enemy");
            enemy.Slowed(slowTime, slowRatio);

            DestroyObject(gameObject);
        }
    }
}
