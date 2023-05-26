using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyInfoPannel : MonoSingleton<EnemyInfoPannel>
{
    [SerializeField] private Image EnemyImage;
    [SerializeField] private TextMeshProUGUI EnemyNameText;
    [SerializeField] private TextMeshProUGUI EnemySummaryText;

    public void EnemyInfoPannelUpdate(EnemyInfoSO Enemy)
    {
        EnemyImage.sprite = Enemy.EnemyImage;
        EnemyNameText.text = Enemy.EnemyName;
        EnemySummaryText.text = Enemy.Summary;
    }
}
