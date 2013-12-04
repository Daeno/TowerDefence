using UnityEngine;
using System.Collections;

public class LaserBeam : MonoBehaviour {

    //damage per second(original)
    public float attackDamageOriginal = 15f;
    
    //damage increase per second
    public float damageIncreaseRate = 3f;

    public float attackPeriod = 1f;

    public GameObject targetGObj;
    public GameObject lastTargetGObj;
    
    private float attackTimer;
    private float attackDamage;
    


	// Use this for initialization
	void Start () 
    {
        renderer.sortingLayerName = "bullet";
        attackTimer = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if ( targetGObj == null ) {
            DestroyObject( gameObject );
            return;
        }


        bool toAttack = false;
        if ( targetGObj == lastTargetGObj ) {
            if ( Time.time - attackTimer >= attackPeriod ) {
                attackDamage += damageIncreaseRate;
                toAttack = true;
            }
            else
                return;
        }
        else {
            if ( Time.time - attackTimer >= attackPeriod ) {
                attackDamage = attackDamageOriginal;
                toAttack = true;
            }
        }

        if ( toAttack ) {
            attackTimer = Time.time;
            Enemy enemy = (Enemy) targetGObj.GetComponent( "Enemy" );
            enemy.Attacked( attackDamage * attackPeriod );

            if ( enemy.IsAlive() ) {
                lastTargetGObj = targetGObj;
            }
            else {
                lastTargetGObj = null;
                DestroyObject( gameObject );
            }

            targetGObj = null;
        }

	}


    //for reducing some delay of destroying the beam,
    // NO GAME USAGE
    void OnTriggerExit2D( Collider2D collider )
    {
        if ( collider.gameObject == targetGObj ) {
            DestroyObject( gameObject );
        }
    }
}
