using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TurnManager : MonoSingleton<TurnManager>
{
    [SerializeField] int playerTurn;
    [SerializeField] int enemyTurn;
    [SerializeField] Turn pf_Turn;
    [SerializeField] TextMeshProUGUI turnText;

    private List<Turn> turns = new List<Turn>();
    private ArFSM[] enemys;
    private int turnCount = 0;
    private bool isPlayerTurn = true;
    public bool IsPlayerTurn { get { return isPlayerTurn; } }
    public bool SomeoneIsMoving { get; set; }

    private void Start()
    {
        for(int i=0; i<10; i++)
        {
            var turn = Instantiate(pf_Turn, transform);
            turns.Add(turn);
        }
        StartCoroutine(ResetTurn_C());
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
                        StartCoroutine(PassTurn());
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
                        StartCoroutine(PassTurn());
                    }
                    return true;
                }
            }
            
        }
        return false;
    }

    public void ResetTurn()
    {
        turnText.SetText("Player Turn");
        UnActiveNotUseTurn(playerTurn);

        for (int i = 0; i < playerTurn; i++)
        {
            turns[turns.Count - i - 1].EnableTurn();
        }

        turnCount = 0;
    }

    private IEnumerator ResetTurn_C()
    {
        yield return null;
        ResetTurn();
    }

    private IEnumerator PassTurn()
    {
        PlayerController.Instance.SetQuickSlotsEnable(!isPlayerTurn);
        isPlayerTurn = !isPlayerTurn;
        yield return new WaitForSeconds(3f);
        ActiveAllTurn();
        if (isPlayerTurn)
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
        turnText.SetText("Enemy Turn");
        turnCount = 0;

        enemys = FindObjectsOfType<ArFSM>();
        enemyTurn = enemys.Length;
        Debug.Log($"enemys: {enemys.Length}");

        UnActiveNotUseTurn(enemyTurn);
        //기다리는 시간 변수화!
        yield return new WaitForSeconds(0.1f);

        for(int i = 0; i< enemyTurn; i++)
        {
            turns[turns.Count - i - 1].EnableTurn();
        }

        foreach (ArFSM arFSM in enemys)
        {
            while(SomeoneIsMoving)
            {
                yield return new WaitForSeconds(1f);
            }
            if (arFSM.gameObject.activeSelf)
            {
                arFSM.StartTurn();
                yield return new WaitForSeconds(6f);
            }
            else
            {
                UseTurn();
            }

            //yield return new WaitUntil(() => arFSM.GetComponent<Rigidbody2D>().velocity.x + arFSM.GetComponent<Rigidbody2D>().velocity.y <= 0.1f);
        }

        StopCoroutine(ResetEnemyTurn());
    }

    private void ActiveAllTurn()
    {
        foreach(Turn turn in turns)
        {
            turn.SetActiveTurnObj(true);
        }
    }

    private void UnActiveNotUseTurn(int count)
    {
        for (int i = 0; i < turns.Count - count; i++)
        {
            turns[i].SetActiveTurnObj(false);
        }
    }
}
