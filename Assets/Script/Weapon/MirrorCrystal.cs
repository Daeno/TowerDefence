using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MirrorCrystal : MonoBehaviour {

    public GameObject prefabMirrorBullet;

    public LaserGun  laserGun;
    public Transform target;

    public Vector2 center;
    public float   radius;
    public float   speed;

    public float   mirrorBeamDelayTime;

    //private Dictionary<Transform, Pair<float, Vector2> > bulletsTrfmTargetMap = new Dictionary<Transform, Pair<float, Vector2> >();
    private List<Transform> bulletTrfms = new List<Transform>();
    private List<float>     bulletSpeeds = new List<float>();
    private List<Vector2>   bulletDirections = new List<Vector2>();



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
        GameObject gobj = collider.gameObject;
        if ( gobj.CompareTag( "Bullet" ) ) {
            Debug.Log( "Triggered Crystal by Bullet" );

            float bulletSpeed = 0;
            if ( gobj.GetComponent<Bullet>() != null ) {
                Bullet bullet = gobj.GetComponent<Bullet>();
                bulletSpeed = bullet.speed;
                bullet.speed  = 0;
            }
            else {
                if ( gobj.GetComponent<SplittingCubeBullet>() != null ) {
                    SplittingCubeBullet bullet = gobj.GetComponent<SplittingCubeBullet>();
                    bulletSpeed = bullet.speed;
                    bullet.speed = 0;
                }
                else
                    return;
            }


            // shoot the original bullet to the current target enemy
            if ( target != null ) {
                Transform bulletTrfm = gobj.transform;
                if ( !bulletTrfms.Contains( bulletTrfm ) ) {
                    bulletTrfms.Add( bulletTrfm );
                    bulletSpeeds.Add( bulletSpeed );
                    Vector2 direction = target.position - myTrfm.position;
                    direction.Normalize();
                    bulletDirections.Add( direction );
                }
            }
            // make a new MirrorBullet and shoot it to the LaserGun
            else {
                DestroyObject( gobj );
                GameObject mirrorBulletGObj = (GameObject) Instantiate(prefabMirrorBullet, myTrfm.position, Quaternion.identity);
                MirrorBullet mirrorBullet  = (MirrorBullet) mirrorBulletGObj.GetComponent( "MirrorBullet" );
                mirrorBullet.mirrorCrystal = this;
                mirrorBullet.laserGun      = laserGun;
                mirrorBullet.laserGunPos   = center;
                mirrorBullet.mirrorCrystalTrfm = myTrfm;
                mirrorBullet.delayTime = mirrorBeamDelayTime;
            }


            /*if ( !bulletsTrfmTargetMap.ContainsKey( gobj.transform ) ) {
                bulletsTrfmTargetMap.Add( gobj.transform, new Pair<float, Vector2>( bullet.speed, target.position ) );
            }*/
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
        for ( int i = bulletTrfms.Count - 1; i >= 0; i-- ) {
            Transform bulletTrfm = bulletTrfms[i];
            
            if ( bulletTrfm == null ) {
                bulletTrfms.RemoveAt( i );
                bulletSpeeds.RemoveAt( i );
                bulletDirections.RemoveAt( i );
                continue;
            }


            float     speed      = bulletSpeeds[i] ;
            Vector2   direction  = bulletDirections[i];
            direction.Normalize();
            bulletTrfm.position += (Vector3) ( direction * speed * Time.deltaTime );
        }
    }


    /*
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
    */

}
