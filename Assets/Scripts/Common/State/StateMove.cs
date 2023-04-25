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
        Vector2 myPos = stateMachineClass.transform.position;
        Vector2 targetPos = stateMachineClass.SearchAr().position;
        RaycastHit2D hit = Physics2D.Raycast(myPos, targetPos - myPos, Vector2.Distance(myPos, targetPos) - 1f);

        if(hit.collider == null)
        {
            Vector2 angle = Vector3.Normalize(targetPos - myPos);

            stateMachineClass.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            float rangeAngle = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
            stateMachineClass.transform.GetChild(0).rotation = Quaternion.Euler(new Vector3(0, 0, rangeAngle));
            stateMachineClass.StartCoroutine("MoveAr", angle * enemy.GetPower());
        }
        else
        {
            //A*
        }
        

        stateMachineClass.turnFlag = !stateMachineClass.turnFlag;
    }

    public override void OnUpdate(float deltaTime)
    {

    }

    public override void OnEnd()
    {
        
    }
}
