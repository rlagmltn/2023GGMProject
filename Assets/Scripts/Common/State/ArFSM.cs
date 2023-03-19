using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArFSM : MonoBehaviour
{
    protected StateMachine<ArFSM> fsmManager;
    public StateMachine<ArFSM> FsmManager => fsmManager;
    private List<Ar> arList = new List<Ar>();
    [HideInInspector] public bool turnFlag = false;

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

    public void StartTurn()
    {
        Debug.Log("StartEnemy");
        CameraMove.Instance.MovetoTarget(GetComponent<Enemy>());
        Invoke("ChangeTurnFlag", 2f);
    }

    private void ChangeTurnFlag()
    {
        turnFlag = true;
    }

    public bool IsEnded()
    {
        return turnFlag;
    }

    private void PassiveSkill()
    {
        Vector3 dir = SearchAr().position - transform.position;
        Collider2D[] cols = Physics2D.OverlapBoxAll(transform.position, new Vector2(10f, 10f), 0);
        foreach (Collider2D col in cols)
        {
            Debug.Log(col.transform.position);
            if (col.CompareTag("Player"))
            {
                col.GetComponent<Rigidbody2D>().AddForce(dir * 5f);
                fsmManager.ChangeState<StateAtk>();
            }
        }
    }
}
