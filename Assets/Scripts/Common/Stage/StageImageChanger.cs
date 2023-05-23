using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageImageChanger : MonoSingleton<StageImageChanger>
{
    [SerializeField] Sprite BattleImage;
    [SerializeField] Sprite EventImage;
    [SerializeField] Sprite ShopImage;
    [SerializeField] Sprite BossImage;

    public Sprite GetStageImage(eStageState StageState) => StageState switch
    {
        eStageState.Battle => BattleImage,
        eStageState.Event => EventImage,
        eStageState.Shop => ShopImage,
        eStageState.Boss => BossImage,
        _ => BattleImage,
    };
}
