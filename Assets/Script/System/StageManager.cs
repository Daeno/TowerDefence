using UnityEngine;
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


    public class Stage
    {
        public int              stageIndex;
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
        return ( single ? singlePlayStages[stageIdx] : multiPlayStages[stageIdx] );
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
