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

    private int overloadSp;
    private float overloadWeight;

    private bool pShield;
    private int shieldCnt;

    protected override void Start()
    {
        base.Start();
    }

    public override void StatReset()
    {
        adSp = 0;
        adWeight = 0f;
        pHeavyArmor = false;
        pShield = false;
        pOverload = 0;
        passiveCnt = 4;
        overloadSp = 0;
        overloadWeight = 0f;
        
    }

    public void Passive()
    {
        if(pOverload > 0)
        {
            pOverload--;
            if(pOverload == 0)
            {
                stat.SP = overloadSp;
                stat.WEIGHT = overloadWeight;
                overloadSp = 0;
                overloadWeight = 0f;
            }
        }
        else if(shieldCnt > 0)
        {
            shieldCnt--;
            stat.HP = Mathf.Min(stat.MaxHP, stat.HP + 10);
            stat.ATK += 3;
            pShield = true;
        }
        else
        {
            passiveCnt--;
            if(pHeavyArmor)
            {
                HeavyArmor();
            }
            if(passiveCnt == 0)
            {
                stat.SP += 7;
                adSp += 7;
                stat.WEIGHT += adSp / 5 * 2;
                adWeight += adSp / 5 * 2;
            }
        }
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
        overloadSp = stat.SP;
        overloadWeight = stat.WEIGHT;
        stat.SP = 25;
    }

    public bool CanMove()
    {
        if (pOverload > 0 || shieldCnt > 0) return false;

        return true;
    }

    public void SkillShield()
    {
        overloadSp = stat.SP;
        stat.SP = 10;
        shieldCnt = 1;
    }
}
