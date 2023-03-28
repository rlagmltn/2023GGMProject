using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "StageSO", menuName = "SO/StageSO")]
public class StageSO : ScriptableObject
{
    public string stageName;

    public Sprite stageImage;

    public bool isEndStage;

    [Multiline(5)]
    public string explanationText;

    public eStageState stageKind;

    public Transform map;
}
