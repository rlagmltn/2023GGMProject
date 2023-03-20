using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditWarrior_StatePassive : State<ArFSM>
{
    private Rigidbody2D rigid;
    private Enemy enemy;

    public override void OnAwake()
    {
        rigid = stateMachineClass.GetComponent<Rigidbody2D>();
        enemy = stateMachineClass.GetComponent<Enemy>();
    }

    public override void OnStart()
    {
        Vector3 dir;
        Collider2D[] cols = Physics2D.OverlapCircleAll(stateMachineClass.transform.position, 1.5f);
        foreach (Collider2D col in cols)
        {
            if (col.CompareTag("Player"))
            {
                Debug.Log("패시브 사용");
                dir = Vector3.Normalize(col.transform.position - stateMachineClass.transform.position);
                col.GetComponent<Rigidbody2D>().velocity = dir * 7f;
            }
        }
        stateMachineClass.StartCoroutine("Move");
    }

    public override void OnUpdate(float deltaTime)
    {

    }

    public override void OnEnd()
    {
        
    }
}
