using UnityEngine;
using System.Collections;

public class GUIMainScript : MonoBehaviour {
	public GameObject TankPrefabBlue;
	public GameObject TankPrefabRed;
	public GameObject beginPoint;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("t")){
			GameObject tank = (GameObject)Instantiate (TankPrefabBlue, beginPoint.transform.position, Quaternion.identity);

			//tankYellowList.Add( tank );
		}
		if (Input.GetKeyDown("r")){
			GameObject tank = (GameObject)Instantiate (TankPrefabRed, beginPoint.transform.position, Quaternion.identity);

			//tankRedList.Add( tank );
		}
	}
}
