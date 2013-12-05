using UnityEngine;
using System.Collections;

public class GUIInput : MonoBehaviour {
	public GameObject TankPrefabBlue;
	public GameObject TankPrefabRed;
	public GameObject beginPoint;

	private GUI_Disp disp;
	// Use this for initialization
	void Start () {
		disp = GameObject.Find(GameStatics.SCENESYSTEM_OBJ_NAME).GetComponent<GUI_Disp> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("t")){
			GameObject tank = (GameObject)Instantiate (TankPrefabBlue, beginPoint.transform.position, Quaternion.identity);

			//tankYellowList.Add( tank );
		}
		if (Input.GetKeyDown("r")){
			GameObject tank = (GameObject)Instantiate (TankPrefabRed, beginPoint.transform.position, Quaternion.identity);
			tank.SetActive(true);
			//tankRedList.Add( tank );
		}
		
        
        if (Input.GetKeyDown(KeyCode.Escape)) {
			disp.toggleMenu();
		}

	}
}
