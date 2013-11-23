
欄位皆為大寫開頭，表示以C#的get、set設定
目前其小寫的同名欄位都還是public方便在UNITY中設定，
系統完整之後將把大部分改為由程式中設定，並改為private。

使用get set的目的：將本體和外部隔開，並控制讀寫權限，也可能在其中實作事件通知功能。

class Enemy

    public float OriginalSpeed(RW) 
    public float Speed        (RW) //減速過之類的
    public int   MaxLife      (RW) //滿血
    public int   Life         (R)  //這隻的血量
    public Transform MyTransform(R)
    
    public void  SetRoute(GameObject routePrefab) //設定路徑

    //開始跑：  不要直接initiate就開始跑，因為可能必須用script重設路徑或其他資訊，弄完最後再CALL這個開始跑。
    public void  StartMoving()
    
    public void  Attacked(float damage)
    public void  Slowed(float lastTime, float slowratio)  //延遲時間，減速比率
    public void  Poisoned(float lastTime, float damage)   //延遲時間，每秒扣血量
    public bool  IsSlowed()
    public bool  IsPoisoned()
    public bool  IsAlive()
    public bool  IsReached() //到達終點
    

    public void  DestroyGameObject()   //不再需要它了 就殺掉它吧
    public void  MoveTo(Vector2 pos)   
    public void  Translate(float x, float y)
    public void  Translate(Vector2 trans)
    public void  Rotate(Quarternion rot)  //直接將transform.rotation設為rot
    public void  Rotate(float degAntiCW)  //逆時針旋轉角度
    
    


class Weapon
    public float DetectRadius   (R)
    public float ShootPeriod    (R)
    public int   MaxLevel       (RW)
    public int   Level          (RW)
    public float AttackDamage   (R)
    public Transform myTransform(RW)
    
    public float[] attackDamageList (RW) = { 0, 10, 20, 30, 40, 50} //每個等級的攻擊力，0等 = 0，數字亂訂的 可依遊戲性調整
    public float[] shootPeriodList  (RW) = { 1<<10, 1, 0.9, 0.8, 0.6, 0.4} 
    public float[] detectRadiusList (RW) = { 0, 10, 15, 20, 25, 35}
    
    public void Attack();
    public void LevelUp()
    public void KillEnemy(GameObject enemy)
    public void DestroyGameObject() //刪掉物件  玩家選擇刪除之類的
    
    public void MoveTo(Vector2 pos)
    public void Translate(float x, float y)
    public void Translate(Vector2 trans)
    public void Rotate(Quarternion rot)  //直接將transform.rotation設為rot
    public void Rotate(float degAntiCW)  //逆時針旋轉角度
    

class LaserGun
    public GameObject PrefabBullet
class Gun
    public GameObject PrefabLaserBullet
    
class SlowGun
    public GameObject prefabSlowBullet;    
    public float SlowRatio (R)
    public float SlowTime  (R)
    public float[] slowRatioList = //數字亂訂的 可依遊戲性調整
                                    { 1f,   0.7f, 0.6f, 0.5f, 0.4f, 0.3f  };

    public float[] slowTimeList = //數字亂訂的 可依遊戲性調整
                                    {0f, 5f, 6f, 8f, 11f, 15f };

