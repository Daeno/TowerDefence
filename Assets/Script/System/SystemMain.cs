using UnityEngine;
using System.Collections;

public class SystemMain : MonoBehaviour {

	//Enemy Prefabs
	public Tank tank;
	public GameObject beginPoint;
	// Use this for initialization
	void SetNextWave(){
		//GameStatics.waveTime += 10;
		GameStatics.waves++;
		if (GameStatics.waveTime > 0.001f)
						GameStatics.waveTime *= 0.9f;
	}
	IEnumerator WaveCoroutine(){
		//生成
		//yield return new WaitForEndOfFrame ();
		//Debug.Log ("Coroutine");
			for (int i = 0; i < GameStatics.waves + 5; i++){

				Instantiate (tank, beginPoint.transform.position, Quaternion.identity);
				yield return new WaitForSeconds (GameStatics.waveTime);
			}
		SetNextWave ();
	}
	void Start () {
		GameStatics.waves = 1;
		GameStatics.cash = 100;
		GameStatics.gameScore = 0;
		GameStatics.lives = 20;
		GameStatics.waveTime = 1f;


	}
	
	// Update is called once per frame
	void Update () {


	}

	public void SendWaves(){
		//SetNextWave ();
		StartCoroutine("WaveCoroutine");
	}

	//public function for manipulating GameStatics
	void AddScore(int i){GameStatics.gameScore += i;}
	void AddCash(int i){GameStatics.cash += i;}
	void AddLives(int i){GameStatics.lives+= i;}
	void AddWaves(int i){GameStatics.waves += i;}




}
