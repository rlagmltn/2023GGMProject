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
    [SerializeField] GameObject turnObj;
    [SerializeField] TextMeshProUGUI turnText;

    private List<Turn> turns = new List<Turn>();
    private ArFSM[] enemys;
    private int turnCount = 0;
    private bool isPlayerTurn = true;
    private CameraMove cameraMove;
    private PlayerController playerController;
    public bool IsPlayerTurn { get { return isPlayerTurn; } }
    public bool IsWaitingTurn { get; private set; }
    public bool SomeoneIsMoving { get; set; }

    private void Start()
    {
        cameraMove = FindObjectOfType<CameraMove>();
        playerController = FindObjectOfType<PlayerController>();

        for(int i=0; i<10; i++)
        {
            var turn = Instantiate(pf_Turn, transform);
            turns.Add(turn);
        }
        StartCoroutine(ResetTurn_C());
        turnObj?.SetActive(false);
    }

    public bool UseTurn()
    {
        StopBlink();
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
                        //플레이어 턴 종료
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
                        //적 턴 종료
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
        turnText?.SetText("Enemy Turn");
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
        playerController.SetQuickSlotsEnable(!isPlayerTurn);
        IsWaitingTurn = true;
        if(turnObj!=null) StartCoroutine(ActiveTurnPanel());
        yield return new WaitForSeconds(5f);
        IsWaitingTurn = false;
        isPlayerTurn = !isPlayerTurn;
        ActiveAllTurn();
        if (isPlayerTurn)
        {
            //플레이어 턴 시작
            ResetTurn();
        }
        else
        {
            //적 턴 시작
            StartCoroutine(ResetEnemyTurn());
        }
    }

    public bool GetTurn()
    {
        if (isPlayerTurn) return true;
        else return false;
    }

    private IEnumerator ResetEnemyTurn()
    {
        turnText?.SetText("Player Turn");
        turnCount = 0;

        enemys = FindObjectsOfType<ArFSM>();
        enemyTurn = enemys.Length;
        Debug.Log($"enemys: {enemys.Length}");

        UnActiveNotUseTurn(enemyTurn);
        //기다리는 시간 변수화!
        yield return new WaitForSeconds(0.5f);

        for(int i = 0; i< enemyTurn; i++)
        {
            turns[turns.Count - i - 1].EnableEnemyTurn();
        }

        cameraMove.SetDefaultZoom();

        foreach (ArFSM arFSM in enemys)
        {
            while(SomeoneIsMoving)
            {
                yield return new WaitForSeconds(1f);
            }
            if (arFSM.gameObject.activeSelf)
            {
                BlinkNextTurn();
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

    public void BlinkNextTurn()
    {
        foreach (Turn turn in turns)
        {
            if (turn.active)
            {
                turn.Blink();
                break;
            }
        }
    }

    public void StopBlink()
    {
        foreach (Turn turn in turns)
        {
            if (turn.active)
            {
                turn.StopBlink();
                break;
            }
        }
    }

    private IEnumerator ActiveTurnPanel()
    {
        yield return new WaitForSeconds(3f);
        turnObj.SetActive(true);
        turnObj.transform.DORotate(Vector3.zero, 0.5f);
        yield return new WaitForSeconds(1.5f);
        turnObj.transform.DORotate(new Vector3(0, 90, 0), 0.5f);
        yield return new WaitForSeconds(0.5f);
        turnObj.SetActive(false);
    }
}
