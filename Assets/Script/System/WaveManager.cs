using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour {

	// Use this for initialization
	public GameObject[] enemyType;
	public float waitingTime;
	public GameObject nextEnemy;
	public int waveNumber;
	public float difficulty;
	/*public class singleWave{
		public int type;
		public int number;
		public float cd;
		public singleWave(int a,int b,float c){
			type = a;
			number = b;
			cd = c;
		}
	};*/
	//first: waveN, second: type, third: number
	//private singleWave waveData[];
	private int i=0,j=0,k=0;
	void Start () {
		//singleData = new singleWave[30]();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void setNextWave(){
		
	}


}
