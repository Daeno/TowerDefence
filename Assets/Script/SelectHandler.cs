using UnityEngine;
using System.Collections;

public class SelectHandler : MonoBehaviour {

	// Use this for initialization
	private Rect botMenu;
	void Start () {
		Vector2 temp = GUIUtility.GUIToScreenPoint (new Vector2(0, Screen.height / 8 * 7 - 50));
		botMenu = new Rect (temp.x,temp.y, Screen.width, Screen.height / 8 + 50);
	}
	void OnGUI(){

		//GUI.Label (botMenu,"haha");
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			if(botMenu.Contains(Input.mousePosition))Debug.Log ("inside bot!!!");
            if ( !ScreenMouseRay() && !botMenu.Contains(Input.mousePosition)) {

                //tang
                if ( GameStatics.selectedTower != null ) {
                    GameStatics.selectedTower.GetComponent<Weapon>().selected = false;
                    GameStatics.selectedTower = null;
                }
            }
		}
	}
	public bool ScreenMouseRay()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = 5f;
			
		Vector2 v = Camera.main.ScreenToWorldPoint(mousePosition);
		Collider2D col = Physics2D.OverlapPoint(v,1<<8);
		/*Collider2D[] colall = Physics2D.OverlapPointAll (v);
		if(colall.Length > 0){
			foreach(Collider2D c in colall)
			{
				Debug.Log("   Collided with: " + c.collider2D.gameObject.name);
			}
			
		}*/
		if (col != null) {
			Debug.Log (col.gameObject.name);
            if ( GameStatics.selectedTower != null && GameStatics.selectedTower != col.gameObject ) {
                GameStatics.selectedTower.GetComponent<Weapon>().selected = false;
            }

			GameStatics.selectedTower = col.gameObject;
            GameStatics.selectedTower.GetComponent<Weapon>().selected = true;
			return true;
		}
		return false;
	}
}
