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
        //È®·ü ¸¸µé¾îÁà¾ßÇÔ
        player.currentCooltime = player.skillCooltime;
    }
}
