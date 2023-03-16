using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoSingleton<TurnManager>
{
    [SerializeField] int playerTurn;
    [SerializeField] int enemyTurn;
    [SerializeField] Turn pf_Turn;
    private List<Turn> turns = new List<Turn>();
    private List<ArFSM> enemys = new List<ArFSM>();
    private int turnCount = 0;
    private bool isPlayerTurn = true;

    private void Start()
    {
        for(int i=0; i<playerTurn; i++)
        {
            var turn = Instantiate(pf_Turn, transform);
            turns.Add(turn);
        }

        enemys.CopyTo(FindObjectsOfType<ArFSM>());
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
                        Debug.Log("�÷��̾� �� ����");
                        PassTurn();
                    }
                    return true;
                }
            }
        }
        else
        {
            if (turnCount >= enemyTurn)
            {
                Debug.Log("�� �� ����");
                PassTurn();
            }
            return true;
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
            Debug.Log("�÷��̾� �� ����");
            ResetTurn();
        }
        else
        {
            Debug.Log("�� �� ����");
            ResetEnemyTurn();
            //���⿡ ���� ���� ������ �� �ֵ��� �Ѵ�.

        }
    }

    public bool GetTurn()
    {
        if (isPlayerTurn) return true;
        else return false;
    }

    private void ResetEnemyTurn()
    {
        Debug.Log($"enemys: {enemys.Count}");
        foreach (ArFSM arFSM in enemys)
        {
            arFSM.StartTurn();
        }
    }
}
