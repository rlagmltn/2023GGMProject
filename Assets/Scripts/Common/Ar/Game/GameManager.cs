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
        Finding();   
        Global.gMainCamTrm.position = new Vector3(0, 0, -10);
    }

    public void Finding()
    {
        enemies = FindObjectsOfType<Enemy>();
        friendly = FindObjectsOfType<Player>();
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

    public void GameExit()
    {
        StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        Time.timeScale = 0.3f;
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1f;
        SaveManager.Instance.GameData.IsPlayingGame = false;
        SaveManager.Instance.GameDataSave();
        MGUI.Instance.GameOver();
    }

    private IEnumerator GameClear()
    {
        Time.timeScale = 0.3f;
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1f;
        foreach(Player player in friendly)
        {
            player.TakeAllStat();
        }
        SaveManager.Instance.GameData.ClearStages++;
        SaveManager.Instance.GameDataSave();
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
