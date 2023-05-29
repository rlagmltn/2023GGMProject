using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfo_EnemyButton : MonoBehaviour
{
    int Num = 0;
    public void SetEnemyNum(int num)
    {
        Num = num;
    }

    public int GetEnemyNum()
    {
        return Num;
    }
}
