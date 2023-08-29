using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditArcher_StateAtk : StateAtk
{
    private BanditArcher archer;
    private int reloadCnt = 3;

    public override void OnAwake()
    {
        archer = stateMachineClass.GetComponent<BanditArcher>();
    }

    public override void OnStart()
    {
        if(reloadCnt == 0)
        {
            //reloading
            reloadCnt = 3;
        }
        else
        {
            Vector2 angle = Vector3.Normalize(stateMachineClass.SearchAr().position - stateMachineClass.transform.position);
            archer.ShootArrow(angle, reloadCnt == 1);
            reloadCnt--;
        }
        stateMachineClass.enemy.isMove = true;
        TurnManager.Instance.UseTurn();
        stateMachine.ChangeState<BanditArcher_StateIdle>();
    }
}
