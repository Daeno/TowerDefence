using UnityEngine;
using System.Collections;

public class gameMessenger : MonoBehaviour {

	// Use this for initialization
	private bool showGameOver = false;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GameStatics.lives == 0)
						GameOver ();
	}
	public void GameOver(){
		Time.timeScale = 0;
		showGameOver = true;
	}
	void OnGUI(){
		if (showGameOver) {
				GUI.BeginGroup (new Rect (Screen.width / 2 - 75, Screen.height / 2 - 100, 150, 200));
				// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.

				// We'll make a box so you can see where the group is on-screen.
				GUI.Box (new Rect (0, 0, 150, 200), "Game Over!!!!");
				//GUI.Button (new Rect (10, 40, 80, 30), "Save");
				if (GUI.Button (new Rect (17, 40, 120, 30), "Restart")) {
						Application.LoadLevel ("scene");
				}
				if (GUI.Button (new Rect (17, 80, 120, 30), "Exit")) {
						Application.Quit ();
				}

				// End the group we started above. This is very important to remember!
				GUI.EndGroup ();
		}
	}
}
