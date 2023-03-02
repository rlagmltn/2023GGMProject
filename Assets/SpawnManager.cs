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

        //_SpawnObjDic[ePrefabs.EnemyObj1][0].SetActive(true);
    }

    private void Update()
    {
        ClearCheck();
    }

    /// <summary>
    /// Enemy Prefab���� ������ �Լ�
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

    void StageSet()
    {

    }

    /// <summary>
    /// true�϶� �������� Ŭ����
    /// </summary>
    /// <returns></returns>
    void ClearCheck() //���߿� bool�� �ٲ㼭 �����
    {
        foreach(ePrefabs prefab in enemyKinds)
        {
            foreach(CONEntity con in _SpawnObjDic[prefab])
            {
                if(con.IsActive())
                {
                    Debug.Log("Don't Clear");
                    //return false;
                }
            }
        }

        //return true;
    }
}
