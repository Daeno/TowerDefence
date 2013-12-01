using UnityEngine;
using System.Collections;

/*----------------------------------------------------
 * This class inherits class Weapon for consistence of
 * implementing the system.
 * However, this weapon has its own way of attacking and
 * detecting any enemy.
 * Hence, it leaves the Attack() and LevelUp() empty.
 * It has a useless weaponDetector , too.
 * --------------------------------------------------*/


public class EraserWeapon : Weapon {

    public  float bombDelayTime = 0.1f;

    private bool isTriggered;
    public  bool IsTriggered
    {
        get { return isTriggered; }
    }

    private float bombStartTime = 0;

	// Use this for initialization
	void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();


        //if not enebled, dont do anything
        if ( !enabled ) {
            return;
        }

        if (isTriggered && (Time.time - bombStartTime >= bombDelayTime)){
            DestroyObject( gameObject );
        }
	}

    void OnTriggerEnter2D( Collider2D collider )
    {
        if ( !enabled ) {
            return;
        }


        if ( collider.gameObject.CompareTag( "Enemy" ) ) {
            Enemy enemy = (Enemy) collider.gameObject.GetComponent( "Enemy" );
            enemy.Attacked( attackDamage );
            isTriggered = true;
            bombStartTime = Time.time;
        }
    }

    public override void Attack()
    {
        return;
    }

    public new void LevelUp()
    {
        return;
    }

}
