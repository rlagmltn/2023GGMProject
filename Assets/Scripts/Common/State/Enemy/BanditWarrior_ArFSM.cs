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
        fsmManager.AddStateList(new BanditWarrior_StateSkill());
    }

    private void Update()
    {
        fsmManager.Update(Time.deltaTime);
    }

    private IEnumerator Move()
    {
        yield return new WaitForSeconds(1f);
        transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        fsmManager.ChangeState<BanditWarrior_StateMove>();
        StopCoroutine("Move");
    }

}
