using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditArcher_ArFSM : ArFSM
{
    private void Awake()
    {
        fsmManager = new StateMachine<ArFSM>(this, new BanditArcher_StateIdle());
    }

    protected override void Start()
    {
        base.Start();
        fsmManager.AddStateList(new BanditArcher_StateMove());
        fsmManager.AddStateList(new BanditArcher_StatePassive());
        fsmManager.AddStateList(new BanditArcher_StateAtk());
        fsmManager.AddStateList(new BanditArcher_StateSkill());
    }

    public override void Update()
    {
        base.Update();
    }
}
