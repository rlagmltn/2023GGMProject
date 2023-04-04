using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditWarrior_StateMove : StateMove
{
    private CONEntity conEntity;
    private Ar ar;
    private Rigidbody2D rigid;
    private Enemy enemy;

    public override void OnAwake()
    {
        conEntity = stateMachineClass.GetComponent<CONEntity>();
        ar = stateMachineClass.GetComponent<Ar>();
        rigid = stateMachineClass.GetComponent<Rigidbody2D>();
        enemy = stateMachineClass.GetComponent<Enemy>();
    }
    
    public override void OnStart()
    {
        Vector2 angle = Vector3.Normalize(stateMachineClass.SearchAr().position - stateMachineClass.transform.position);
        
        stateMachineClass.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        float rangeAngle = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        stateMachineClass.transform.GetChild(0).rotation = Quaternion.Euler(new Vector3(0, 0, rangeAngle));
        stateMachineClass.StartCoroutine("MoveAr", angle * enemy.GetPower());
        
        stateMachineClass.turnFlag = !stateMachineClass.turnFlag;
    
        stateMachine.ChangeState<BanditWarrior_StateIdle>();
    }
}
