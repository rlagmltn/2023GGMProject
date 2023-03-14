using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArFSM : MonoBehaviour
{
    protected StateMachine<ArFSM> fsmManager;
    public StateMachine<ArFSM> FsmManager => fsmManager;
    private List<Ar> arList = new List<Ar>();


    private void Awake()
    {
        fsmManager = new StateMachine<ArFSM>(this, new StateIdle());
    }

    private void Start()
    {
        
        fsmManager.AddStateList(new StateMove());
        fsmManager.AddStateList(new StateAtk());
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
    
    public virtual void MoveToTarget()
    {
        Vector3 dir = SearchAr().position - transform.position;
        transform.position += dir * 1f * Time.deltaTime;
    }
    
    public virtual void ManageTurn()
    {
        for(int i = 0; i < arList.Count; i++)
        {
            //������ �ൿ���� ������
            if(true)
            {
                //�� ������
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
    
        //�� ��Ž����� �߰� �ʿ�
        //������� ���ʸӿ� ������ ��� �ؾߵ���
    
    
    
        return false;
    }
}
