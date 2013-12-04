using UnityEngine;
using System.Collections;

public class SaveLoadManager : MonoBehaviour {
    public SystemMain systemMain;

    public static string STAGE_STATE_STR = "stagestate";
    
    //stage
    // "[passedstage0],[passedstage1],[ps2],...]/otherInformation1/otherInformation2"
    //
    //



	// Use this for initialization
	void Start () {
        systemMain = GetComponent<SystemMain>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}


    public void SaveSnapshot()
    {
        
    }

    public void SaveWhenEnd()
    {
    }

    public void LoadSnapshot()
    {
       
    }

    public void LoadWhenBegin()
    {
    }


}
