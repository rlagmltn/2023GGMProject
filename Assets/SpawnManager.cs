using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    public Dictionary<ePrefabs, List<CONEntity>> _SpawnObjDic;

    [SerializeField] BattleMapHolder battleMapHolder;
    public BattleMapSO battleMapSO;

    [SerializeField] private Transform[] summonTrs;

    void Awake()
    {
        _SpawnObjDic = new Dictionary<ePrefabs, List<CONEntity>>();
        battleMapSO = battleMapHolder.map;

        Summon();

        //_SpawnObjDic[ePrefabs.EnemyObj1][0].SetActive(true);
    }

    private void Summon()
    {
        var map = Instantiate(battleMapSO.Map);
        summonTrs = battleMapSO.Map.transform.GetChild(2).GetComponentsInChildren<Transform>();
        
        map.transform.position = new Vector3(0, 0, 0);

        int enemycount = 0;

        foreach (EnemyCount enemy in battleMapSO.Enemies)
        {
            enemycount += enemy.Count;
        }

        var numbers = GenerateRandomNumbers(summonTrs.Length, enemycount);
        enemycount = 0;
        foreach (EnemyCount enemy in battleMapSO.Enemies)
        {
            for (int i=0; i < enemy.Count; i++)
            {
                var newEnemy = Instantiate(enemy.Enemy);
                newEnemy.transform.position = summonTrs[numbers[i+enemycount]].position;
            }
            enemycount += enemy.Count;
        }
    }

    private int[] GenerateRandomNumbers(int a, int b)
    {
        List<int> numbers = new List<int>();

        for (int i = 1; i < a; i++)
        {
            numbers.Add(i);
        }

        int[] randomNumbers = new int[b];

        for (int i = 0; i < b; i++)
        {
            int index = Random.Range(0, numbers.Count);
            randomNumbers[i] = numbers[index];
            numbers.RemoveAt(index);
        }

        return randomNumbers;
    }
}