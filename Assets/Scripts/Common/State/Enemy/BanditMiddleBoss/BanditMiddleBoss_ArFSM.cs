using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditMiddleBoss_ArFSM : ArFSM
{
    private void Awake()
    {
        fsmManager = new StateMachine<ArFSM>(this, new BanditMiddleBoss_StateIdle());
    }

    protected override void Start()
    {
        base.Start();
        fsmManager.AddStateList(new BanditMiddleBoss_StateMove());
    }
}
