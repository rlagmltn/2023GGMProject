using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoSingleton<GoldManager>
{
    /*
     * ���
     * �������� Ŭ����� ȹ��
     * ������ ���Žÿ� ����
     * ������ ������ �ʱ�ȭ?
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
