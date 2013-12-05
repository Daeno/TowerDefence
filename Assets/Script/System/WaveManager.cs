using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour {

	// Use this for initialization
	//public GameObject[] enemyType;
	//public float waitingTime;
	//public GameObject nextEnemy;
	//public int waveNumber;
	//public float difficulty;
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


	//private int i=0,j=0,k=0;


    public SystemMain       systemMain;
    public GameObject[]     prefabEnemies;
    protected List<Wave>    waveList;
    protected int           currentWaveIndex = 0;
    protected int           maxWaveIndex;
    protected bool          win = false;
    protected int           restEnemyNumThisWave = 0;
    public Vector2          beginPoint;
    public GameObject       route;

    public int CurrentWaveIndex
    {
        get { return currentWaveIndex; }
    }
    public bool Win
    {
        get { return win; }
    }


	void Start () {
		//singleData = new singleWave[30]();
        currentWaveIndex = 0;

        Debug.Log( "WaveManager::start" );
        systemMain    = GameStatics.systemMain;
        prefabEnemies = systemMain.prefabEnemies;
        Reset();
	}


    void Update()
    {
        restEnemyNumThisWave = GameStatics.restEnemyNum;

        Debug.Log( "restNum: " + restEnemyNumThisWave );

        try {
            if ( waveList != null && waveList.Count >= currentWaveIndex && waveList.Count != 0 ) {
                if ( waveList[waveList.Count-1].IsEnded() && restEnemyNumThisWave == 0 && GameStatics.lives > 0 ) {
                    win = true;
                }
            }
        }
        catch ( ArgumentOutOfRangeException e ) {
            Debug.LogError("WaveManager: Update: waveList out Of range");
            return;
        }
    }

    public void Reset()
    {
        currentWaveIndex =  0;
        maxWaveIndex     = -1;
        win              = false;
        restEnemyNumThisWave = 0;
    }

    public void SendWave( List<Wave> waves, GameObject routeObj )
    {
        InitRoute( routeObj );

        waveList     = waves;
        maxWaveIndex = waveList.Count;
        //SetNextWave ();
        //StartCoroutine("WaveCoroutine");
        if ( currentWaveIndex >= maxWaveIndex ) {
            Debug.Log( "No wave left!" );
            return;
        }

        Wave wave = waveList[currentWaveIndex];
        if ( wave.IsEnded() ) {
            return;
        }

        GameStatics.waves = currentWaveIndex + 1;
        StartCoroutine( sendSubwaves( wave ) );
        currentWaveIndex++;
    }

    protected void InitRoute( GameObject routeObj)
    {
        if ( route != routeObj ) {
            this.route = routeObj;
            //this loop is the easiest(maybe only) way to walk through children of an GameObject
            //We get the position of the first child as the "beginPoint:
            foreach ( Transform child in route.transform ) {
                if ( child.name == "1" ) {
                    beginPoint = child.position;
                    break; //after get the value, break!( always the first loop time)
                }
            }
        }
    }

    protected IEnumerator sendSubwaves( Wave wave )
    {
        while ( !wave.IsEnded() ) {
            SubwaveInfo subwave = wave.CurrentSubwave;
            int num = subwave.enemyNum;
            for ( int i = 0; i < num; i++ ) {
                // 如果場面上沒有怪  會直接出怪。有個目的是讓場面上不會有同時"有波在輸出"卻"沒有怪"的情況。
                //如果有上述情況，則按nextWave會壞掉。
                if (GameStatics.restEnemyNum > 0)
                    yield return new WaitForSeconds( GetEnemyStartPeriod( subwave.enemyType ) );

                GameObject enemy = (GameObject) Instantiate( prefabEnemies[(int) subwave.enemyType], beginPoint, Quaternion.identity );
                enemy.GetComponent<Enemy>().SetRoute( route );
                GameStatics.restEnemyNum += 1;
            }

            wave.NextSubwave();
        }
    }


    public static float GetEnemyStartPeriod( SystemMain.EnemyType type )
    {
        Enemy enemy = SystemMain.GetEnemyInstanceByType( type );
        return enemy.StartPeriod;
    }

}
