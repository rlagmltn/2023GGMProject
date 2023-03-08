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
    public bool CrushBattle()
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

        var pushAtk = new Vector2(0, 0);
        var pushDef = new Vector2(0, 0);
        // 
        // �������
        //
        attacker.Push(pushAtk);
        defender.Push(pushDef);

        arOne.AfterCrash.Invoke();
        arTwo.AfterCrash.Invoke();

        ResetAll();
        return true;
    }

    //���ݽ� �۵��Ǵ� ��Ʋ �ý���(2��)
    public bool AttackBattle()
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
}
