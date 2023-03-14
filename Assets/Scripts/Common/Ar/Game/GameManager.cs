using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private Transform map;
    public Ar[] enemies;
    public Ar[] freindly;
    private int enemyCount;
    private int playerCount;

    private void Awake()
    {
        map = Global.Map;
        InstantiateMap();
    }

    private void Start()
    {
        enemies = FindObjectsOfType<Enemy>();
        freindly = FindObjectsOfType<Player>();
        Debug.Log(Global.gMainCamTrm.position);
        Global.gMainCamTrm.position = new Vector3(0, 0, -10);
    }

    void InstantiateMap()
    {
        Instantiate(map);
    }

    public void ArDead()
    {
        enemyCount = 0;
        playerCount = 0;
        foreach (Ar ar in enemies)
        {
            if (!ar.isDead) enemyCount++;
        }
        foreach (Ar ar in freindly)
        {
            if (!ar.isDead) playerCount++;
        }
        CheckGameDone();
    }

    void CheckGameDone()
    {
        if (playerCount == 0)
        {
            GameOver();
        }
        else if (enemyCount == 0)
        {
            GameClear();
        }
    }

    private void GameOver()
    {
        MGUI.Instance.GameOver();

    }

    private void GameClear()
    {
        MGUI.Instance.GameClear();
    }
}
