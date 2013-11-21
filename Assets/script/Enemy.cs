using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float Life = 3f;
    public float Speed = 10;

    public Vector2[] targetList;

    private int targetIdx;


	// Use this for initialization
	void Start () {
	    //set the sorting layer
        renderer.sortingLayerName = "weapon";

        //at the beginning
        targetIdx = 0;
	}
	
	// Update is called once per frame
	void Update () {
        //move toward the target
        move();
	}


    public void OnTriggerEnter2D(Collider2D collider)
    {

    }


    // called by attacking bullet or weapon, 
    // if the rest life <= 0, die, destroy self
    public void Attacked(float damage)
    {
        Debug.Log("Enemy being attacked: life = " + Life + " , damage = " + damage);
        Life -= damage;

        //dies
        if (Life <= 0) {
            killed();
        }
    }




    private void move()
    {
        //move toward the target
        Vector3 pos = transform.position;
        Vector2 tgt = targetList[targetIdx];
        //not reached
        if (Vector3.Distance(pos, tgt) > 0.1) {
            Vector3 direction = new Vector3(tgt.x - pos.x, tgt.y - pos.y);
            direction = Vector3.Normalize(direction);
            transform.Translate(direction * Speed * Time.deltaTime);
        }
        //reach the target
        else {
            targetIdx++;

            //reach the last target
            if (targetIdx == targetList.Length) {
                reached();
            }
        }
    }


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

}
