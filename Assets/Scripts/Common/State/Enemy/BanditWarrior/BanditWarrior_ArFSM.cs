using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditWarrior_ArFSM : ArFSM
{
    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        fsmManager = new StateMachine<ArFSM>(this, new BanditWarrior_StateIdle());
    }

    protected override void Start()
    {
        fsmManager.AddStateList(new BanditWarrior_StateMove());
        fsmManager.AddStateList(new BanditWarrior_StatePassive());
        fsmManager.AddStateList(new BanditWarrior_StateSkill());
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    private IEnumerator Move()
    {
        yield return new WaitForSeconds(1f);
        transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        fsmManager.ChangeState<BanditWarrior_StateSkill>();
    }

    private IEnumerator TriggerOff()
    {
        yield return new WaitForSeconds(2f);
        col.isTrigger = false;
    }

}
