using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoSingleton<BattleManager>
{
    private Ar arOne, arTwo;
    private Ar arAtk;
    private int damage;

    public void SettingAr(Ar ar)
    {
        if (arOne == null)
        {
            arOne = ar;
        }
        else if (arTwo == null)
        {
            arTwo = ar;
            if (arOne != null && arTwo != null)
            {
                if(arOne.tag == arTwo.tag)
                {
                    Crush();
                }
                else
                {
                    CrushBattle();
                }

            }
        }
    }

    public void SettingAr(Ar ar, Ar attacker)
    {
        arOne = ar;
        arAtk = attacker;
        if (arAtk != null && arOne != null) AttackBattle();
        else ResetAll();
    }

    public void SettingAr(Ar ar, int damage)
    {
        arOne = ar;
        this.damage = damage;
        if (arOne != null) BulletDamage();
        else ResetAll();
    }

    //충돌시 작동되는 배틀 시스템
    private bool CrushBattle()
    {
        arOne.BeforeCrash.Invoke();
        arTwo.BeforeCrash.Invoke();

        Ar attacker = null;
        Ar defender = null;
        if(arOne.isMove)
        {
            attacker = arOne;
            defender = arTwo;
        }
        else if(arTwo.isMove)
        {
            attacker = arTwo;
            defender = arOne;
        }
        else if(TurnManager.Instance.IsPlayerTurn)
        {
            if(arOne.CompareTag("Player"))
            {
                attacker = arOne;
                defender = arTwo;
            }
            else
            {
                attacker = arTwo;
                defender = arOne;
            }
        }
        else if(!TurnManager.Instance.IsPlayerTurn)
        {
            if(arOne.CompareTag("Enemy"))
            {
                attacker = arOne;
                defender = arTwo;
            }
            else
            {
                attacker = arTwo;
                defender = arOne;
            }
        }

        attacker.BeforeAttack.Invoke();
        defender.BeforeDefence.Invoke();

        attacker.AnimAttackStart();

        var criChance = Random.Range(1, 101);
        Debug.Log(criChance);
        bool isDead;
        if(criChance<attacker.stat.CriPer)
            isDead = defender.Hit(attacker.stat.ATK * attacker.stat.CriPer);
        else
            isDead = defender.Hit(attacker.stat.ATK);

        attacker.AfterAttack.Invoke();
        if(!isDead)
        {
            defender.AfterDefence.Invoke();

            Vector2 a, b;
            (a, b) = D2c(attacker.lastVelocity, defender.lastVelocity, attacker.rigid.position, defender.rigid.position, 1+attacker.stat.WEIGHT*0.1f, 1+defender.stat.WEIGHT*0.1f);

            attacker.Push(a);
            defender.Push(b);

            defender.AfterCrash.Invoke();
        }
        attacker.AfterCrash.Invoke();

        ResetAll();
        return true;
    }

    //같은 팀 끼리 충돌 시 작동되는 시스템
    private void Crush()
    {
        if (arOne.isUsingSkill && arOne.stat.SATK < 0)
        {
            arTwo.Hit(arOne.stat.SATK);
            arOne.isUsingSkill = false;
        }
        if (arTwo.isUsingSkill && arTwo.stat.SATK < 0)
        {
            arOne.Hit(arTwo.stat.SATK);
            arTwo.isUsingSkill = false;
        }

        Vector2 a, b;
        (a, b) = D2c(arOne.lastVelocity, arTwo.lastVelocity, arOne.rigid.position, arTwo.rigid.position, 1+arOne.stat.WEIGHT*0.1f, 1+arTwo.stat.WEIGHT*0.1f);
        arOne.Push(a);
        arTwo.Push(b);

        ResetAll();
    }

    //스킬 공격시 작동되는 배틀 시스템
    private bool AttackBattle()
    {
        arAtk.BeforeAttack?.Invoke();
        arOne.BeforeDefence?.Invoke();

        var isdead = arOne.Hit(arAtk.stat.SATK);

        if (!isdead) arOne.AfterDefence?.Invoke();
        arAtk.AfterAttack?.Invoke();

        ResetAll();
        return true;
    }

    private bool BulletDamage()
    {
        var isdead = arOne.Hit(damage);

        ResetAll();

        if (isdead) return false;
        else return true;
    }

    private void ResetAll()
    {
        arOne = null;
        arTwo = null;
        arAtk = null;
        damage = 0;
    }

    private (Vector2, Vector2) D2c(Vector2 v1, Vector2 v2, Vector2 c1, Vector2 c2, float w1, float w2,float e = 1)
    {
        Vector2 basisX = (c2 - c1).normalized;
        Vector2 basisY = Vector2.Perpendicular(basisX);

        float sin1, sin2, cos1, cos2;

        if(v1.magnitude==0)
        {
            sin1 = 0;
            cos1 = 1;
        }
        else
        {
            cos1 = Vector2.Dot(v1, basisX)/v1.magnitude;
            Vector3 cross = Vector3.Cross(v1, basisX);
            if (cross.z > 0)
            {
                sin1 = cross.magnitude / v1.magnitude;
            }
            else
            {
                sin1 = -cross.magnitude / v1.magnitude;
            }
        }

        if (v2.magnitude == 0)
        {
            sin2 = 0;
            cos2 = 1;
        }
        else
        {
            cos2 = Vector2.Dot(v2, basisX) / v2.magnitude;
            Vector3 cross = Vector3.Cross(v2, basisX);
            if (cross.z > 0)
            {
                sin2 = cross.magnitude / v2.magnitude;
            }
            else
            {
                sin2 = -cross.magnitude / v2.magnitude;
            }
        }

        Vector2 u1, u2;
        u1 = ((w1 - e * w2) / (w1 + w2) * v1.magnitude * cos1 + (w2 + e * w2) / (w1 + w2) * v2.magnitude * cos2) / 2 * basisX - v1.magnitude * sin1 * basisY;
        u2 = ((w1 + e * w1) / (w1 + w2) * v1.magnitude * cos1 + (w2 - e * w1) / (w1 + w2) * v2.magnitude * cos2) / 2 * basisX - v2.magnitude * sin2 * basisY;

        return (u1, u2);
    }
}
