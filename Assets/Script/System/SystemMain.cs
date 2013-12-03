using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class SystemMain : MonoBehaviour {

	//Enemy Prefabs
	public GameObject tank;
	public GameObject beginPoint;
	
    //tang
    public GameObject[] prefabEnemies;
    public enum EnemyType
    {
        A,
        B,
        C,
        D,
        E
    };
    protected List<Wave> waveList = new List<Wave>();
    protected int currentWaveNum = 1;

	void Start () {
		GameStatics.waves = 1;
		GameStatics.cash = 100;
		GameStatics.gameScore = 0;
		GameStatics.lives = 20;
		GameStatics.waveTime = 1f;

        SetupWaves();
        currentWaveNum = 1;
	}
	
	// Update is called once per frame
	void Update () {

	}



	//public function for manipulating GameStatics
	void AddScore(int i){GameStatics.gameScore += i;}
	void AddCash(int i){GameStatics.cash += i;}
	void AddLives(int i){GameStatics.lives+= i;}
	void AddWaves(int i){GameStatics.waves += i;}


    /*-----------------------------Start By Tang: Waves--------------------*/


    // fucking hard-code the waves data
    protected void SetupWaves()
    {
        Wave wave0 = new Wave();
        waveList.Add( wave0 );
        wave0.AddSubwave(EnemyType.A, 10);
        wave0.AddSubwave(EnemyType.B, 20);

        Wave wave1 = new Wave();
        waveList.Add( wave1 );
        wave1.AddSubwave( EnemyType.B, 15 );
        wave1.AddSubwave( EnemyType.A, 15 );

        Wave wave2 = new Wave();
        waveList.Add( wave2 );
        wave2.AddSubwave( EnemyType.A, 5 );
        wave2.AddSubwave( EnemyType.B, 10 );
        wave2.AddSubwave( EnemyType.A, 5 );
        wave2.AddSubwave( EnemyType.B, 17 );
    
    }
   
    public void SendWave(){
		//SetNextWave ();
		//StartCoroutine("WaveCoroutine");
        if ( currentWaveNum >= waveList.Count ) {
            Debug.Log( "No wave left!" );
            return;
        }

        Wave wave = waveList[currentWaveNum];
        if (wave.IsEnded()){
            return;
        }

        GameStatics.waves = currentWaveNum + 1;
        StartCoroutine( sendSubwaves(wave) );
        currentWaveNum++;
        
	}

    protected IEnumerator sendSubwaves( Wave wave )
    {
        while ( !wave.IsEnded() ) {
            SubwaveInfo subwave = wave.CurrentSubwave;
            int num = subwave.enemyNum;
            for ( int i = 0; i < num; i++ ) {

                yield return new WaitForSeconds( GetEnemyStartPeriod( subwave.enemyType ) );
                Instantiate( prefabEnemies[(int) subwave.enemyType], beginPoint.transform.position, Quaternion.identity );
            }

            wave.NextSubwave();
        }
    }


    protected float GetEnemyStartPeriod( EnemyType type )
    {
        Enemy enemy = GetEnemyInstanceByType( type );
        return enemy.StartPeriod;
    }

    protected Enemy GetEnemyInstanceByType( EnemyType type )
    {
        Enemy enemy = null;
        switch ( type ) {
            case EnemyType.A: enemy = new Tank();   break;
            case EnemyType.B: enemy = new TankB();  break;
            case EnemyType.C: break;
            case EnemyType.D: break;
            case EnemyType.E: break;
        }
       
        return enemy;
    }
    /*--------------------------End------------------------*/


    //這段我整個複寫ㄏㄏ  by Tang
    /*
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
                //Tank tt;
                //tt = (Tank)
                Instantiate (tank, beginPoint.transform.position, Quaternion.identity);
				
                yield return new WaitForSeconds (GameStatics.waveTime);
            }
        SetNextWave ();
    }
    */
	
}
