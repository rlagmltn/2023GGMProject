using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoSingleton<BattleManager>
{
    /*
     * 1. ������ �˵��� ���� �浹����� �ϸ� �ȵǱ� ������ �浹�� �� ���� ������ ��Ʋ�Ŵ����� �޾Ƽ� ��� ����ϰ� ���ʿ� �����Ѵ�.
     * 2. ��ų ������ ���� ���� ���� �ҷ��� �ǰݴ��� ���� ������ ��Ʋ�Ŵ����� �޾Ƽ� ��� ����ϰ� �ǰݴ��� �ʿ� �����Ѵ�.
     * 
     * �������� ����?
     * �ҷ��� ������ ������ ���� 1��
     * ���� ������ ������ ���� 2��
     * �ҷ��� ���� ������ ���� �� �ִ� �Լ�
     * �� ������ �̿��Ͽ� ����ϴ� �Լ�
     * ����� ����� ������� ���� �����鿡�� �����ϰ� ������ �ʱ�ȭ�ϴ� �Լ�
     * 
     * 0�� �Լ�-1 ( ���� ���� )
     * {
     * 1�� �� ������ ����ִٸ� ���⿡ ���� ������ �ִ´�.
     * �׸��� �ҷ��� ������ ������ 2�� �Լ��� �����Ѵ�.
     * �ƴϰ� 2�� �� ������ ����ִٸ� ���⿡ ���� ������ �ִ´�.
     * 
     * 1, 2�� ���� ���� ������ ������ 1�� �Լ��� �����Ѵ�.
     * }
     * 0�� �Լ�-2 ( �ҷ��� ���� )
     * {
     * �ҷ� ������ ����ִٸ� ���⿡ �ҷ��� ������ �ִ´�.
     * �� �� 1�� �˿� ������ ������ 2�� �Լ��� �����Ѵ�.
     * }
     * 
     * 1���� �Լ�
     * {
     * �� ���� BeforeCrush ����
     * ��� �� �� ���� ������ٵ� üũ�ؼ� �����ڿ� ����ڸ� �Ǵ��Ѵ�.
     * �������� BeforeAttack ����
     * ������� BeforeDefence ����
     * �������� ����������(�������� ������ ������� �������� ���� ��� �Ϸ��� ��ġ)�� ����ڿ��� ����
     * �������� AfterAttack ����
     * ������� AfterDefence ����
     * �� ���� AfterCrush ����
     * �������� ������ٵ� ����� ����ڰ� �з��� ���� ���
     * ����ڿ��� ������� ����
     * ���� �ʱ�ȭ
     * }
     * 
     * 2���� �Լ�
     * {
     * �ҷ��� ������������ �ǰ��ڿ��� ����
     * ���� �ʱ�ȭ
     * }
     * 
     * ���� �ʱ�ȭ�ϴ� �Լ�
     * {
     * �ʱ�ȭ�Ѵ�.
     * }
     */
    private Ar arOne, arTwo;
    private Bullet bullet;

    public void SettingAr(Ar ar)
    {
        if (arOne == null)
        {
            arOne = ar;
            if (bullet != null) AttackBattle();
        }
        else if (arTwo == null)
        {
            arTwo = ar;
            if (arOne != null && arTwo != null) CrushBattle();
        }
    }

    //�浹�� �۵��Ǵ� ��Ʋ �ý���(1��)
    private bool CrushBattle()
    {
        arOne.BeforeCrash.Invoke();
        arTwo.BeforeCrash.Invoke();

        Ar attacker = null;
        Ar defender = null;
        if(arOne.rigid.velocity.magnitude>arTwo.rigid.velocity.magnitude)
        {
            attacker = arOne;
            defender = arTwo;
        }
        else
        {
            attacker = arTwo;
            defender = arOne;
        }

        attacker.BeforeAttack.Invoke();
        defender.BeforeDefence.Invoke();

        defender.Hit(attacker.ATK);

        attacker.AfterAttack.Invoke();
        defender.AfterDefence.Invoke();

        Vector2 a, b;
        (a, b) = D2c(attacker.lastVelocity, defender.lastVelocity, attacker.rigid.position, defender.rigid.position);

        Debug.Log(a);
        attacker.Push(a);
        defender.Push(b);

        arOne.AfterCrash.Invoke();
        arTwo.AfterCrash.Invoke();

        ResetAll();
        return true;
    }

    //���ݽ� �۵��Ǵ� ��Ʋ �ý���(2��)
    private bool AttackBattle()
    {
        arOne.Hit(bullet.damage);
        ResetAll();
        return true;
    }

    private void ResetAll()
    {
        arOne = null;
        arTwo = null;
        bullet = null;
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
