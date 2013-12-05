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
        systemMain = GetComponent<SystemMain>();
        stageMgr   = GetComponent<StageManager>();
	}
	
	// Update is called once per frame
	void Update () {
	    
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
        systemMain.ChangePage( SystemMain.SceneID.GAME );
    }

    protected void ExitToMainMenu()
    {
        systemMain.ChangePage( SystemMain.SceneID.MAIN_MENU );
    }


}
