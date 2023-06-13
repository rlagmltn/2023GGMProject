using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldManager : MonoSingleton<GoldManager>
{
    /*
     * ���
     * �������� Ŭ����� ȹ��
     * ������ ���Žÿ� ����
     * ������ ������ �ʱ�ȭ?
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
        SaveManager.Instance.GameData.Gold = Gold;
    }

    public bool AddGold(int amount)
    {
        Gold += amount;
        tmp?.SetText(Gold.ToString());
        SaveManager.Instance.GameData.Gold = Gold;
        return true;
    }

    public bool RemoveGold(int amount)
    {
        if (amount > Gold) return false;
        else
        {
            Gold -= amount;
            tmp.SetText(Gold.ToString());
            SaveManager.Instance.GameData.Gold = Gold;
            return true;
        }
    }
}
