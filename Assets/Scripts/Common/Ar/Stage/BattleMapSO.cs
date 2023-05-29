using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleMapSO", menuName = "SO/BattleMapSO")]
public class BattleMapSO : ScriptableObject
{
    public string MapName;
    public GameObject Map;
    public List<EnemyCount> Enemies;
    public int MaxPlayers;
    public int BasePlayerTurn;
    public bool IsBossMap;
}

[System.Serializable]
public class EnemyCount
{
    public Enemy Enemy;
    public int Count;
}
