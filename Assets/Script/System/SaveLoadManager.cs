using UnityEngine;
using System.Collections;

public class SaveLoadManager : MonoBehaviour {
    public SystemMain systemMain;

    public static string STAGE_STATE_STR = "stagestate";
    public static string STR_SPLITTER = "/";


	// Use this for initialization
	void Start () {
        systemMain = GetComponent<SystemMain>();
        LoadWhenBegin();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}


    public void SaveSnapshot()
    {
        
    }

    public void SaveWhenEnd()
    {
        StageManager stageMgr = GetComponent<StageManager>();
        PlayerPrefs.SetString( STAGE_STATE_STR,  stageMgr.GetStageDataStr());
        PlayerPrefs.Save();
    }

    public void LoadSnapshot()
    {
       
    }

    public void LoadWhenBegin()
    {
        StageManager stageMgr = GetComponent<StageManager>();
        stageMgr.LoadStageData();
    }
        
    public string GetSavedStageData()
    {
        return PlayerPrefs.GetString( STAGE_STATE_STR );
    }
}
