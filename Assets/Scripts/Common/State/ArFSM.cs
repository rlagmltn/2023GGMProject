using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArFSM : MonoBehaviour
{
    protected StateMachine<ArFSM> fsmManager;
    public StateMachine<ArFSM> FsmManager => fsmManager;
    private List<Ar> arList = new List<Ar>();


    private void Start()
    {
        fsmManager = new StateMachine<ArFSM>(this, new StateIdle());
        fsmManager.AddStateList(new StateMove());
    }

    private void Update()
    {
        fsmManager.Update(Time.deltaTime);
        Ray2D ray = new Ray2D(transform.position, Vector2.up);
        
    }
    
    public virtual Transform SearchAr()
    {
        Ar[] ars = FindObjectsOfType<Player>();
        float distance;
        float minDistance = float.MaxValue;
        int minIndex = 0;
    
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
    
    public virtual void ManageTurn()
    {
        for(int i = 0; i < arList.Count; i++)
        {
            //각각의 행동들이 끝나면
            if(true)
            {
                //적 턴종료
                TurnManager.Instance.UseTurn();
    
            }
        }
    }

    public virtual bool CheckWall()
    {
        Vector3 dir = SearchAr().position - transform.position;
        float distance = Vector2.Distance(transform.position, SearchAr().position);
        RaycastHit2D[] hits = Physics2D.RaycastAll(gameObject.transform.position, dir, distance);
        foreach(RaycastHit2D hit in hits)
        {
            if (hit.transform.gameObject.CompareTag("Object"))
            {
                return true;
            }
        }

        return false;
    }
}
