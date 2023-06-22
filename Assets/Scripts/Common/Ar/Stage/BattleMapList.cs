using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleMapListSO", menuName = "SO/BattleMapListSO")]
public class BattleMapList : ScriptableObject
{
    public BattleMaps[] stages;
}

[System.Serializable]
public class BattleMaps
{
    public BattleMapSO[] maps;
}