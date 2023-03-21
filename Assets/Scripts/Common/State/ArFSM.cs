using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArFSM : MonoBehaviour
{
    protected StateMachine<ArFSM> fsmManager;
    public StateMachine<ArFSM> FsmManager => fsmManager;
    protected List<Ar> arList = new List<Ar>();
    [HideInInspector] public bool turnFlag = false;
    
    public virtual Transform SearchAr()
    {
        Ar[] ars = FindObjectsOfType<Player>();
        float distance;
        float minDistance = float.MaxValue;
        int minIndex = -1;
    
        Debug.Log(ars.Length);
    
        for (int i = 0; i < ars.Length; i++)
        {
            distance = Vector2.Distance(transform.position, ars[i].transform.position);
            if(distance < minDistance)
            {
                minDistance = distance;
                minIndex = i;
                
            }
        }

        if (minIndex == -1) return null;
        return ars[minIndex].transform;
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

        //적 재탐색기능 추가 필요
        //모든적이 벽너머에 있으면 어떻게 해야되지
    
        return false;
    }

    public virtual void StartTurn()
    {
        CameraMove.Instance.MovetoTarget(transform);
        Invoke("ChangeTurnFlag", 2f);
    }

    public virtual void ChangeTurnFlag()
    {
        turnFlag = true;
    }

    public virtual bool IsEnded()
    {
        return turnFlag;
    }
}
