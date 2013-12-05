using UnityEngine;
using System.Collections;

public class NetManager : MonoBehaviour {

	public GameObject TankPrefabBlue, beginPoint;
	private bool isWaiting = true;
	private PhotonView photonView;

	private int temp_gameScore;
	private int temp_cash;
	private int temp_waves;
	private int temp_lives;

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
				Debug.Log("Waiting Zzzz");		
			}
			else{
				isWaiting=false;
			}
			if(!isWaiting){
			if(Input.GetMouseButtonDown(0)){
				Debug.Log ("sending 1");
				photonView = PhotonView.Get(this);
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
			GUI.BeginGroup (new Rect (Screen.width / 2 - 75, Screen.height / 2 - 100, 150, 200));
			// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.
			
			// We'll make a box so you can see where the group is on-screen.
			GUI.Box (new Rect (0, 0, 150, 200), "");
			//GUI.Button (new Rect (10, 40, 80, 30), "Save");
			GUI.Label (new Rect (0, 0, 150, 200),"Waiting");		
			// End the group we started above. This is very important to remember!
			GUI.EndGroup ();		
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


}
