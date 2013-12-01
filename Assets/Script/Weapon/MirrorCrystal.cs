using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MirrorCrystal : MonoBehaviour {


    public Transform target;

    public Vector2 center;
    public float   radius;
    public float   speed;

    private Dictionary<Transform, Pair<float, Vector2> > bulletsTrfmTargetMap = new Dictionary<Transform, Pair<float, Vector2> >();
    private List<Transform> bulletTrfms = new List<Transform>();
    private List<float>     bulletSpeeds = new List<float>();
    private List<Vector2>   bulletTargets = new List<Vector2>();



    private Transform myTrfm;
    private float startTime;
    
    // 初始角度，和x軸的正夾角(第一象限之角 > 0)
    private const float startAngle = 0;
    private float movePeriod;

	// Use this for initialization
	void Start () {
        startTime   = Time.time;
        myTrfm      = transform;
        movePeriod  = 2 * Mathf.PI * radius / speed;
        SetPosition( startTime );
	}
	
	// Update is called once per frame
	void Update () {
        SetPosition(Time.time);

        UpdateBullets();

	}

    void OnTriggerEnter2D( Collider2D collider )
    {
        Debug.Log( "Triggered Crystal " + gameObject.ToString() +  "  " +collider.gameObject.ToString() );
        GameObject gobj = collider.gameObject;
        if ( gobj.CompareTag( "Bullet" ) ) {
            Debug.Log( "Triggered Crystal by Bullet" );
            Bullet bullet = (Bullet)gobj.GetComponent( "Bullet" );
            Transform bulletTrfm = gobj.transform;
            /*if ( !bulletTrfms.Contains( bulletTrfm ) ) {
                bulletTrfms.Add( bulletTrfm );
                bulletSpeeds.Add( bullet.speed );
                bulletTargets.Add( target.position );
            }*/
            if ( !bulletsTrfmTargetMap.ContainsKey( gobj.transform ) ) {
                //bulletsTrfmTargetMap.Add( gobj.transform, new Pair<float, Vector2>( bullet.speed, target.position ) );
            }
            bullet.speed  = 0;
        }
    }


    private void SetPosition( float time )
    {
        float deltaTime = (time - startTime) % movePeriod;
        float vdt       = speed * deltaTime;
        float angle     = vdt / radius;      // radium
        myTrfm.position = center + ( new Vector2( Mathf.Cos( angle ), Mathf.Sin( angle ) ) ) * radius;
    }


    private void UpdateBullets()
    {
        foreach ( KeyValuePair<Transform, Pair<float,Vector2> > bulletPair in bulletsTrfmTargetMap )
        {
            if ( bulletPair.Key == null ) {
                bulletsTrfmTargetMap.Remove( bulletPair.Key );
                continue;   
            }

            Transform bulletTrfm = bulletPair.Key;
            float     speed      = bulletPair.Value.First;
            Vector2   targetPos  = bulletPair.Value.Second;
            Vector2   direction  = targetPos - (Vector2)bulletTrfm.position;
            direction.Normalize();
            bulletTrfm.position += (Vector3)( direction * speed * Time.deltaTime ); 
        }
    }



    private class Pair<T, U>
    {
        public Pair()
        {
        }

        public Pair( T first, U second )
        {
            this.First  = first;
            this.Second = second;
        }

        public T First { get; set; }
        public U Second { get; set; }
    };


}
