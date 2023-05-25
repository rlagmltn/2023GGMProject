using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "StageSO", menuName = "SO/StageSO")]
public class StageSO : ScriptableObject
{
    public StageInfo stageInfo;

    public eStageState stageKind;

    public List<EnemyInfoSO> StageEnemy;

    public List<Sprite> stageRewardSprite;

    public bool IsCleared;

    public bool IsCanEnter;

    public Transform map;
}

[System.Serializable]
public class StageInfo
{
    public string stageName;

    public Sprite stageImage;

    [Multiline(5)]
    public string explanationText;
}