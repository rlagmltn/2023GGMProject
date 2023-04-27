using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmellingFeather : ItemInfo
{
    [SerializeField] int PushPowerIncreaseAmount;
    public override void Passive()
    {
        SmellingFeather_Play();
    }

    void SmellingFeather_Play()
    {
        player.pushPower += PushPowerIncreaseAmount;
    }
}
