using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoSingleton<BattleManager>
{
    /*
     * 1. 각각의 알들이 서로 충돌계산을 하면 안되기 때문에 충돌한 두 알의 정보를 배틀매니저가 받아서 대신 계산하고 양쪽에 전달한다.
     * 2. 스킬 등으로 인한 피해 또한 불렛과 피격당한 알의 정보를 배틀매니저가 받아서 대신 계산하고 피격당한 쪽에 전달한다.
     * 
     * 만들어야할 것은?
     * 불렛의 정보를 저장할 변수 1개
     * 알의 정보를 저장할 변수 2개
     * 불렛과 알의 정보를 받을 수 있는 함수
     * 두 정보를 이용하여 계산하는 함수
     * 계산한 결과를 비어있지 않은 변수들에게 전달하고 변수를 초기화하는 함수
     * 
     * 0번 함수-1 ( 알의 정보 )
     * {
     * 1번 알 변수가 비어있다면 여기에 알의 정보를 넣는다.
     * 그리고 불렛에 정보가 있으면 2번 함수를 실행한다.
     * 아니고 2번 알 변수가 비어있다면 여기에 알의 정보를 넣는다.
     * 
     * 1, 2번 알이 전부 정보가 있으면 1번 함수를 실행한다.
     * }
     * 0번 함수-2 ( 불렛의 정보 )
     * {
     * 불렛 변수가 비어있다면 여기에 불렛의 정보를 넣는다.
     * 그 후 1번 알에 정보가 있으면 2번 함수를 실행한다.
     * }
     * 
     * 1번의 함수
     * {
     * 양 쪽의 BeforeCrush 실행
     * 계산 전 양 쪽의 리지드바디를 체크해서 공격자와 방어자를 판단한다.
     * 공격자의 BeforeAttack 실행
     * 방어자의 BeforeDefence 실행
     * 공격자의 최종데미지(공격자의 버프와 방어자의 버프등을 전부 계산 완료한 수치)를 방어자에게 전달
     * 공격자의 AfterAttack 실행
     * 방어자의 AfterDefence 실행
     * 양 쪽의 AfterCrush 실행
     * 공격자의 리지드바디를 계산해 방어자가 밀려날 값을 계산
     * 방어자에게 계산결과를 전달
     * 변수 초기화
     * }
     * 
     * 2번의 함수
     * {
     * 불렛의 최종데미지를 피격자에게 전달
     * 변수 초기화
     * }
     * 
     * 변수 초기화하는 함수
     * {
     * 초기화한다.
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

    //충돌시 작동되는 배틀 시스템(1번)
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

    //공격시 작동되는 배틀 시스템(2번)
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
