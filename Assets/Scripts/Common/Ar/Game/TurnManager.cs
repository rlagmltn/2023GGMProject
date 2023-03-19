using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TurnManager : MonoSingleton<TurnManager>
{
    [SerializeField] int playerTurn;
    [SerializeField] int enemyTurn;
    [SerializeField] Turn pf_Turn;
    private List<Turn> turns = new List<Turn>();
    private ArFSM[] enemys;
    private int turnCount = 0;
    private bool isPlayerTurn = true;

    private void Start()
    {
        for(int i=0; i<playerTurn; i++)
        {
            var turn = Instantiate(pf_Turn, transform);
            turns.Add(turn);
        }

        
    }

    public bool UseTurn()
    {
        if (isPlayerTurn)
        {
            foreach (Turn turn in turns)
            {
                if (turn.active)
                {
                    turn.DisableTurn();
                    turnCount++;
                    
                    if (turnCount >= playerTurn)
                    {
                        Debug.Log("플레이어 턴 종료");
                        PassTurn();
                    }
                    return true;
                }
            }
        }
        else
        {
            foreach(Turn turn in turns)
            {
                if(turn.active)
                {
                    turn.DisableTurn();
                    turnCount++;

                    if (turnCount >= enemyTurn)
                    {
                        Debug.Log("적 턴 종료");
                        PassTurn();
                    }
                    return true;
                }
            }
            
        }
        return false;
    }

    public void ResetTurn()
    {
        foreach (Turn turn in turns)
        {
            turn.EnableTurn();
        }
        turnCount = 0;
    }

    private void PassTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        if(isPlayerTurn)
        {
            Debug.Log("플레이어 턴 시작");
            ResetTurn();
        }
        else
        {
            Debug.Log("적 턴 시작");
            StartCoroutine(ResetEnemyTurn());
            //여기에 적이 턴을 진행할 수 있도록 한다.

        }
    }

    public bool GetTurn()
    {
        if (isPlayerTurn) return true;
        else return false;
    }

    private IEnumerator ResetEnemyTurn()
    {
        //for (int i = 0; i < playerTurn; i++)
        //{
        //    var turn = Instantiate(pf_Turn, transform);
        //    turns.Add(turn);
        //}
        foreach (Turn turn in turns)
        {
            turn.EnableTurn();
        }
        turnCount = 0;
        enemys = FindObjectsOfType<ArFSM>();

        Debug.Log($"enemys: {enemys.Length}");
        foreach (ArFSM arFSM in enemys)
        {
            arFSM.StartTurn();
            yield return new WaitForSeconds(5f);

            //yield return new WaitUntil(() => arFSM.GetComponent<Rigidbody2D>().velocity.x + arFSM.GetComponent<Rigidbody2D>().velocity.y <= 0.1f);
        }

        StopCoroutine(ResetEnemyTurn());
    }
}
