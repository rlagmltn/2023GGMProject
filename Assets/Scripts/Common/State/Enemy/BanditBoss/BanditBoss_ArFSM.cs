using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditBoss_ArFSM : ArFSM
{
    private void Awake()
    {
        fsmManager = new StateMachine<ArFSM>(this, new BanditBoss_StateIdle());
    }

    protected override void Start()
    {
        base.Start();
        fsmManager.AddStateList(new BanditBoss_StateMove());
    }
}
