using UnityEngine;
using System.Collections;

public class GUI_Disp : MonoBehaviour
{

    public Texture2D icon;
    public Texture2D icon2;
    public Texture2D iconLaserTower;
    public Texture2D iconSlowTower;
    public Texture2D iconGun;
    public Texture2D iconPoisonGun;
    public Texture2D iconBombWeapon;
    public Texture2D iconSplittingCube;
    public Texture2D iconSpiralWeapon;
    public Texture2D iconEraserWeapon;
    public GameObject laserTower;
    public GameObject slowTower;
    public GameObject Gunn;
    public GameObject poisonGun;
    public GameObject bombWeapon;
    public GameObject splittingCube;
    public GameObject spiralWeapon;
    public GameObject eraserWeapon;




    private bool showMenu = false;
    private bool showBotBar = true;
    private bool showSideBar = false;
    //private bool isPlacing = false;
    // Use this for initialization
    void Start()
    {

    }


    public void toggleMenu()
    {
        showMenu = !showMenu;
        Time.timeScale = ( Time.timeScale == 1 )?0:1;
        Debug.Log( "toggle!!" );
    }

    public void hideBotBar()
    {
        showBotBar = false;
    }

    public void toggleSideBar()
    {
        showSideBar = !showSideBar;
    }
    // Update is called once per frame

    void OnGUI()
    {

        //Debug.Log(guiT.text);
        //print(aa);
        if ( GUI.Button( new Rect( 10, 10, 80, 30 ), "Next Wave" ) ) {
            //print ("You clicked the button!");
            GameStatics.systemMain.SendWave();
        }

        //Menu
        if ( showMenu ) {
            GUI.BeginGroup( new Rect( Screen.width / 2 - 75, Screen.height / 2 - 150, 150, 200 ) );
            // All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.

            // We'll make a box so you can see where the group is on-screen.
            GUI.Box( new Rect( 0, 0, 150, 200 ), "Menu" );
            //GUI.Button (new Rect (10, 40, 80, 30), "Save");
            if ( GUI.Button( new Rect( 17, 40, 120, 30 ), "Resume" ) ) {
                toggleMenu();
            }
            if ( GUI.Button( new Rect( 17, 80, 120, 30 ), "選關" ) ) {
                GameStatics.systemMain.ChangeToScene( GameStatics.SCENE_CHOOSESTAGE );
            }
            if ( GUI.Button( new Rect( 17, 120, 120, 30 ), "主選單" ) ) {
                GameStatics.systemMain.ChangeToScene( GameStatics.SCENE_MAINMENU );
            }
            if ( GUI.Button( new Rect( 17, 160, 120, 30 ), "Exit" ) ) {
                Application.Quit();
            }

            // End the group we started above. This is very important to remember!
            GUI.EndGroup();
        }
        //Button Menu
        if ( showBotBar ) {

            //TODO:Switch to  GUILayout.area...
            GUI.BeginGroup( new Rect( 0, Screen.height *0.79f, Screen.width, Screen.height *0.21f ) );
            // All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.

            // We'll make a box so you can see where the group is on-screen..
            GUI.Box( new Rect( 0, 0, Screen.width, Screen.height / 8 + 50 ), "" );
            GUILayout.BeginArea( new Rect( Screen.width/20, Screen.height/20, Screen.width /5, Screen.height/8 ) );

            //GUI.Button (new Rect (10, 40, 80, 30), "Save");
            GUILayout.BeginVertical();
            /*GUI.Label (new Rect(10,10,100,100),"Wave: "+GameStatics.waves.ToString ());
            GUI.Label (new Rect(10,30,100,100),"Cash: $ "+GameStatics.cash.ToString ());
            GUI.Label (new Rect)*/

            
            GUIStyle labelStyle = new GUIStyle();
            GUI.contentColor = Color.white;
            GUI.color = Color.white;
            labelStyle.fontSize = 13;
            labelStyle.normal.textColor = Color.white;

            GUILayout.Label( "Wave :   "+GameStatics.waves.ToString() , labelStyle);
            GUILayout.Label( "Cash :$  "+GameStatics.cash.ToString() , labelStyle);
            GUILayout.Label( "Lives:   "+GameStatics.lives.ToString() , labelStyle);
            GUILayout.EndVertical();
            GUILayout.EndArea();


            GUILayout.BeginArea( new Rect( Screen.width*0.81f, Screen.height/80, Screen.width/5, Screen.height / 5 ) );
            GUILayout.BeginVertical();
            if ( GameStatics.selectedTower!=null ) {
                Weapon temp = GameStatics.selectedTower.GetComponent<Weapon>();
                GUILayout.BeginVertical();

                GUIStyle style = new GUIStyle();
                style.alignment = TextAnchor.MiddleCenter;
                style.normal.textColor = Color.white;
                style.fontSize         = 15;
                GUILayout.Label( GameStatics.selectedTower.name, style);

                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical();
                GUILayout.Label( "Level: " + temp.Level.ToString() );
                GUILayout.Label( "Cost: $ "+ temp.cost * 0.2f );
                GUILayout.Label( "Sell: "+ ( temp.cost * (int) ( 0.8f + temp.Level * 0.2f ) ) + "$" );
                GUILayout.EndVertical();

                GUILayout.BeginVertical();
                GUI.enabled = temp.cost*0.2f <= GameStatics.cash;
                if ( GUILayout.Button( "LEVEL UP" ,GUILayout.Height(Screen.height/13))) {
                    //Debug.Log ("itchy");
                    temp.LevelUp();
                    GameStatics.cash -= (int) ( temp.cost*0.2f );
                }
                GUI.enabled = true;

                if ( GUILayout.Button( "SELL", GUILayout.Height( Screen.height/26 ) ) ) {
                    GameStatics.cash += (int) ( temp.cost * ( 0.8f + temp.Level * 0.2f ) );
                    DestroyObject( GameStatics.selectedTower );
                    GameStatics.selectedTower = null;
                }
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
                
            }


            GUILayout.EndVertical();
            // End the group we started above. This is very important to remember!
            GUILayout.EndArea();
            GUI.EndGroup();
        }



        //WeaponChooser

        GUILayout.BeginArea( new Rect( Screen.width/4, Screen.height*4/5, Screen.width/2, Screen.height/5 ) );
        // All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.

        // We'll make a box so you can see where the group is on-screen.

        //GUILayout.Box (new Rect (0, 0, 150, Screen.height/7 * 3));
        //GUI.Button (new Rect (10, 40, 80, 30), "Save");

        //GUILayout.Label (new Rect (10, 10, 100, 100), "Score: " + GameStatics.gameScore.ToString ());
        //GUILayout.Label (new Rect (10, 30, 100, 100), "Cash: $ " + GameStatics.cash.ToString ());


        //塔的按鈕
        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical();
        GUI.enabled = GameStatics.cash >= ( laserTower.GetComponent<LaserGun>() ).cost;
        if ( GUILayout.Button( iconLaserTower , GUILayout.Height(Screen.height/9)) ) {
            //gameObject.getComponent("HoverTest");
            HoverTest hh = gameObject.GetComponent<HoverTest>();
            hh.SetHover( laserTower );
            toggleSideBar();

        }
        GUILayout.Label( "  $ " +  ( laserTower.GetComponent<LaserGun>() ).cost );
        GUILayout.EndVertical();

        GUILayout.BeginVertical();
        GUI.enabled = GameStatics.cash >= ( slowTower.GetComponent<SlowGun>() ).cost;
        if ( GUILayout.Button( iconSlowTower, GUILayout.Height( Screen.height/9 ) ) ) {
            HoverTest hh = gameObject.GetComponent<HoverTest>();
            hh.SetHover( slowTower );
            toggleSideBar();
        }
        GUILayout.Label( "  $ " +  ( slowTower.GetComponent<SlowGun>() ).cost );
        GUILayout.EndVertical();

        GUILayout.BeginVertical();
        GUI.enabled = GameStatics.cash >= ( poisonGun.GetComponent<PosionGun>() ).cost;
        if ( GUILayout.Button( iconPoisonGun, GUILayout.Height( Screen.height/9 ) ) ) {
            HoverTest hh = gameObject.GetComponent<HoverTest>();
            hh.SetHover( poisonGun );
            toggleSideBar();
        }

        GUILayout.Label( "  $ " +  ( poisonGun.GetComponent<PosionGun>() ).cost );
        GUILayout.EndVertical();

        GUILayout.BeginVertical();

        GUI.enabled = GameStatics.cash >= ( bombWeapon.GetComponent<BombWeapon>() ).cost;
        if ( GUILayout.Button( iconBombWeapon, GUILayout.Height( Screen.height/9 ) ) ) {
            HoverTest hh = gameObject.GetComponent<HoverTest>();
            hh.SetHover( bombWeapon );
            toggleSideBar();
        }
        GUILayout.Label( "  $ " +  ( bombWeapon.GetComponent<BombWeapon>() ).cost );
        GUILayout.EndVertical();

        GUILayout.BeginVertical();
        GUI.enabled = GameStatics.cash >= ( splittingCube.GetComponent<SplittingCubeWeapon>() ).cost;
        if ( GUILayout.Button( iconSplittingCube, GUILayout.Height( Screen.height/9 ) ) ) {
            HoverTest hh = gameObject.GetComponent<HoverTest>();
            hh.SetHover( splittingCube );
            toggleSideBar();
        }
        GUILayout.Label( "  $ " +  ( splittingCube.GetComponent<SplittingCubeWeapon>() ).cost );
        GUILayout.EndVertical();

        GUILayout.BeginVertical();
        GUI.enabled = GameStatics.cash >= ( spiralWeapon.GetComponent<SpiralEmitterWeapon>() ).cost;
        if ( GUILayout.Button( iconSpiralWeapon, GUILayout.Height( Screen.height/9 ) ) ) {
            HoverTest hh = gameObject.GetComponent<HoverTest>();
            hh.SetHover( spiralWeapon );
            toggleSideBar();
        }

        GUILayout.Label( "  $ " +  ( spiralWeapon.GetComponent<SpiralEmitterWeapon>() ).cost );
        GUILayout.EndVertical();

        GUILayout.BeginVertical();
        GUI.enabled = GameStatics.cash >= ( eraserWeapon.GetComponent<EraserWeapon>() ).cost;
        if ( GUILayout.Button( iconEraserWeapon, GUILayout.Height( Screen.height/9 ) ) ) {
            HoverTest hh = gameObject.GetComponent<HoverTest>();
            hh.SetHover( eraserWeapon );
            toggleSideBar();
        }

        GUILayout.Label( "  $ " +  ( eraserWeapon.GetComponent<EraserWeapon>() ).cost );
        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
        GUI.enabled = true;


        // End the group we started above. This is very important to remember!
        GUILayout.EndArea();
        if ( GUI.Button( new Rect( Screen.width - 177, Screen.height / 3 +10, 25, 30 ), icon2 ) ) {
            toggleSideBar();
        }

    }
}
