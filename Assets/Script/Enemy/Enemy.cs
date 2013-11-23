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

    //the MyNavigation object attached to the GameObject
    private MyNavigation myNavigation;


	// Use this for initialization
	public void Start () {
	    //set the sorting layer
        renderer.sortingLayerName = "weapon";

        InitMyNavigation();

	}
	
	// Update is called once per frame
	public void Update () {

        //update speed and tell MyNavigation
        UpdateSpeed();

        //update slowed and poisoned state
        UpdateSlowedState();
        UpdatePoisonedState();

        

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
            Killed();
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


    //may need to send "Killed" message to the Game
    private void Killed()
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


    private void InitMyNavigation()
    {
        myNavigation = (MyNavigation)GetComponent<MyNavigation>();
        UpdateSpeed();
    }


    private void UpdateSpeed()
    {
        myNavigation.SetSpeed(speed);
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
