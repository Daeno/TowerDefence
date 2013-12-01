using UnityEngine;
using System.Collections;

public class SelectHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			if(!ScreenMouseRay()){
				GameStatics.selectedTower = null;
			}
		}
	}
	public bool ScreenMouseRay()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = 5f;
			
		Vector2 v = Camera.main.ScreenToWorldPoint(mousePosition);
		Collider2D col = Physics2D.OverlapPoint(v,1<<8);
		/*if(col.Length > 0){
			foreach(Collider2D c in col)
			{
				Debug.Log("Collided with: " + c.collider2D.gameObject.name);
			}
			
		}*/
		if (col != null) {
			Debug.Log (col.gameObject.name);
			GameStatics.selectedTower = col.gameObject;
			return true;
		}
		return false;
	}
}
