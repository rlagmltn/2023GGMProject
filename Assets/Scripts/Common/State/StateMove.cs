using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMove : State<ArFSM>
{
    protected Enemy enemy;
    private AStarGrid grid;

    public override void OnAwake()
    {
        enemy = stateMachineClass.GetComponent<Enemy>();
        grid = stateMachineClass.GetComponent<AStarGrid>();
    }
    
    public override void OnStart()
    {
        Vector2 myPos = stateMachineClass.transform.position;
        Vector2 targetPos = stateMachineClass.SearchAr().position;
        Vector2 angle;

        grid.SetNode(myPos, targetPos);
        List<Vector2> path = grid.GetPath();
        Vector2 curVec = Vector2.zero;
        Vector2 sumVec = Vector2.zero;
        int changeCnt = -1;

        if(path == null)
        {
            Debug.LogError("None Path!");
            stateMachineClass.StartCoroutine("MoveAr", 0);
            stateMachineClass.turnFlag = !stateMachineClass.turnFlag;
            return;
        }

        foreach(Vector2 vec in path)
        {
            if(curVec != vec)
            {
                changeCnt++;
                curVec = vec;
                if (changeCnt == 2) break;

            }
            sumVec += vec;
        }

        angle = sumVec.normalized;
        
        foreach(RaycastHit2D ray in Physics2D.RaycastAll(myPos, angle, sumVec.magnitude))
        {
            if(ray.collider.CompareTag("Out") || ray.collider.name.Contains("Obstacle"))
            {
                Debug.Log($"Find Obstacle in Path : {ray.collider.name}");
                angle = path[0];
                break;  
            }
        }

        stateMachineClass.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        float rangeAngle = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        stateMachineClass.transform.GetChild(0).rotation = Quaternion.Euler(new Vector3(0, 0, rangeAngle));
        stateMachineClass.StartCoroutine("MoveAr", angle * 30f);

        stateMachineClass.turnFlag = !stateMachineClass.turnFlag;
    }

    public override void OnUpdate(float deltaTime)
    {

    }

    public override void OnEnd()
    {
        
    }
}
