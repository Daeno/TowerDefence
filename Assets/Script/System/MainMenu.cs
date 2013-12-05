using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    SystemMain systemMain;

	// Use this for initialization
	void Start () {
        systemMain = GetComponent<SystemMain>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI()
    {
        GUI.BeginGroup( new Rect( 0, 0, 800, 80 ) );
        if ( GUI.Button( new Rect( 10, 40, 80, 30 ), "Single" ) ) {
            EnterSingleGame();
        }
        if ( GUI.Button( new Rect( 110, 40, 80, 30 ), "Multi" ) ) {
            EnterMultiGame();
        }
        if ( GUI.Button( new Rect( 210, 40, 80, 30 ), "Settings" ) ) {
            EnterSettings();
        }
        if ( GUI.Button( new Rect( 310, 40, 80, 30 ), "About" ) ) {
            EnterAbout();
        }
        if ( GUI.Button( new Rect( 410, 40, 80, 30 ), "Exit" ) ) {
            Application.Quit();
        }
        GUI.EndGroup();
    
	}



    protected void EnterSingleGame()
    {
        Application.LoadLevel( GameStatics.SCENE_CHOOSESTAGE );
    }

    protected void EnterMultiGame()
    {
        Application.LoadLevel( GameStatics.SCENE_MULTIGAME );
    }

    protected void EnterSettings()
    {
        Application.LoadLevel( GameStatics.SCENE_SETTINGS );
    }

    protected void EnterAbout()
    {
        Application.LoadLevel( GameStatics.SCENE_ABOUT );
    }

}
