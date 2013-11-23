using UnityEngine;
using System.Collections;

public class GUIMainScript : MonoBehaviour {
	public GameObject TankPrefabBlue;
	public GameObject TankPrefabRed;
	public GameObject beginPoint;

	private GUI_Disp disp;
	// Use this for initialization
	void Start () {
		disp = gameObject.GetComponent<GUI_Disp> ();
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
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Time.timeScale = Time.timeScale == 1?0:1;
			disp.toggleMenu();
		}
	}
}
