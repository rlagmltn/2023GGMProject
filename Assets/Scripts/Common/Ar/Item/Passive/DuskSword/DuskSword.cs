using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuskSword : ItemInfo
{
    [SerializeField] private int Percent;

    public override void Passive()
    {
        DuskSword_Passive();
    }

    void DuskSword_Passive()
    {
        int tempNum = Random.Range(1, 101);
        if (Percent > tempNum) return;
        //Ȯ�� ����������
        player.currentCooltime = player.skillCooltime;
    }
}
