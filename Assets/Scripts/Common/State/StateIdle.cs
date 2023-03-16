using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : State<ArFSM>
{

    public override void OnAwake()
    {

    }

    public override void OnStart()
    {
        
    }
 
     public override void OnUpdate(float deltaTime)
     {
         //�ڱ����̸�
         if(!TurnManager.Instance.GetTurn() && stateMachineClass.turnFlag)
         {
             Debug.Log("����");
             Transform target = stateMachineClass.SearchAr();
             if(target)
             {
                stateMachineClass.turnFlag = !stateMachineClass.turnFlag;
                stateMachine.ChangeState<StateMove>();
             }
         }
  
         if(Input.GetKeyDown(KeyCode.Space))
         {
             stateMachineClass.turnFlag = true;
         }
    }

    public override void OnEnd()
    {

    }
}
