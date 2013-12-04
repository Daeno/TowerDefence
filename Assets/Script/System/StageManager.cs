using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class StageManager : MonoBehaviour {

    protected int currentStateIndex;
    public int singlePlayStageNum = 1;
    public int multiPlayStageNum  = 1;
    public List<GameObject> singlePlayRoutes = new List<GameObject>();
    public List<GameObject> multiPlayRoutes = new List<GameObject>();
    protected List<Stage> singlePlayStages = new List<Stage>();
    protected List<Stage> multiPlayStages = new List<Stage>();

    public static string STR_SPLITTER = SaveLoadManager.STR_SPLITTER;

    public class Stage
    {
        //saved data 
        public int              stageIndex;
        public bool             passed = false;
        public int              score  = 0;

        //not saved data
        public GameObject       route;
        public List<Wave>       waveList = new List<Wave>();
        public Sprite           backGround;
    }


	// Use this for initialization
	void Start () {
        InitStages();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public List<Wave> GetWaveList( bool single, int stageIdx )
    {
        return GetStage(single, stageIdx).waveList;
    }

    public GameObject GetRoute( bool single, int stageIdx )
    {
        return GetStage( single, stageIdx ).route;
    }

    public Stage GetStage( bool single, int stageIdx )
    {
        try {
            return ( single ? singlePlayStages[stageIdx] : multiPlayStages[stageIdx] );
        }
        catch ( ArgumentOutOfRangeException e ) {
            Debug.LogError( "ArgumentOutOfRangeException in StageManager::GetStage(bool single, int stageIdx). The parameter is out of range" );
        }
        return null;
    }


    public void PassStage( bool single, int stageIdx )
    {
        GetStage( single, stageIdx ).passed = true ;
    }

    


    public int GetLastPassedStageIndex()
    {
        foreach ( Stage s in singlePlayStages ) {
            if ( s.passed == false ) {
                return s.stageIndex - 1;
            }
        }
        return -1;
    }

    //stage
    // "lastpassedstage/otherInformation1/otherInformation2"
    //
    //

    public void LoadStageData()
    {
        SaveLoadManager slMgr           = GetComponent<SaveLoadManager>();
        string          stageData       = slMgr.GetSavedStageData();
        
        //first time play
        if ( stageData == null ) {
            return;
        }

        string[]        dataArr         = stageData.Split( '/' );
        int             lastPassedStage = int.Parse( dataArr[0] );


        Debug.Log( "loading stage data: lastPassedStage = " + lastPassedStage );


        for ( int i = 0; i < singlePlayStages.Count; i++ ) {
            singlePlayStages[i].passed = ( i <= lastPassedStage );
            
            //if someday we have scores, add this line
            //singlePlayStages[i].score  = singleStagesData[i].score;
        }
    }

    public string GetStageDataStr()
    {
        string data = "";
        int    lastPassedStage = -1;
        int[]  scores = new int[singlePlayStageNum];

        lastPassedStage = GetLastPassedStageIndex();

        for ( int i = 0; i < singlePlayStageNum; i++ ) {
            scores[i] = singlePlayStages[i].score;
        }

        data += lastPassedStage.ToString();
        data += STR_SPLITTER;

        //if some day we have scores, add this block
        /*
        for ( int i = 0; i < singlePlayStageNum; i++ ) {
            data += scores[i].ToString();
            
            if (i != singlePlayStageNum - 1)
                data += ",";
        }
        */

        return data;
    }


    //fucking hardcoding waves here
    protected void InitStages()
    {
        List< List<Wave> > singleWaveLists = new List< List<Wave> >();
        List< List<Wave> > multiWaveLists = new List<List<Wave>>();
        List<Wave> waveList;

        //------------------------SINGLE------------------------//

        //single stage 0
        waveList = new List<Wave>();
        singleWaveLists.Add( waveList );

        Wave wave0 = new Wave();
        waveList.Add( wave0 );
        wave0.AddSubwave( SystemMain.EnemyType.A, 3 );
        wave0.AddSubwave( SystemMain.EnemyType.B, 5 );

        Wave wave1 = new Wave();
        waveList.Add( wave1 );
        wave1.AddSubwave( SystemMain.EnemyType.B, 2 );
        wave1.AddSubwave( SystemMain.EnemyType.A, 5 );

        Wave wave2 = new Wave();
        waveList.Add( wave2 );
        wave2.AddSubwave( SystemMain.EnemyType.A, 5 );
        wave2.AddSubwave( SystemMain.EnemyType.B, 10 );
        wave2.AddSubwave( SystemMain.EnemyType.A, 5 );
        wave2.AddSubwave( SystemMain.EnemyType.B, 17 );


        //------------------MULTI---------------------//

        //multi stage 0
        waveList = new List<Wave>();
        multiWaveLists.Add( waveList );



        //----------------add to stage lists-------------//
        for ( int i = 0; i < singlePlayStageNum; i++ ) {
            singlePlayStages.Add(new Stage());
            singlePlayStages[i].stageIndex = i;
            singlePlayStages[i].route      = singlePlayRoutes[i];
            singlePlayStages[i].waveList   = singleWaveLists[i];
        }

        for ( int i = 0; i < multiPlayStageNum; i++ ) {
            multiPlayStages.Add( new Stage() );
            multiPlayStages[i].stageIndex   = i;
            Debug.LogWarning( "Add single-play route to a multiPlayStage, in StageManager.InitStages()." );
            multiPlayStages[i].route        = singlePlayRoutes[i]; //還沒有multi的地圖可以加
            multiPlayStages[i].waveList     = multiWaveLists[i];
        }

    }


}
