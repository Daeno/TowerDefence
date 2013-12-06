using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChooseStagePage : MonoBehaviour {

    SystemMain systemMain;
    StageManager stageMgr;

    //save stages: < index, passed >
    protected List< KeyValuePair<int, bool> > singleStages = new List<KeyValuePair<int, bool> >();
    protected int                             lastPassedStageIdx = -1;

	// Use this for initialization
	void Start () {
        systemMain = GameStatics.systemMain;
        stageMgr   = systemMain.stageManager;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}


    void OnGUI()
    {
        GUI.BeginGroup( new Rect( 0, 0,1920,1280) );
        GUI.contentColor = Color.clear;
        GUI.backgroundColor = Color.clear;
        if ( GUI.Button( new Rect( Screen.width/12, Screen.height/3, Screen.width/7, Screen.height/4 ), "第1關")) {
            EnterStage( 0 );
        }
        /*if ( GUI.Button( new Rect( 110, 40, 80, 30 ), "Back" ) ) {
            ExitToMainMenu();
        }*/
        GUI.EndGroup();

    }
    
    public void UpdateShowStage()
    {
        //first open
        if ( singleStages.Count == 0 ) {
            int sStageNum = stageMgr.singlePlayStageNum;
            for ( int i = 0; i < sStageNum; i++ ) {
                StageManager.Stage stage = stageMgr.GetStage(true, i);
                singleStages.Add( new KeyValuePair<int, bool>( i, stage.passed ) ); 
            }
            lastPassedStageIdx = stageMgr.GetLastPassedStageIndex();
            return;
        }

        //shown enabled stages don't change
        if ( lastPassedStageIdx == stageMgr.GetLastPassedStageIndex() ) {
            return;
        }
        // some stages may have been won.
        else {
            for ( int i = lastPassedStageIdx + 1; i <= stageMgr.GetLastPassedStageIndex(); i++ ) {
                singleStages[i] = new KeyValuePair<int, bool>( i, true );
            }
            

            //update the look
            //TODO
        }
        
    }


    protected void EnterStage( int stageIdx )
    {
        systemMain.SetCurrentStage( true, stageIdx );
        GameStatics.systemMain.ChangeToScene( GameStatics.SCENE_GAME );
    }

    protected void ExitToMainMenu()
    {
        GameStatics.systemMain.ChangeToScene( GameStatics.SCENE_MAINMENU );
    }


}
