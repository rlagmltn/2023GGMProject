using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMove : State<ArFSM>
{
    protected Enemy enemy;

    public override void OnAwake()
    {
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
    }

    public override void OnUpdate(float deltaTime)
    {

    }

    public override void OnEnd()
    {
        
    }
}
