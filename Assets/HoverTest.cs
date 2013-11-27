using UnityEngine;
using System.Collections;

public class HoverTest : MonoBehaviour {

	// Use this for initialization
	public GameObject hoverItem;
	public GameObject anchorLeft, anchorRight;
	Vector3 mouseStartPos, offset;
	public void SetHover(GameObject hh){;
		mouseStartPos = Input.mousePosition;
		//mouseStartPos.z = 0;
		Vector3 CurPos = camera.ScreenToWorldPoint(mouseStartPos);
		hoverItem = (GameObject)Instantiate(hh,CurPos,Quaternion.identity);
		Debug.Log("HoverTest Constructor called!");
	}


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (hoverItem != null && Input.mousePresent) {
			Vector3 CurPos = camera.ScreenToWorldPoint(Input.mousePosition);
			Vector3 offset = camera.ScreenToViewportPoint(CurPos);
			//CurPos.z = 10;
			//Vector3 temp = new Vector3(offset.x * ((anchorRight.transform).position.x -(anchorLeft.transform).position.x) + (anchorLeft.transform).position.x,
			//                           offset.y * ((anchorRight.transform).position.y - (anchorLeft.transform).position.y) + (anchorLeft.transform).position.y,
			//                           0);
			CurPos.z = 0;
			hoverItem.transform.position = CurPos;
			Debug.Log("moving" + CurPos.ToString());

		}
		if (Input.GetMouseButtonDown (0)) {
			GameStatics.cash -= hoverItem.GetComponent<Weapon>().cost;
			hoverItem = null;
			Debug.Log("hahaha");		
		}
	}

}
