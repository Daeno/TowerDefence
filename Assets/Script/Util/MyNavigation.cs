using UnityEngine;
using System.Collections;

public class MyNavigation: MonoBehaviour {
	public  GameObject  parentObj;
	private Transform[] targets;

	private TankB        tt ;
	private float       speed       = 0;
    private bool        isSpeedInitialized = false;

	private int         targetNum   = 1;
	private float       dis         = 0;
	private float       startTime;
	private Vector2     startMarker;
	private GameObject  player;

    private bool        isReached    = false;
    public  bool        IsReached { get { return isReached; } }

    private Transform   myTrfm;


    // for getting the most forward enemy.
    // if an enemy has the biggest value of "targetNum" and the smallest value of "dis,"
    // then he is the closest to the terminal.
    public int TargetNum
    {
        get { return targetNum; }
    }

    public float DistToCurrTarget
    {
        get { return dis; }
    }




	
	// Use this for initialization
	void Start () {
        myTrfm    = transform;

		player    = this.gameObject;
		targetNum = 1;

		//transform.position += new Vector2 (1, 0, 0);
		Transform[] childrenTransform = parentObj.GetComponentsInChildren<Transform> (true);
		int idx = 0, len = childrenTransform.Length;

		//Debug.Log ("haha" + len.ToString ());
		//Debug.Log (childrenTransform [0].ToString ());
		targets = new Transform[len];
		foreach (Transform a in childrenTransform) {
			targets[idx++] = a;
		}
		startTime   = Time.time;
		dis         = Vector2.Distance (myTrfm.position, (targets [targetNum]).position);
		startMarker = myTrfm.position;
        isReached   = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (!isSpeedInitialized) {
            Debug.LogError("Error: MyNavigation: GameObject hasn't initialized speed!");
        }


		if (Vector2.Distance (myTrfm.position, (targets [targetNum]).position) < 0.05f) {

		    if (targetNum < targets.Length - 1) {
				targetNum ++;
				startTime = Time.time;
                dis = Vector2.Distance( myTrfm.position, ( targets[targetNum] ).position );
                startMarker = myTrfm.position;
					
			} else {
				//walk to end, Enemy destroyed
                
				//Destroy(this.gameObject);
				//GameStatic.game_score -=100;

                isReached = true;
			}
					
		}

		myTrfm.position = Vector2.Lerp (startMarker, targets [targetNum].position, speed * (Time.time - startTime) / dis);

	}


    //Must call this function when INITIALIZING AN OBJECT USING THIS
    public void SetSpeed(float spd)
    {
        isSpeedInitialized = true;
        
        if ( spd == speed ) {
            return;
        }

        speed = spd;
        startTime = Time.time;
        if ( myTrfm != null ) {
            dis = Vector2.Distance( myTrfm.position, ( targets[targetNum] ).position );
            startMarker = myTrfm.position;
        }
    }






}
