using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArFSM : MonoBehaviour
{
    protected StateMachine<ArFSM> fsmManager;
    public StateMachine<ArFSM> FsmManager => fsmManager;
    
    protected List<Ar> arList = new List<Ar>();
    [HideInInspector] public bool turnFlag = false;
    [HideInInspector] public Enemy enemy;
    protected CameraMove cameraMove;

    protected bool turnSkip = false;

    protected virtual void Start()
    {
        enemy = GetComponent<Enemy>();
        cameraMove = FindObjectOfType<CameraMove>();
    }

    public virtual void Update()
    {
        fsmManager.Update(Time.deltaTime);
    }

    public virtual Transform SearchAr()
    {
        Ar[] ars = FindObjectsOfType<Player>();
        float distance;
        float minDistance = float.MaxValue;
        int minIndex = -1;
    
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
    
        return false;
    }

    public virtual void StartTurn()
    {
        cameraMove.MovetoTarget(transform);
        GetComponent<Enemy>().PassiveCoolDown();
        if(turnSkip)
        {
            Invoke("TurnSkip", 1.5f);
        }
        else
        {
            Invoke("ChangeTurnFlag", 1.5f);
        }
    }

    public virtual void ChangeTurnFlag()
    {
        turnFlag = true;
    }

    public virtual bool IsEnded()
    {
        return turnFlag;
    }

    public IEnumerator MoveAr(Vector2 vel)
    {
        yield return new WaitForSeconds(1f);

        float pushPower = GetComponent<Enemy>().pushPower;
        float weight = GetComponent<Enemy>().stat.WEIGHT;
        TurnManager.Instance.UseTurn();
        GetComponent<Rigidbody2D>().velocity = ((vel.normalized) * pushPower) / (1 + weight * 0.1f);
        Debug.Log(vel.normalized * pushPower / (1 + weight * 0.1f));
        transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        enemy.isMove = true;
        
    }

    public void SetTurnSkip()
    {
        turnSkip = true;
    }

    private void TurnSkip()
    {
        turnSkip = false;
        enemy.isMove = true;
        TurnManager.Instance.UseTurn();
    }
}
