using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour {

    public float life = 3f;
    public float originalSpeed = 10;

    //public Vector2[] targetList;
    //private int targetIdx;

    public float speed = 10;


    //slowed 
    private float slowStartTime;
    private float slowTime;
    private float slowRatio;
    

    //poisoned
    private float poisonedStartTime;
    private float poisonedTime;
    private float poisonedDamage;
    private float poisonedDeltaTime;




	// Use this for initialization
	public void Start () {
	    //set the sorting layer
        renderer.sortingLayerName = "weapon";

        //at the beginning
//        targetIdx = 0;
	}
	
	// Update is called once per frame
	public void Update () {

        //update slowed and poisoned state
        UpdateSlowedState();
        UpdatePoisonedState();


        //move toward the target
        //move();
	}


    public void OnTriggerEnter2D(Collider2D collider)
    {

    }


    // called by attacking bullet or weapon, 
    // if the rest life <= 0, die, destroy self
    public void Attacked(float damage)
    {
        Debug.Log("Enemy being attacked: life = " + life + " , damage = " + damage);
        
        
        life -= damage;

        //dies
        if (life <= 0) {
            killed();
        }
    }


    
    public void Slowed(float lastTime, float slowratio)
    {
        slowStartTime = Time.time;
        slowTime = lastTime;
        slowRatio = slowratio;
    }


    // time: last time
    // damage: damage per second
    public void Poisoned(float lastTime, float damage)
    {
        poisonedStartTime = Time.time;
        poisonedTime      = lastTime;
        poisonedDamage    = damage;
    }


	/*
    private void move()
    {
        //move toward the target
        Vector3 pos = transform.position;
        Vector2 tgt = targetList[targetIdx];
        //not reached
        if (Vector3.Distance(pos, tgt) > 0.1) {
            Vector3 direction = new Vector3(tgt.x - pos.x, tgt.y - pos.y);
            direction = Vector3.Normalize(direction);
            transform.Translate(direction * speed * Time.deltaTime);
        }
        //reach the target
        else {
            targetIdx++;

            //reach the last target
            if (targetIdx == targetList.Length) {
                reached();
            }
        }
    }*/


    //may need to send "Killed" message to the Game
    private void killed()
    {
        DestroyObject(gameObject);

        //TODO send message to the game
    }

    //may need to send "Killed" message to the Game
    private void reached()
    {
        DestroyObject(gameObject);

        //TODO send message to the game
    }


    private void UpdateSlowedState()
    {

        if (Time.time - slowStartTime < slowTime) {
            speed = originalSpeed * slowRatio;
        }
        else {
            speed = originalSpeed;
        }
    }

    private void UpdatePoisonedState()
    {
        /*
        if (Time.time - poisonedStartTime < poisonedTime) {
            
        }
        else {
            
        }*/

    }

}
