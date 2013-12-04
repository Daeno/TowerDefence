using UnityEngine;
using System.Collections;

public class HoverTest : MonoBehaviour {

	// Use this for initialization
	public GameObject hoverItem, hovertype;

	//route variable
	public GameObject parentObj;
	private Transform[] targets;
	public float minDis = 1.0f;

	private bool activated = false;
	private Weapon weapon;
	Vector3 mouseStartPos;



	public void SetHover(GameObject hh){;
		mouseStartPos = Input.mousePosition;
		//mouseStartPos.z = 0;
		/*Vector3 CurPos = camera.ScreenToWorldPoint(mouseStartPos);

		Debug.Log("HoverTest Constructor called!");*/
		hovertype = hh;
		activated = true;
	}
	public static float minimum_distance(Vector2 v, Vector2 w, Vector2 p) {
		// Return minimum distance between line segment vw and point p
		float l2 = Vector2.SqrMagnitude(w-v);  // i.e. |w-v|^2 -  avoid a sqrt
		if (l2 == 0.0) return Vector2.Distance(p, v);   // v == w case
		// Consider the line extending the segment, parameterized as v + t (w - v).
		// We find projection of point p onto the line. 
		// It falls where t = [(p-v) . (w-v)] / |w-v|^2
		float t = Vector2.Dot(p - v, w - v) / l2;
		if (t < 0.0) return Vector2.Distance(p, v);       // Beyond the 'v' end of the segment
		else if (t > 1.0) return Vector2.Distance(p, w);  // Beyond the 'w' end of the segment
		Vector2 projection = v + t * (w - v);  // Projection falls on the segment
		return Vector2.Distance(p, projection);
	}

	public bool PlaceToRoute(Vector2 pos){
		//Debug.Log("called");
		for(int i=1 ;i<targets.Length - 1;i++){
			if(minimum_distance(new Vector2(targets[i].position.x,targets[i].position.y),
			                    new Vector2(targets[i+1].position.x,targets[i+1].position.y),pos) < minDis){
				//Debug.Log ("PlaceToRoute return false" + targets[i].gameObject.name+"  "+targets[i+1].gameObject.name);
				return false;
			}
		}
		//Debug.Log ("PlaceToRoute return true");
		return true;
	}


	void Start () {
		//StageManager SM = GetComponent<StageManager> ();
		//parentObj = SM.GetRoute (true,0);

		Transform[] childrenTransform = parentObj.GetComponentsInChildren<Transform> (true);
		int idx = 0, len = childrenTransform.Length;
		
		//Debug.Log ("haha" + len.ToString ());
		//Debug.Log (childrenTransform [0].ToString ());
		targets = new Transform[len];
		foreach (Transform a in childrenTransform) {
			targets[idx++] = a;
			//Debug.Log ("se: "+a.name);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(activated){
			if (hoverItem == null && Input.GetMouseButtonDown(0)) {
				hoverItem = (GameObject)Instantiate(hovertype,Input.mousePosition,Quaternion.identity);
                weapon = GetWeaponByGameObject( hoverItem );
				GameStatics.cash -= weapon.cost;
                weapon.selected = true;
				weapon.placing = true;
			}
			if (hoverItem != null && Input.GetMouseButton(0)) {
				Vector3 CurPos = camera.ScreenToWorldPoint(Input.mousePosition);
				//Vector3 offset = camera.ScreenToViewportPoint(CurPos);                   
				CurPos.z = 0;
				hoverItem.transform.position = CurPos;
				//Debug.Log("moving" + CurPos.ToString());
				Vector3 temp = camera.ScreenToWorldPoint(Input.mousePosition);
				if(!PlaceToRoute(new Vector2(temp.x,temp.y))){
					weapon.ShowDetector(false);
					Debug.Log ("too close to place");
				}else{
					weapon.ShowDetector (true);
				}
			}
			if(hoverItem != null && Input.GetMouseButtonUp(0)){
				Debug.Log ("HoverUnit deactivated!");
				activated = false;
				Vector3 temp = camera.ScreenToWorldPoint(Input.mousePosition);
				//Weapon weapon = GetWeaponByGameObject( hoverItem );
				if(PlaceToRoute(new Vector2(temp.x,temp.y))){
					weapon.placing = false;
                	weapon.selected = false;
					weapon.enabled = true; 
				}else{
					GameStatics.cash += weapon.cost;
					DestroyObject(hoverItem);
				}
					
				hoverItem = null;
				
			}
		}
	}

    protected Weapon GetWeaponByGameObject( GameObject gobj )
    {
        Weapon weapon = null;
        weapon = gobj.GetComponent<Weapon>();
        return weapon;

    }


}
