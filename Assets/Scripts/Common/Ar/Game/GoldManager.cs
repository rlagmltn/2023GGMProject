using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldManager : MonoSingleton<GoldManager>
{
    /*
     * 골드
     * 스테이지 클리어마다 획득
     * 아이템 구매시에 차감
     * 게임이 끝나면 초기화?
    */

    public static int Gold { get; private set; }
    [SerializeField] TextMeshProUGUI tmp;

    private void Start()
    {
        AddGold(0);
    }

    public void ResetGold()
    {
        Gold = 0;
        SaveManager.Instance.GameData.Gold += Gold;
        SaveManager.Instance.GameDataSave();
    }

    public bool AddGold(int amount)
    {
        Gold += amount;
        tmp?.SetText(Gold.ToString());
        SaveManager.Instance.GameData.Gold += amount;
        SaveManager.Instance.GameDataSave();
        return true;
    }

    public bool RemoveGold(int amount)
    {
        if (amount > Gold) return false;
        else
        {
            Gold -= amount;
            tmp.SetText(Gold.ToString());
            SaveManager.Instance.GameData.Gold -= amount;
            SaveManager.Instance.GameDataSave();
            return true;
        }
    }
}
