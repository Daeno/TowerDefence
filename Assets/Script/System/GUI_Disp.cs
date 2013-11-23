using UnityEngine;
using System.Collections;

public class GUI_Disp: MonoBehaviour {
	

	private bool showMenu = false;
	private bool showBotBar = true;
	// Use this for initialization
	void Start () {

	}
	public void toggleMenu(){
		showMenu = !showMenu;
		Time.timeScale = (Time.timeScale == 1)?0:1;
		Debug.Log ("toggle!!");
	}
	public void hideBotBar(){
		showBotBar = false;
	}
	// Update is called once per frame
	void OnGUI () {
		//Debug.Log(guiT.text);
		//print(aa);
		/*if (GUI.Button (new Rect (10,10,15,10), "I am a button")) {
			print ("You clicked the button!");
		}*/
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
			GUI.Label (new Rect(10,10,100,100),"Score: "+GameStatics.gameScore.ToString ());
			GUI.Label (new Rect(10,30,100,100),"Cash: $ "+GameStatics.cash.ToString ());

			// End the group we started above. This is very important to remember!
			GUI.EndGroup ();
		}
	}
}
