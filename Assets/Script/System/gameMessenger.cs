using UnityEngine;
using System.Collections;

public class gameMessenger : MonoBehaviour {

    public SystemMain systemMain;

	// Use this for initialization
	private bool showGameOver = false;
    private bool showWinGame  = false;
	void Start () {
        systemMain = GameStatics.systemMain;
	}
	
	// Update is called once per frame
	void Update () {
        if ( GameStatics.lives == 0 ) {
            GameOver();
        }
        
        //這段可以加進去  看要怎麼加~
        if ( GameStatics.systemMain.waveManager.Win ) {
            WinGame();
        }
        

	}

    
    public void WinGame()
    {
        systemMain.WinStage();
        Time.timeScale = 0;
        showWinGame = true;
    }
    


	public void GameOver(){
        systemMain.FailedStage();
		Time.timeScale = 0;
		showGameOver = true;
	}
	void OnGUI(){
        if ( showWinGame ) {
            GUI.BeginGroup( new Rect( Screen.width / 2 - 75, Screen.height / 2 - 100, 150, 200 ) );
            // All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.

            // We'll make a box so you can see where the group is on-screen.
            GUI.Box( new Rect( 0, 0, 150, 200 ), "你幹爆這關了!!!!!" );
            //GUI.Button (new Rect (10, 40, 80, 30), "Save");
            if ( GUI.Button( new Rect( 17, 40, 120, 30 ), "Restart" ) ) {
                Time.timeScale = 1;
                systemMain.ChangeToScene( GameStatics.SCENE_GAME );
 			}
            if ( GUI.Button( new Rect( 17, 80, 120, 30 ), "選擇關卡" ) ) {
                Time.timeScale = 1;
                systemMain.ChangeToScene( GameStatics.SCENE_CHOOSESTAGE );
            }
            if ( GUI.Button( new Rect( 17, 120, 120, 30 ), "主選單" ) ) {
                Time.timeScale = 1;
                systemMain.ChangeToScene( GameStatics.SCENE_MAINMENU );
            }
			if (GUI.Button (new Rect (17, 160, 120, 30), "Exit")) {
                Application.Quit();
            }

            // End the group we started above. This is very important to remember!
            GUI.EndGroup();
        }



		if (showGameOver) {
			GUI.BeginGroup (new Rect (Screen.width / 2 - 75, Screen.height / 2 - 100, 150, 200));
			// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.

			// We'll make a box so you can see where the group is on-screen.
			GUI.Box (new Rect (0, 0, 150, 200), "Game Over!!!!");
			//GUI.Button (new Rect (10, 40, 80, 30), "Save");
			if (GUI.Button (new Rect (17, 40, 120, 30), "Restart")) {
					Time.timeScale = 1;
                    systemMain.ChangeToScene( GameStatics.SCENE_GAME );
			}
            if ( GUI.Button( new Rect( 17, 80, 120, 30 ), "選擇關卡" ) ) {
                Time.timeScale = 1;
                systemMain.ChangeToScene( GameStatics.SCENE_CHOOSESTAGE );
            }
            if ( GUI.Button( new Rect( 17, 120, 120, 30 ), "主選單" ) ) {
                Time.timeScale = 1;
                systemMain.ChangeToScene( GameStatics.SCENE_MAINMENU );
            }
			if (GUI.Button (new Rect (17, 160, 120, 30), "Exit")) {
					Application.Quit ();
			}

			// End the group we started above. This is very important to remember!
			GUI.EndGroup ();
		}
	}
}
