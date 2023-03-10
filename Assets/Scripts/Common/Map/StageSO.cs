using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageSO", menuName = "SO/StageSO")]
public class StageSO : ScriptableObject
{
    public string stageName;

    [Multiline(5)]
    public string explanationText;

    public eStageState stageKind;

    public Transform map;
}
