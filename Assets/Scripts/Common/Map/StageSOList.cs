using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageSO", menuName = "SO/StageSOList")]
public class StageSOList : ScriptableObject
{
    public List<StageSO> stageList;
}
