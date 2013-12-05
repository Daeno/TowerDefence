using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    SystemMain systemMain;

    public Texture btnTexture;

	// Use this for initialization
	void Start () {
        systemMain = GameStatics.systemMain;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI()
    {
        GUIStyle guiStyle = new GUIStyle();
        GUI.BeginGroup( new Rect( 0, Screen.height /2 , Screen.width, Screen.height /5 ) );

        if ( GUI.Button( new Rect( Screen.width/20,                    0, Screen.height /10, Screen.height / 10 ), btnTexture )) {
            EnterSingleGame();
        }
        if ( GUI.Button( new Rect( Screen.width / 5 + Screen.width/20, 0, Screen.width/10, Screen.height / 10 ), "Multi" ) ) {
            EnterMultiGame();
        }
        if ( GUI.Button( new Rect( 2*Screen.width / 5 + Screen.width/20, 0, Screen.width/10, Screen.height / 10 ), "Settings" ) ) {
            EnterSettings();
        }
        if ( GUI.Button( new Rect( 3*Screen.width / 5 + Screen.width/20, 0, Screen.width/10, Screen.height / 10 ), "About" ) ) {
            EnterAbout();
        }
        if ( GUI.Button( new Rect( 4*Screen.width / 5 + Screen.width/20, 0, Screen.width/10, Screen.height / 10 ), "Exit" ) ) {
            Application.Quit();
        }

        /*if ( GUI.Button( new Rect( 10, 40, 80, 30 ), "Single" ) ) {
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
         */
        GUI.EndGroup();
        //GUI.backgroundColor = Color.black;
    
	}



    protected void EnterSingleGame()
    {
        systemMain.ChangeToScene( GameStatics.SCENE_CHOOSESTAGE );
    }

    protected void EnterMultiGame()
    {
        systemMain.ChangeToScene( GameStatics.SCENE_MULTIGAME );
    }

    protected void EnterSettings()
    {
        systemMain.ChangeToScene( GameStatics.SCENE_SETTINGS );
    }

    protected void EnterAbout()
    {
        systemMain.ChangeToScene( GameStatics.SCENE_ABOUT );
    }

}
