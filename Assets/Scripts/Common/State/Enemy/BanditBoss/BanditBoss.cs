using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditBoss : Enemy
{
    private int adSp;
    private float adWeight;
    private bool pHeavyArmor;
    private int pOverload;
    private int passiveCnt;

    protected override void Start()
    {
        
    }

    public override void StatReset()
    {
        adSp = 0;
        adWeight = 0f;
        pHeavyArmor = false;
        pOverload = 0;
        passiveCnt = 4;
        
    }

    public void Passive()
    {
        passiveCnt--;
        if(passiveCnt == 0)
        {
            stat.SP += 7;
            adSp += 7;
        }
        if(pHeavyArmor)
        {
            HeavyArmor();
        }

        stat.WEIGHT += adSp / 5 * 2;
        adWeight += adSp / 5 * 2;
    }

    public void SkillHeavyArmor()
    {
        pHeavyArmor = true;
        stat.SP -= adSp;
        stat.WEIGHT -= adWeight;
        adSp = 0;
        adWeight = 0f;
    }

    private void HeavyArmor()
    {
        stat.SP += 3;
        adSp += 3;
    }

    public void SkillOverload()
    {
        pOverload = 2;
    }
}
