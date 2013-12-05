using UnityEngine;
using System.Collections;

public class NetManager : MonoBehaviour {

	public GameObject TankPrefabBlue, beginPoint;
	private bool isWaiting = true,isWaiting_sending = false, isWaiting_opp = false;
	private PhotonView photonView;

	private int temp_gameScore;
	private int temp_cash;
	private int temp_waves;
	private int temp_lives;

	private int prev_gameScore = 0;

	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings("0.1");
		/*temp_gameScore = GameStatics.gameScore;
		temp_cash = GameStatics.cash;
		temp_waves = GameStatics.waves;
		temp_lives = GameStatics.lives;*/
		//photonView.RPC ("SetOppStatics",PhotonTargets.Others,GameStatics.gameScore,GameStatics.cash,GameStatics.waves,GameStatics.lives);
		//photonView = PhotonView.Get (this);
	}
	void CheckChanged(){
		if (temp_cash != GameStatics.cash || temp_gameScore != GameStatics.gameScore || temp_waves != GameStatics.waves
						|| temp_lives != GameStatics.lives) {
			photonView.RPC ("SetOppStatics",PhotonTargets.Others,GameStatics.gameScore,GameStatics.cash,GameStatics.waves,GameStatics.lives);
			temp_gameScore = GameStatics.gameScore;
			temp_cash = GameStatics.cash;
			temp_lives = GameStatics.lives;
			temp_waves = GameStatics.waves;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(PhotonNetwork.connected)
		   if(PhotonNetwork.room != null){
			if (PhotonNetwork.room.playerCount == 1) {
				//Debug.Log("Waiting Zzzz");
				isWaiting = true;
			}
			else{
				isWaiting = false;
			}
			if(!isWaiting){
				if(photonView == null)
					photonView = PhotonView.Get(this);
				//if(Input.GetMouseButtonDown(0)){
				if(GameStatics.gameScore - prev_gameScore > 20){
					Debug.Log ("sending 1");
					prev_gameScore += 20;
					photonView.RPC("SpawnEnemy",PhotonTargets.Others);
				}
				CheckChanged ();
			}
		}


	}
	void OnGUI(){
		GUILayout.BeginArea (new Rect (10, 50, 100, 500));
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		foreach (RoomInfo game in PhotonNetwork.GetRoomList())
		{
			GUILayout.Label(game.name + " " + (game.playerCount).ToString() + "/" + (game.maxPlayers).ToString());
		}

		GUILayout.EndArea();
		if (isWaiting) {
			Time.timeScale = 0;
			GUI.BeginGroup (new Rect (Screen.width / 2 - 75, Screen.height / 2 - 100, 150, 200));
			// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.

			// We'll make a box so you can see where the group is on-screen.
			GUI.Box (new Rect (0, 0, 150, 200), "");
			//GUI.Button (new Rect (10, 40, 80, 30), "Save");
			GUI.Label (new Rect (0, 0, 150, 200), "Waiting");		
			// End the group we started above. This is very important to remember!
			GUI.EndGroup ();
		} else {
			Time.timeScale = 1;
		}
		if (isWaiting_sending) {
			Time.timeScale = 0;
			GUI.BeginGroup (new Rect (Screen.width / 2 - 75, Screen.height / 2 - 100, 150, 200));
			// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.
			
			// We'll make a box so you can see where the group is on-screen.
			GUI.Box (new Rect (0, 0, 150, 200), "");
			//GUI.Button (new Rect (10, 40, 80, 30), "Save");
			GUI.Label (new Rect (0, 0, 150, 200), "Ready!!!! Waiting for opponent to press the button");		
			// End the group we started above. This is very important to remember!
			GUI.EndGroup ();
		} else {
			Time.timeScale = 1;
		}
		if (isWaiting_opp) {
			Time.timeScale = 0;
			GUI.BeginGroup (new Rect (Screen.width / 2 - 75, Screen.height / 2 - 100, 150, 200));
			// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.
			
			// We'll make a box so you can see where the group is on-screen.
			GUI.Box (new Rect (0, 0, 150, 200), "");
			//GUI.Button (new Rect (10, 40, 80, 30), "Save");
			GUI.Label (new Rect (0, 0, 150, 200), "Your opponent is ready!! Hurry up!!!");		
			// End the group we started above. This is very important to remember!
			GUI.EndGroup ();
		} else {
			Time.timeScale = 1;
		}
	}
	void OnJoinedLobby()
	{
		PhotonNetwork.JoinRandomRoom();
	}
	void OnPhotonRandomJoinFailed()
	{
		PhotonNetwork.CreateRoom(null,true,true,2);
		//PhotonNetwork.JoinRandomRoom ();
	}
	[RPC]
	void SpawnEnemy(){
		GameObject tank = (GameObject)Instantiate (TankPrefabBlue, beginPoint.transform.position, Quaternion.identity);
	}

	[RPC]
	void SetOppStatics(int a,int b,int c,int d){
		GameStatics.opp_gameScore = a;
		GameStatics.opp_cash = b;
		GameStatics.opp_waves = c;
		GameStatics.opp_lives = d;

	}

	public void SetReady(){
		if (GameStatics.opp_ready) {
			isWaiting_sending = false;
			isWaiting_opp = false;
			photonView.RPC("OppOK_back",PhotonTargets.Others);
			GameStatics.systemMain.SendWave ();
		} else {
			isWaiting_sending = true;
			photonView.RPC("OppOK",PhotonTargets.Others);
		}
	}
	[RPC]
	void OppOK(){
		GameStatics.opp_ready = true;
		isWaiting_opp = true;
	}
	[RPC]
	void OppOK_back(){
		GameStatics.opp_ready = true;
		isWaiting_sending = false;
		isWaiting_opp = false;
		GameStatics.systemMain.SendWave ();
	}

}
