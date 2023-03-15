using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoSingleton<GoldManager>
{
    /*
     * 골드
     * 스테이지 클리어마다 획득
     * 아이템 구매시에 차감
     * 게임이 끝나면 초기화?
    */

    public int Gold { get; set; }

    public bool AddGold(int amount)
    {
        Gold += amount;
        return true;
    }

    public bool RemoveGold(int amount)
    {
        if (amount > Gold) return false;
        else
        {
            Gold -= amount;
            return true;
        }
    }
}
