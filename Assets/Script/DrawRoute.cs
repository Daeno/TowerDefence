using UnityEngine;
using System.Collections;

public class DrawRoute : MonoBehaviour {

	// Use this for initialization
	public GameObject parentObj;
	private Transform[] targets;
	void Start () {
		Transform[] childrenTransform = parentObj.GetComponentsInChildren<Transform> (true);
		int idx = 0,len = childrenTransform.Length;
		targets = new Transform[len];
		foreach (Transform a in childrenTransform) {
			targets[idx++] = a;
		}

	}

	// Update is called once per frame
	void OnGUI () {
		/*Vector2 pointA = new Vector2(Screen.width/2, Screen.height/2);
		Vector2 pointB = Event.current.mousePosition;
		DrawLines.DrawLine(pointA, pointB, 5.0f);*/

		int idx = 0, len = targets.Length;
		//Debug.Log(len);
		//targets = new Transform[len];
		if (len > 2) {
			idx = 1;
			Vector3 startPoint = targets[1].position;
			while (++idx<len) {
				Debug.DrawLine (startPoint, (targets [idx]).position);
				startPoint = targets [idx].position;
			}
		}
	}
}
