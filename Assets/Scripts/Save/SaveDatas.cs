using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int ClearStages;
    public int Gold;
}

[System.Serializable]
public class PlayerData
{
    public int UpgradePoint;
    public bool[] UpgradeCheck;
    public bool tutorialCheck;
}
