using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private Transform EnemyPrefab;
    [SerializeField] private List<Transform> SpawnPos;

    private int EnemyCount;

    private List<int> NumList;

    public void SetEnemyNum(int num)
    {
        EnemyCount = num;
        RandomSpawn();
    }

    void RandomSpawn()
    {
        for(int num = 0; num < EnemyCount;)
        {
            bool isContinue = false;
            int tempNum = Random.Range(0, SpawnPos.Count);

            for(int i = 0; i < NumList.Count; i++) if (NumList[i] == tempNum) isContinue = true;

            if (isContinue) continue;
            NumList.Add(tempNum);
            num++;
        }
        //랜덤 숫자만들기까지 완성

        for (int i = 0; i < NumList.Count; i++)
        {
            int tempNum = NumList[i];
            Instantiate(EnemyPrefab, SpawnPos[tempNum]);
        }
    }
}
