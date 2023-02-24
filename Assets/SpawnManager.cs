using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int EnemyCount = 0;

    public List<ePrefabs> enemyKinds;

    public Dictionary<ePrefabs, List<CONEntity>> _SpawnObjDic;

    void Start()
    {
        _SpawnObjDic = new Dictionary<ePrefabs, List<CONEntity>>();

        LoadPrefabs();
    }

    /// <summary>
    /// Enemy Prefab들을 들고오는 함수
    /// </summary>
    void LoadPrefabs()
    {
        foreach (ePrefabs prefab in enemyKinds)
        {
            _SpawnObjDic[prefab] = new List<CONEntity>();

            foreach (CONEntity con in MGPool.Instance.poolListDic[prefab])
            {
                _SpawnObjDic[prefab].Add(con);
                EnemyCount++;
            }
        }
    }
}
