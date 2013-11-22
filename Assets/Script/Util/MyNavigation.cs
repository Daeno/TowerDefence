using UnityEngine;
using System.Collections;

public class MyNavigation: MonoBehaviour {
	public GameObject parentObj;
	private Transform[] targets;

	private Tank tt ;
	private float speed;

	private int targetNum = 1;
	private float dis = 0;
	private float startTime;
	private Vector2 startMarker;
	private GameObject player;
	
	// Use this for initialization
	void Start () {
		tt = (Tank)(gameObject.GetComponent("Tank"));
		player = this.gameObject;
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
		startTime = Time.time;
		dis = Vector2.Distance (transform.position, (targets [targetNum]).position);
		startMarker = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (speed != tt.speed) {
			startTime = Time.time;
			dis = Vector2.Distance (transform.position, (targets [targetNum]).position);
			startMarker = transform.position;
			speed = tt.speed;		
		}
		if (Vector2.Distance (transform.position, (targets [targetNum]).position) < 0.05f) {

				if (targetNum < targets.Length - 1) {
						targetNum ++;
						startTime = Time.time;
						dis = Vector2.Distance (transform.position, (targets [targetNum]).position);
						startMarker = transform.position;
					
					} else {
						//walk to end, Enemy destroyed

						Destroy(this.gameObject);
						//GameStatic.game_score -=100;
					}
					
		}

		player.transform.position = Vector2.Lerp (startMarker, targets [targetNum].position, speed * (Time.time - startTime) / dis);

	}

}
