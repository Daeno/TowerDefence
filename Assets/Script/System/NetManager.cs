using UnityEngine;
using System.Collections;

public class NetManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings("0.1");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI(){
		GUILayout.BeginArea (new Rect (10, 50, 100, 500));
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		foreach (Room game in PhotonNetwork.GetRoomList())
		{
			GUILayout.Label(game.name + " " + game.playerCount + "/" + game.maxPlayers);
		}

		GUILayout.EndArea();
	}
	void OnJoinedLobby()
	{
		PhotonNetwork.JoinRandomRoom();
	}
	void OnPhotonRandomJoinFailed()
	{
		PhotonNetwork.CreateRoom(null);
	}
	[RPC]
	void SpawnEnemy(){
		
	}
}
