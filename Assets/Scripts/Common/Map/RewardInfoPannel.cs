using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardInfoPannel : MonoSingleton<RewardInfoPannel>
{
    [SerializeField] private Image RewardImage;
    [SerializeField] private TextMeshProUGUI NameText;
    [SerializeField] private TextMeshProUGUI SummaryText;

    public void EnemyInfoPannelUpdate(StageInfo_RewardSO Reward)
    {
        RewardImage.sprite = Reward.RewardImage;
        NameText.text = Reward.RewardName;
        SummaryText.text = Reward.Summary;
    }
}
