using UnityEngine;
using System.Collections;

public class HoverTest : MonoBehaviour {

	// Use this for initialization
	public GameObject hoverItem, hovertype;
	public GameObject anchorLeft, anchorRight;
	private bool activated = false;
	Vector3 mouseStartPos, offset;
	public void SetHover(GameObject hh){;
		mouseStartPos = Input.mousePosition;
		//mouseStartPos.z = 0;
		/*Vector3 CurPos = camera.ScreenToWorldPoint(mouseStartPos);

		Debug.Log("HoverTest Constructor called!");*/
		hovertype = hh;
		activated = true;
	}
	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(activated){
			if (hoverItem == null && Input.GetMouseButtonDown(0)) {
				hoverItem = (GameObject)Instantiate(hovertype,Input.mousePosition,Quaternion.identity);
				GameStatics.cash -= hoverItem.GetComponent<Weapon>().cost;
			}
			if (hoverItem != null && Input.GetMouseButton(0)) {
				Vector3 CurPos = camera.ScreenToWorldPoint(Input.mousePosition);
				Vector3 offset = camera.ScreenToViewportPoint(CurPos);                   
				CurPos.z = 0;
				hoverItem.transform.position = CurPos;
				Debug.Log("moving" + CurPos.ToString());

			}
			if(hoverItem != null && Input.GetMouseButtonUp(0)){
				Debug.Log ("HoverUnit deactivated!");
				activated = false;
				(hoverItem.GetComponent<Weapon>()).enabled = true; 
				hoverItem = null;
			}
		}
	}

}
