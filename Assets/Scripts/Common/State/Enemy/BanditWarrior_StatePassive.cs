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
        Collider2D[] cols = Physics2D.OverlapCircleAll(stateMachineClass.transform.position, 3f);
        Debug.Log("패시브 사용");
        stateMachineClass.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        foreach (Collider2D col in cols)
        {
            if (col.CompareTag("Player"))
            {
                dir = Vector3.Normalize(col.transform.position - stateMachineClass.transform.position);
                col.GetComponent<Rigidbody2D>().velocity = dir * 10f;

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
