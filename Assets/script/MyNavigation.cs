using UnityEngine;
using System.Collections;

public class MyNavigation: MonoBehaviour {
	//public GameObject route;
	public Transform[] targets;
	//public Transform target1;
	//public Transform target2;
	public float speed;
	//private ArrayList targets = new ArrayList();
	private int targetNum = 0;
	private float dis = 0;
	private float startTime;
	private Vector3 startMarker;
	private GameObject player;
	private bool stopMove = false;

	//public bool diedOrReached = false;

	// Use this for initialization
	void Start () {
		player = this.gameObject;
		/*
		if (target0 != null) 
			targets.Add(target0);
		if (target1 != null) 
			targets.Add(target1);
		if (target2 != null) 
			targets.Add(target2);*/

		//target = (Transform[])route.transform.GetComponentsInChildren();
		/*foreach (Component a in cc) {
			target.
		}*/
		//targets = new ArrayList (target);
		targetNum = 0;
		//transform.position += new Vector3 (1, 0, 0);
		startTime = Time.time;
		dis = Vector3.Distance (transform.position, (targets [targetNum]).position);
		startMarker = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//if (!stopMove) {
						if (Vector3.Distance (transform.position, (targets [targetNum]).position) < 0.05f) {

								if (targetNum < targets.Length - 1) {
										targetNum ++;
										startTime = Time.time;
										dis = Vector3.Distance (transform.position, (targets [targetNum]).position);
										startMarker = transform.position;
										//Debug.Log ("+++++++++");
								} else {
										//stopMove = true;
									Destroy(this.gameObject);
									GameStatic.game_score -=100;
								}
								
						}


						//Debug.Log (targetNum);
						player.transform.position = Vector3.Lerp (startMarker, targets [targetNum].position, speed * (Time.time - startTime) / dis);
						//yield return new WaitForSeconds (1f);
				//}
		
		//player.destination = target.position;
	}

}
