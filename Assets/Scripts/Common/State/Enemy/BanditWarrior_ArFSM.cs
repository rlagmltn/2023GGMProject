using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditWarrior_ArFSM : ArFSM
{
    private void Awake()
    {
        fsmManager = new StateMachine<ArFSM>(this, new BanditWarrior_StateIdle());
    }

    private void Start()
    {
        fsmManager.AddStateList(new BanditWarrior_StateMove());
        fsmManager.AddStateList(new BanditWarrior_StatePassive());
    }

    private void Update()
    {
        fsmManager.Update(Time.deltaTime);
    }

    private IEnumerator Move()
    {
        yield return new WaitForSeconds(1.5f);
        fsmManager.ChangeState<BanditWarrior_StateMove>();
        StopCoroutine("Move");
    }

}
