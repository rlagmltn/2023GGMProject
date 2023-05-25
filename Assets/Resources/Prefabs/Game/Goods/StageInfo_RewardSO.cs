using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardInfoSO", menuName = "SO/RewardInfoSO")]
public class StageInfo_RewardSO : ScriptableObject
{
    public Sprite RewardImage;
    public string RewardName;
    public string Summary;
}
