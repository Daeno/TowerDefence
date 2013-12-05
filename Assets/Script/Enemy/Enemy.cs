using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour {

    public GameObject prefabHealthBar;
    public GameObject healthBarGObj;

    public float life = 100f;
    public float maxLife = 100f;
    public float originalSpeed = 10f;
    public float speed = 10f;
	public int value = 20;

    protected static float startPeriod = 0.5f;

    //slowed 
    private float slowStartTime;
    private float slowTime;
    private float slowRatio;
    

    //poisoned
    private float poisonedDamagePerSec;
    private float poisonedTime;
    private float poisonedStartTime;
    private float poisonedTimer;
    private float poisonedDamagePeriod = 0.1f;

    //the MyNavigation object attached to the GameObject
    private MyNavigation myNavigation;
    private bool         isReached = false;


    // Getter Setter
    public float OriginalSpeed 
    {
        get { return originalSpeed; }
        set { originalSpeed = value; } 
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public float MaxLife
    {
        get { return maxLife; }
        set { maxLife = value; }
    }

    public float Life
    {
        get { return life; }
        set { life = value; }
    }

    public Transform MyTransform
    {
        get { return transform; }
    }


    public float StartPeriod
    {
        get { return startPeriod; }
    }




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

        UpdateHealthBar();

	}


    public void OnTriggerEnter2D(Collider2D collider)
    {

    }


    // =================== PUBLIC METHODS ==================

    //set the route
    public void SetRoute( GameObject routePrefab )
    {
        if ( myNavigation == null ) {
            InitMyNavigation();
        }
        myNavigation.parentObj = routePrefab;
    }


    // called by attacking bullet or weapon, 
    // if the rest life <= 0, die, destroy self
    public void Attacked(float damage)
    {
        //Debug.Log("Enemy being attacked: life = " + life + " , damage = " + damage);

        Debug.Log( "Enemy Attacked: " + damage );
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
        poisonedDamagePerSec = damage;
        poisonedTimer = Time.time;
    }
        


    //may need to send "Killed" message to the Game
    private void Killed()
    {
        GameStatics.cash += value;
        GameStatics.restEnemyNum --;
        DestroyObject( healthBarGObj );
        DestroyObject(gameObject);
    }



    public bool IsSlowed()
    {
        return ( slowTime > 0 );
    }

    public bool IsPoisoned()
    {
        return ( poisonedTime > 0 );
    }

    public bool IsAlive()
    {
        return ( life > 0 );
    }

    //whether reach the last target
    public bool IsReached()
    {
        return myNavigation.IsReached;
    }


    public void  DestroyGameObject()   //不再需要它了 就殺掉它吧
    {
        Destroy(gameObject);
    }

    public void  MoveTo(Vector2 pos)
    {
        transform.position = pos;
    }


    public void  Translate(float x, float y)
    {
        transform.Translate(x, y, 0);
    }

    public void  Translate(Vector2 trans)
    {
        this.Translate(trans.x, trans.y);
    }

    public void  Rotate(Quaternion rot)  //直接將transform.rotation設為rot
    {
        transform.rotation = rot;
    }


    public void Rotate( float degAntiCW )  //逆時針旋轉角度
    {
        Quaternion rotation = transform.rotation;
        rotation.SetEulerAngles( rotation.x, rotation.y, rotation.z + degAntiCW );
    }

    //==================== PRIVATE METHODS =======================


    private void InitMyNavigation()
    {
        myNavigation = (MyNavigation) GetComponent<MyNavigation>();
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
            slowTime = 0;
            slowRatio = 0;
        }
    }

    private void UpdatePoisonedState()
    {
        if ( Time.time < poisonedStartTime + poisonedTime ) {

            if ( Time.time - poisonedTimer >= poisonedDamagePeriod ) {

                poisonedTimer = Time.time;
                life -= poisonedDamagePerSec * poisonedDamagePeriod;
            }
        }
        else {
            poisonedTime = 0;
            poisonedDamagePerSec = 0;
            poisonedTimer = 0;
        }
    }



    protected void UpdateHealthBar()
    {
        if ( healthBarGObj == null ) {
            healthBarGObj = (GameObject) Instantiate( prefabHealthBar, (Vector2)MyTransform.position + Vector2.up*0.5f, Quaternion.identity );
        }

        healthBarGObj.transform.position = (Vector2)MyTransform.position + Vector2.up*0.5f;
        healthBarGObj.transform.localScale = new Vector2( life / maxLife, healthBarGObj.transform.localScale.y );
        
    }
}
