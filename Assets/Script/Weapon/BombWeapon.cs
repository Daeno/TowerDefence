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
	}

    public override void Attack()
    {
        Vector2 myPos    = transform.position;
        Vector2 enemyPos = currentTarget.transform.position;

        GameObject bombGObj = (GameObject) Instantiate( prefabBomb, myPos, Quaternion.identity );
        Bomb bomb = (Bomb) bombGObj.GetComponent( "Bomb" );
        bomb.attackDamage = attackDamage;
        bomb.speed        = bombSpeed;
        bomb.standTime    = bombStandTime;
        bomb.target       = enemyPos;
        bomb.bombRadius   = bombRadius;
        bomb.detectedList = targetList;
    }

    public new void LevelUp()
    {
        bombRadius = bombRadiusLevels[level];
    }
}
