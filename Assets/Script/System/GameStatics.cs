using UnityEngine;
using System.Collections;

public class GameStatics : MonoBehaviour
{
    public static int gameScore;
    public static int cash;
    public static int waves;
    public static int lives;
    public static float waveTime;
    public static int   restEnemyNum = 0;
    public static GameObject selectedTower;

	public static int opp_gameScore;
	public static int opp_cash;
	public static int opp_waves;
	public static int opp_lives;

	public static bool ready = false;
	public static bool opp_ready = false;
	//public static float opp_waveTime;
	//public static GameObject opp_selectedTower;


    public static SystemMain systemMain;
    // Use this for initialization

    //names of the scenes
    public static string SCENE_MAINMENU     = "mainMenu";
    public static string SCENE_CHOOSESTAGE  = "chooseStagePage";
    public static string SCENE_GAME         = "sceneNoSystem";
    public static string SCENE_MULTIGAME    = "multiGame";
    public static string SCENE_SETTINGS     = "settings";
    public static string SCENE_ABOUT        = "about";

    //name of the system objects of every scene
    public static string SCENESYSTEM_OBJ_NAME = "SceneSystem";
}
