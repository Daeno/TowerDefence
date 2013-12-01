using UnityEngine;
using System.Collections;

public class SplittingCubeBullet : MonoBehaviour
{


    public float attackDamage;
    public float attackRadius;
    public float speed;
    public float spiralGrowSpeed;
    public float circleTime;        // time on the circle before back in


    public enum CubeState
    {
        OUT,
        CIRCLE,
        IN
    }

    private enum RadiusMode
    {
        INCREASE,
        DECREASE,
        KEEP
    }

    private CubeState   state;
    private Vector2     lastPos;
    private float       currRadius;
    private float       stateStartTime;
    private float       period;  // time it takes from the center to the biggest circle


    // Use this for initialization
    void Start()
    {
        renderer.sortingLayerName = "bullet";
        state            = CubeState.OUT;
        stateStartTime   = Time.time;
        currRadius       = 0;
        lastPos          = new Vector2( 0, 0 );

        //set the sorting layer
        renderer.sortingLayerName = "bullet";

        //set tag
        gameObject.tag = "Bullet";
    }

    // Update is called once per frame
    void Update()
    {
        if ( speed != 0 ) {
            SetPosition();
        }
    }




    void OnTriggerEnter2D( Collider2D collider )
    {
        //if touch an enemy, attack it and destroy self
        if ( collider.CompareTag( "Enemy" ) ) {
            GameObject enemyGObj = collider.gameObject;
            Enemy      enemy     = (Enemy) enemyGObj.GetComponent( "Enemy" );
            enemy.Attacked( attackDamage );
        }
    }



    private void SetPosition()
    {
        RadiusMode radiusMode = RadiusMode.KEEP;

        if (state == CubeState.OUT){
            if ( currRadius >= attackRadius ) {
                currRadius = attackRadius;
                state = CubeState.CIRCLE;
                period = Time.time - stateStartTime;
                return;
            }

            radiusMode = RadiusMode.INCREASE;
        }

        if ( state == CubeState.CIRCLE ) {
            if ( Time.time - stateStartTime >= circleTime ) {
                state = CubeState.IN;
                return;
            }

            radiusMode = RadiusMode.KEEP;
        }

        if ( state == CubeState.IN ) {
            if ( currRadius <= 0 ) {
                //finish all
                DestroyObject( gameObject );
                return;
            }

            radiusMode = RadiusMode.DECREASE;
        }

        Vector2 newPos = Spiral( Time.time - stateStartTime, radiusMode );
        transform.Translate( newPos - lastPos );
        lastPos = newPos;
        
        return;
    }




    private Vector2 Spiral( float time, RadiusMode mode)
    {
        float posX = Mathf.Sin(Time.time * speed) * currRadius;
        float posY = Mathf.Cos(Time.time * speed) * currRadius;

        if ( mode == RadiusMode.INCREASE ) {
            currRadius += spiralGrowSpeed * Time.deltaTime;
        }
        else if ( mode == RadiusMode.DECREASE ) {
            currRadius -= spiralGrowSpeed * Time.deltaTime;
        }

        return new Vector2( posX, posY );
    }


}
