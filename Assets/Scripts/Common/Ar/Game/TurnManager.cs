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
                        Debug.Log("�÷��̾� �� ����");
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
                        Debug.Log("�� �� ����");
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
        foreach (Turn turn in turns)
        {
            turn.EnableTurn();
        }
        PlayerController.Instance.SetQuickSlotsEnable(true);
        turnCount = 0;
    }

    private IEnumerator PassTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        yield return new WaitForSeconds(1.5f);
        if (isPlayerTurn)
        {
            Debug.Log("�÷��̾� �� ����");
            turnText.SetText("Player Turn");
            ResetTurn();
        }
        else
        {
            turnText.SetText("Enemy Trun");
            Debug.Log("�� �� ����");
            StartCoroutine(ResetEnemyTurn());
            //���⿡ ���� ���� ������ �� �ֵ��� �Ѵ�.

        }
    }

    public bool GetTurn()
    {
        if (isPlayerTurn) return true;
        else return false;
    }

    private IEnumerator ResetEnemyTurn()
    {
        PlayerController.Instance.SetQuickSlotsEnable(false);
        turnCount = 0;
        //��ٸ��� �ð� ����ȭ!
        yield return new WaitForSeconds(0.1f);
        enemys = FindObjectsOfType<ArFSM>();
        enemyTurn = enemys.Length;
        Debug.Log($"enemys: {enemys.Length}");
        for(int i = 0; i < enemys.Length; i++)
        {
            turns[turns.Count-i-1].EnableTurn();
        }

        foreach (ArFSM arFSM in enemys)
        {
            arFSM.StartTurn();
            yield return new WaitForSeconds(5f);

            //yield return new WaitUntil(() => arFSM.GetComponent<Rigidbody2D>().velocity.x + arFSM.GetComponent<Rigidbody2D>().velocity.y <= 0.1f);
        }

        StopCoroutine(ResetEnemyTurn());
    }
}
