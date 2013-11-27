using UnityEngine;
using System.Collections;

public class GUI_Disp: MonoBehaviour {

	public Texture2D icon;
	public Texture2D icon2;
	public Texture2D iconRazerTower;
	public Texture2D iconSlowTower;
	public Texture2D iconGun;
	public GameObject razerTower;
	public GameObject slowTower;
	public GameObject Gunn;


	private bool showMenu = false;
	private bool showBotBar = true;
	private bool showSideBar = false;
	//private bool isPlacing = false;
	// Use this for initialization
	void Start () {
		Debug.Log( iconRazerTower.Resize (10,10));
		iconSlowTower.Resize(10,10);
		iconGun.Resize (10, 10);
		iconRazerTower.Apply ();
		iconSlowTower.Apply ();
		iconGun.Apply ();
	}
	public void toggleMenu(){
		showMenu = !showMenu;
		Time.timeScale = (Time.timeScale == 1)?0:1;
		Debug.Log ("toggle!!");
	}

	public void hideBotBar(){
		showBotBar = false;
	}

	public void toggleSideBar(){
		showSideBar = !showSideBar;
	}
	// Update is called once per frame
	void OnGUI () {
		//Debug.Log(guiT.text);
		//print(aa);
		if (GUI.Button (new Rect (10,10,80,30), "Next Wave")) {
			//print ("You clicked the button!");
			(gameObject.GetComponent<SystemMain>()).SendWaves();
		}
		if (showMenu) {
			GUI.BeginGroup (new Rect (Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 100));
			// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.

			// We'll make a box so you can see where the group is on-screen.
			GUI.Box (new Rect (0, 0, 100, 100), "Menu");
			//GUI.Button (new Rect (10, 40, 80, 30), "Save");
			if(GUI.Button (new Rect (10, 40, 80, 30), "Resume")){
				toggleMenu();
			}

			// End the group we started above. This is very important to remember!
			GUI.EndGroup ();
		}
		if (showBotBar) {

			//TODO:Switch to  GUILayout.area...
			GUI.BeginGroup (new Rect (0, Screen.height / 8 * 7 - 50, Screen.width, Screen.height / 8 + 50));
			// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.
						
			// We'll make a box so you can see where the group is on-screen.
			GUI.Box (new Rect (0, 0, Screen.width, Screen.height / 8 + 50), "");
			//GUI.Button (new Rect (10, 40, 80, 30), "Save");
			GUI.Label (new Rect(10,10,100,100),"Wave: "+GameStatics.waves.ToString ());
			GUI.Label (new Rect(10,30,100,100),"Cash: $ "+GameStatics.cash.ToString ());

			// End the group we started above. This is very important to remember!
			GUI.EndGroup ();
		}


		if (showSideBar) {
			//sidebar
			GUILayout.BeginArea (new Rect (Screen.width-150,Screen.height/5, 150 ,Screen.height/7*3 ));
			// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.

			// We'll make a box so you can see where the group is on-screen.
			GUILayout.BeginVertical("Box");
			//GUILayout.Box (new Rect (0, 0, 150, Screen.height/7 * 3));
			//GUI.Button (new Rect (10, 40, 80, 30), "Save");

			//GUILayout.Label (new Rect (10, 10, 100, 100), "Score: " + GameStatics.gameScore.ToString ());
			//GUILayout.Label (new Rect (10, 30, 100, 100), "Cash: $ " + GameStatics.cash.ToString ());
			//塔的按鈕
			GUI.enabled = GameStatics.cash >= (razerTower.GetComponent<RaserGun>()).cost;
			if(GUILayout.Button (iconRazerTower)){
				//gameObject.getComponent("HoverTest");
				HoverTest hh = gameObject.GetComponent<HoverTest>();
				hh.SetHover(razerTower);
				toggleSideBar();

			}
			GUI.enabled = GameStatics.cash >= (Gunn.GetComponent<Gun>()).cost;
			if(GUILayout.Button (iconGun)){
				HoverTest hh = gameObject.GetComponent<HoverTest>();
				hh.SetHover(Gunn);
				toggleSideBar();
			}
			GUI.enabled = GameStatics.cash >= (slowTower.GetComponent<SlowGun>()).cost;
			if(GUILayout.Button (iconSlowTower)){
				HoverTest hh = gameObject.GetComponent<HoverTest>();
				hh.SetHover(slowTower);
				toggleSideBar();
			}
			GUI.enabled = true;
			// End the group we started above. This is very important to remember!
			GUILayout.EndVertical();
			GUILayout.EndArea ();
			if (GUI.Button (new Rect (Screen.width - 177, Screen.height / 3 +10, 25, 30), icon2)) {
				toggleSideBar();
			}
		} else {
				if (GUI.Button (new Rect (Screen.width - 27, Screen.height / 3 +10, 25, 30), icon)) {
				toggleSideBar();
			}
		}
	}
}
