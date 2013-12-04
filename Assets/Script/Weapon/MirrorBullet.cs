using UnityEngine;
using System.Collections;

public class MirrorBullet : MonoBehaviour {

    /*
    public   GameObject targetGObj;
    public    float     speed = 50f;
    public    float     attackRadius;
    public    float     attackDamage;
    protected Transform myTrfm;
    protected Vector2   origPos;
    protected Vector2   enemyPos;
    protected Vector2   direction;
    */

    //father, mother
    public MirrorCrystal mirrorCrystal;
    public LaserGun      laserGun;
    public Vector2       laserGunPos;
    public Transform     mirrorCrystalTrfm;
    private Transform    myTrfm;

    public float delayTime;
    private float startTime;


	// Use this for initialization
	void Start () {
        renderer.sortingLayerName = "bullet";

        myTrfm = transform;
        laserGun.AccumulatePower(); // 集氣
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if ( Time.time - startTime > delayTime ) {
            DestroyObject( gameObject );
        }


        myTrfm.position = mirrorCrystalTrfm.position;
        myTrfm.rotation = Quaternion.identity;

        float currentLength = renderer.bounds.size.x;

        //set ratation
        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = new Vector3( 0, 0, 57.2958f * Mathf.Atan2( ( laserGunPos.y - myTrfm.position.y ), ( laserGunPos.x - myTrfm.position.x ) ) );
        myTrfm.rotation = rot;

        //set scale
        float   targetLength = Vector2.Distance( myTrfm.position, laserGunPos );
        Vector3 scale = myTrfm.localScale;
        float   times = targetLength*2f / currentLength;
        scale.x = scale.x*times;
        scale.y = 1;
        myTrfm.localScale = scale;
    }
}
