using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int ClearStages = 0;
    public int Gold = 0;
    public int PointPoint = 0;
    public bool IsPlayingGame = false;
}

[System.Serializable]
public class PlayerData
{
    public int UpgradePoint = 0;
    public bool[] UpgradeCheck = { };
    public bool tutorialCheck = false;
}

[System.Serializable]
public class TutoData
{
    public bool mainTuto;
    public bool trainingTuto;
    public bool adventrueTuto;
    public bool beforeBattleTuto;
    public bool battleTuto;
    public bool battleEnemyTuto;
}
