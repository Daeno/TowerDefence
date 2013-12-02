﻿using UnityEngine;
using System.Collections;

public class GUI_Disp: MonoBehaviour {

	public Texture2D icon;
	public Texture2D icon2;
	public Texture2D iconLaserTower;
	public Texture2D iconSlowTower;
	public Texture2D iconGun;
	public Texture2D iconPoisonGun;
	public Texture2D iconBombWeapon;
	public Texture2D iconSplittingCube;
	public Texture2D iconSpiralWeapon;
	public GameObject laserTower;
	public GameObject slowTower;
	public GameObject Gunn;
	public GameObject poisonGun;
	public GameObject bombWeapon;
	public GameObject splittingCube;
	public GameObject spiralWeapon;




	private bool showMenu = false;
	private bool showBotBar = true;
	private bool showSideBar = false;
	//private bool isPlacing = false;
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

	public void toggleSideBar(){
		showSideBar = !showSideBar;
	}
	// Update is called once per frame
	[ExecuteInEditMode]
	void OnGUI () {
		//Debug.Log(guiT.text);
		//print(aa);
		if (GUI.Button (new Rect (10,10,80,30), "Next Wave")) {
			//print ("You clicked the button!");
			(gameObject.GetComponent<SystemMain>()).SendWaves();
		}

		//Menu
		if (showMenu) {
			GUI.BeginGroup (new Rect (Screen.width / 2 - 75, Screen.height / 2 - 100, 150, 200));
			// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.

			// We'll make a box so you can see where the group is on-screen.
			GUI.Box (new Rect (0, 0, 150, 200), "Menu");
			//GUI.Button (new Rect (10, 40, 80, 30), "Save");
			if(GUI.Button (new Rect (17, 40, 120, 30), "Resume")){
				toggleMenu();
			}
			if(GUI.Button (new Rect (17, 80, 120, 30), "Exit")){
				Application.Quit();
			}

			// End the group we started above. This is very important to remember!
			GUI.EndGroup ();
		}
		//Button Menu
		if (showBotBar) {

			//TODO:Switch to  GUILayout.area...
			GUI.BeginGroup (new Rect (0, Screen.height / 8 * 7 - 50, Screen.width, Screen.height / 8 + 50));
			// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.
						
			// We'll make a box so you can see where the group is on-screen..
			GUI.Box (new Rect (0, 0, Screen.width, Screen.height / 8 + 50), "");
			GUILayout.BeginArea(new Rect(20,20,Screen.width, Screen.height/8+50));

			//GUI.Button (new Rect (10, 40, 80, 30), "Save");
			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical();
			/*GUI.Label (new Rect(10,10,100,100),"Wave: "+GameStatics.waves.ToString ());
			GUI.Label (new Rect(10,30,100,100),"Cash: $ "+GameStatics.cash.ToString ());
			GUI.Label (new Rect)*/
			GUILayout.Label("Wave: "+GameStatics.waves.ToString ());
			GUILayout.Label("Cash: $ "+GameStatics.cash.ToString ());
			GUILayout.Label("Live: "+GameStatics.lives.ToString());

			GUILayout.EndVertical();
			GUILayout.BeginVertical();
			if(GameStatics.selectedTower!=null){
				GUILayout.Label("Selected Tower: " + GameStatics.selectedTower.name);
			}


			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
			// End the group we started above. This is very important to remember!
			GUILayout.EndArea();
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
		GUILayout.BeginHorizontal();
			GUI.enabled = GameStatics.cash >= (laserTower.GetComponent<LaserGun>()).cost;
			if(GUILayout.Button (iconLaserTower)){
				//gameObject.getComponent("HoverTest");
				HoverTest hh = gameObject.GetComponent<HoverTest>();
				hh.SetHover(laserTower);
				toggleSideBar();

			}
			GUI.enabled = GameStatics.cash >= (Gunn.GetComponent<Gun>()).cost;
			if(GUILayout.Button (iconGun)){
				HoverTest hh = gameObject.GetComponent<HoverTest>();
				hh.SetHover(Gunn);
				toggleSideBar();
			}

		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();

			GUI.enabled = GameStatics.cash >= (slowTower.GetComponent<SlowGun>()).cost;
			if(GUILayout.Button (iconSlowTower)){
				HoverTest hh = gameObject.GetComponent<HoverTest>();
				hh.SetHover(slowTower);
				toggleSideBar();
			}
			//GUILayout.EndVertical();
			//GUILayout.BeginVertical();
			GUI.enabled = GameStatics.cash >= (poisonGun.GetComponent<PosionGun>()).cost;
			if(GUILayout.Button (iconPoisonGun)){
				HoverTest hh = gameObject.GetComponent<HoverTest>();
				hh.SetHover(poisonGun);
				toggleSideBar();
			}

		GUILayout.EndHorizontal();
		//GUILayout.BeginHorizontal();

			GUI.enabled = GameStatics.cash >= (bombWeapon.GetComponent<BombWeapon>()).cost;
			if(GUILayout.Button (iconBombWeapon)){
				HoverTest hh = gameObject.GetComponent<HoverTest>();
				hh.SetHover(bombWeapon);
				toggleSideBar();
			}
			GUI.enabled = GameStatics.cash >= (splittingCube.GetComponent<SplittingCubeWeapon>()).cost;
			if(GUILayout.Button (iconSplittingCube)){
				HoverTest hh = gameObject.GetComponent<HoverTest>();
				hh.SetHover(splittingCube);
				toggleSideBar();
			}
		//GUILayout.EndHorizontal();
		//GUILayout.BeginHorizontal();
			GUI.enabled = GameStatics.cash >= (spiralWeapon.GetComponent<SpiralEmitterWeapon>()).cost;
			if(GUILayout.Button (iconSpiralWeapon)){
				HoverTest hh = gameObject.GetComponent<HoverTest>();
				hh.SetHover(spiralWeapon);
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
