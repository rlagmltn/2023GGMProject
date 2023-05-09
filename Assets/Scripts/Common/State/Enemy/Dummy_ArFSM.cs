using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy_ArFSM : ArFSM
{
    private void Awake()
    {
        fsmManager = new StateMachine<ArFSM>(this, new Dummy_StateIdle());
    }
}
