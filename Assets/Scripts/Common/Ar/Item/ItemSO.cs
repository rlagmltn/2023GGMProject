using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "SO/Item/Item")]
public class ItemSO : ScriptableObject
{
    public Sprite itemIcon;
    public string itemName;
    public int itemPrice;
    public Stat stat;
    public ItemPassiveType itemPassiveType;
    [TextArea(15, 20)]
    public string itemExplain;

    public ItemInfo info;
    private Player armedPlayer;


    public void Armed(Player player)
    {
        player.stat += stat;
        switch(itemPassiveType)
        {
            case ItemPassiveType.BeforeCrash:
                player.BeforeCrash.AddListener(info.Passive);
                break;
            case ItemPassiveType.AfterCrash:
                player.AfterCrash.AddListener(info.Passive);
                break;
            case ItemPassiveType.BeforeAttack:
                player.BeforeAttack.AddListener(info.Passive);
                break;
            case ItemPassiveType.AfterAttack:
                player.AfterAttack.AddListener(info.Passive);
                break;
            case ItemPassiveType.BeforeDefence:
                player.BeforeDefence.AddListener(info.Passive);
                break;
            case ItemPassiveType.AfterDefence:
                player.AfterDefence.AddListener(info.Passive);
                break;
            case ItemPassiveType.AfterMove:
                player.AfterMove.AddListener(info.Passive);
                break;
            case ItemPassiveType.OnOutDie:
                player.OnOutDie.AddListener(info.Passive);
                break;
            case ItemPassiveType.OnBattleDie:
                player.OnBattleDie.AddListener(info.Passive);
                break;
            case ItemPassiveType.MouseUp:
                player.MouseUp.AddListener(info.Passive);
                break;
            default:
                Debug.LogError("아이템 타입이 정해지지 않았습니다!");
                break;
        }
        armedPlayer = player;
    }

    public void UnArmed()
    {
        armedPlayer.stat -= stat;
        switch (itemPassiveType)
        {
            case ItemPassiveType.BeforeCrash:
                armedPlayer.BeforeCrash.RemoveListener(info.Passive);
                break;
            case ItemPassiveType.AfterCrash:
                armedPlayer.AfterCrash.RemoveListener(info.Passive);
                break;
            case ItemPassiveType.BeforeAttack:
                armedPlayer.BeforeAttack.RemoveListener(info.Passive);
                break;
            case ItemPassiveType.AfterAttack:
                armedPlayer.AfterAttack.RemoveListener(info.Passive);
                break;
            case ItemPassiveType.BeforeDefence:
                armedPlayer.BeforeDefence.RemoveListener(info.Passive);
                break;
            case ItemPassiveType.AfterDefence:
                armedPlayer.AfterDefence.RemoveListener(info.Passive);
                break;
            case ItemPassiveType.AfterMove:
                armedPlayer.AfterMove.RemoveListener(info.Passive);
                break;
            case ItemPassiveType.OnOutDie:
                armedPlayer.OnOutDie.RemoveListener(info.Passive);
                break;
            case ItemPassiveType.OnBattleDie:
                armedPlayer.OnBattleDie.RemoveListener(info.Passive);
                break;
            case ItemPassiveType.MouseUp:
                armedPlayer.MouseUp.RemoveListener(info.Passive);
                break;
            default:
                Debug.LogError("아이템 타입이 정해지지 않았습니다!");
                break;
        }
        armedPlayer = null;
    }
}
