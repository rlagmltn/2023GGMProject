using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArFSM : MonoBehaviour
{
    protected StateMachine<ArFSM> fsmManager;
    public StateMachine<ArFSM> FsmManager => fsmManager;


    private void Start()
    {
        fsmManager = new StateMachine<ArFSM>(this, new StateIdle());
        fsmManager.AddStateList(new StateMove());
        fsmManager.AddStateList(new StateAtk());
    }

    public virtual Transform SearchAr()
    {
        

        return transform;
    }

}
