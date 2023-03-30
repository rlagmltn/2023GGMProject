using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private ArSOList ArList;
    private Transform map;
    public Enemy[] enemies;
    public Player[] friendly;

    private int enemyCount;
    private int playerCount;

    private void Awake()
    {
        //map = Global.Map;
        //InstantiateMap();
    }

    private void Start()
    {
        enemies = FindObjectsOfType<Enemy>();
        friendly = FindObjectsOfType<Player>();
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
        foreach (Enemy ar in enemies)
        {
            if (!ar.isDead) enemyCount++;
        }
        foreach (Ar ar in friendly)
        {
            if (!ar.isDead) playerCount++;
        }
        CheckGameDone();
    }

    void CheckGameDone()
    {
        if (playerCount == 0)
        {
            StartCoroutine(GameOver());
        }
        else if (enemyCount == 0)
        {
            StartCoroutine(GameClear());
        }
    }

    private IEnumerator GameOver()
    {
        Time.timeScale = 0.3f;
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 1f;
        MGUI.Instance.GameOver();
    }

    private IEnumerator GameClear()
    {
        Time.timeScale = 0.3f;
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1f;
        MGUI.Instance.GameClear();
    }

    private void OnApplicationQuit()
    {
        foreach (ArSO ar in ArList.list)
        {
            ar.isUse = false;
        }
    }
}
