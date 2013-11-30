using UnityEngine;
using System.Collections;

public class BombWeapon : Weapon {

    public GameObject prefabBomb;
    public float bombSpeed;
    public float bombStandTime; // time since the bomb reaches to the time it bombs
    public float[]  bombRadiusLevels = { 0, 2, 3, 4, 5, 6};

    private float bombRadius;


	// Use this for initialization
	void Start () 
    {
        base.Start();
        bombRadius = bombRadiusLevels[level];
	}
	
	// Update is called once per frame
	void Update () 
    {
        base.Update();

        
        if ( IsFocusing() && !GetWeaponDetector().DetectingEnemy(focusedTarget) ) {
            focusBeam.CancelAttack();
            focusBeamGObj = null;
        }
         
	}


    /*------------------Modified for new kind of Bomb Weapon-----------*/
    public GameObject prefabFocusBeam;

    private GameObject    focusedTarget;
    private GameObject    focusBeamGObj;
    private BombFocusBeam focusBeam;

    public override void Attack()
    {

        if ( IsFocusing() ) {
            return;
        }

        Vector2 myPos     = transform.position;
        Vector2 enemyPos  = currentTarget.transform.position;

        
        focusedTarget  = currentTarget;

        //set focus beam
        focusBeamGObj  = (GameObject) Instantiate( prefabFocusBeam, myTrfm.position, Quaternion.identity );
        focusBeam      = (BombFocusBeam) focusBeamGObj.GetComponent("BombFocusBeam");
        focusBeam.targetGObj = focusedTarget;
        focusBeam.bombGObj = (GameObject) Instantiate( prefabBomb, myPos, Quaternion.identity ); 
        
        //set bomb
        Bomb bomb = (Bomb) focusBeam.bombGObj.GetComponent( "Bomb" );
        bomb.attackDamage = attackDamage;
        bomb.speed        = bombSpeed;
        bomb.standTime    = bombStandTime;
        bomb.targetGObj   = currentTarget;
        bomb.target       = enemyPos;
        bomb.bombRadius   = bombRadius;
        bomb.detectedList = targetList;
    }

    private bool IsFocusing()
    {
        // if the beam is there, it must be focusing on some target.
        // After the bomb bombs / or the enemy escapes, 
        // the beam will destroy itself.
        return ( focusBeamGObj != null );
    }


    public new void LevelUp()
    {
        bombRadius = bombRadiusLevels[level];
    }
}
