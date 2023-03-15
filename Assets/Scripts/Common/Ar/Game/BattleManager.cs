using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoSingleton<BattleManager>
{
    private Ar arOne, arTwo;
    private Ar arAtk;

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

    //�浹�� �۵��Ǵ� ��Ʋ �ý���
    private bool CrushBattle()
    {
        arOne.BeforeCrash.Invoke();
        arTwo.BeforeCrash.Invoke();

        Ar attacker = null;
        Ar defender = null;
        if(arOne.lastVelocity.magnitude>arTwo.lastVelocity.magnitude)
        {
            attacker = arOne;
            defender = arTwo;
            Debug.Log("Attacker = " + arOne.name + arOne.stat.ATK);
        }
        else
        {
            attacker = arTwo;
            defender = arOne;
            Debug.Log("Attacker = " + arTwo.name + arTwo.stat.ATK);
        }

        attacker.BeforeAttack.Invoke();
        defender.BeforeDefence.Invoke();

        Debug.Log(attacker.stat.ATK);
        var isdead = defender.Hit(attacker.stat.ATK);

        attacker.AfterAttack.Invoke();
        if(!isdead)
        {
            defender.AfterDefence.Invoke();

            Vector2 a, b;
            (a, b) = D2c(attacker.lastVelocity, defender.lastVelocity, attacker.rigid.position, defender.rigid.position);

            Debug.Log(a);
            attacker.Push(a);
            defender.Push(b);

            defender.AfterCrash.Invoke();
        }
        attacker.AfterCrash.Invoke();

        ResetAll();
        return true;
    }

    //���� �� ���� �浹 �� �۵��Ǵ� �ý���
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
        (a, b) = D2c(arOne.lastVelocity, arTwo.lastVelocity, arOne.rigid.position, arTwo.rigid.position);
        arOne.Push(a);
        arTwo.Push(b);

        ResetAll();
    }

    //��ų ���ݽ� �۵��Ǵ� ��Ʋ �ý���
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

    private void ResetAll()
    {
        arOne = null;
        arTwo = null;
        arAtk = null;
    }

    private (Vector2, Vector2) D2c(Vector2 v1, Vector2 v2, Vector2 c1, Vector2 c2, float e = 1)
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
        u1 = ((1-e)*v1.magnitude*cos1+(1+e)*v2.magnitude*cos2)/ 2 * basisX - v1.magnitude * sin1 * basisY;
        u2 = ((1+e)*v1.magnitude*cos1+(1-e)*v2.magnitude*cos2)/ 2 * basisX - v2.magnitude * sin2 * basisY;

        return (u1, u2);
    }
}