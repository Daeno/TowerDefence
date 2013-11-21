using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public Transform target;
    public float     speed = 50f;

    //set by the initiating weapon
    public float attackDamage;

    //always change
    private Vector2 myPos;

    //doesnt change after initially set
    private Vector2 enemyPos;


	// Use this for initialization
	void Start () {
        //setup initial position
        Vector3 myPos3D  = transform.position;
        myPos            = new Vector2(myPos3D.x, myPos3D.y);
        enemyPos         = new Vector2(target.position.x, target.position.y);
    
        //set the sorting layer
        renderer.sortingLayerName = "bullet";
    }
	
	// Update is called once per frame
	void Update () {
        myPos = Vector2.MoveTowards(myPos, enemyPos, Time.deltaTime * speed);
        transform.position = new Vector3(myPos.x, myPos.y);
	}



    void OnTriggerEnter2D(Collider2D collider)
    {
        //if touch an enemy, attack it and destroy self
        if (collider.CompareTag("Enemy")) {
            GameObject enemyGObj = collider.gameObject;
            Enemy      enemy     = (Enemy) enemyGObj.GetComponent("Enemy");
            enemy.Attacked(attackDamage);
           

            DestroyObject(gameObject);
        }
        

    }
}
