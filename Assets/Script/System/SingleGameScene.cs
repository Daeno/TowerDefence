using UnityEngine;
using System.Collections;

public class SingleGameScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SystemMain systemMain = GameStatics.systemMain;
        StageManager stageMgr = systemMain.stageManager;
        Instantiate( stageMgr.prefabTarget, 
                     stageMgr.GetTargetPosition( true, systemMain.currentStageInfo.stageIndex ), 
                     Quaternion.identity 
                    );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
