using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct SubwaveInfo
{
    public SubwaveInfo( SystemMain.EnemyType type, int num )
    {
        enemyType = type;
        enemyNum   = num;
    }
    public SystemMain.EnemyType enemyType;
    public int                  enemyNum;

    public override string ToString()
    {
        return "EnemyType: " + enemyType.ToString() + " EnemyNum: " + enemyNum;
    }
};



public class Wave{


    protected int currentWaveIndex;
    public List<SubwaveInfo> subwaves = new List<SubwaveInfo>();

	// Use this for initialization
	void Start () {
        currentWaveIndex = 0;
	}

    public void AddSubwave( SystemMain.EnemyType enemyType, int enemyNum )
    {
        subwaves.Add( new SubwaveInfo( enemyType, enemyNum ) );
    }

    public void ResetLevel()
    {
        currentWaveIndex = 0;
    }

    public void NextSubwave()
    {
        currentWaveIndex ++;
    }

    public bool IsEnded()
    {
        return currentWaveIndex >= subwaves.Count;
    }


    public SubwaveInfo CurrentSubwave
    {
        get { return subwaves[currentWaveIndex]; }
    }

    public int CurrentWaveIndex
    {
        get{ return currentWaveIndex; }
    }

}
