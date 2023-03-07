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
    }

    private void Update()
    {
        fsmManager.Update(Time.deltaTime);
    }

    public virtual Transform SearchAr()
    {
        Ar[] ars = FindObjectsOfType<Player>();
        float distance;
        float minDistance = float.MaxValue;
        int minIndex = 0;
        Vector2 v = Vector2.zero;

        for (int i = 0; i < ars.Length; i++)
        {
            distance = Vector2.Distance(transform.position, ars[i].transform.position);
            if(distance < minDistance)
            {
                minDistance = distance;
                minIndex = i;
            }
        }

        return ars[minIndex].transform;
    }

    public virtual void MoveToTarget()
    {
        Vector3 dir = SearchAr().position - transform.position;
        transform.position += dir * 1f * Time.deltaTime;

    }

    public virtual void SelectTurn()
    {
        


    }

}
