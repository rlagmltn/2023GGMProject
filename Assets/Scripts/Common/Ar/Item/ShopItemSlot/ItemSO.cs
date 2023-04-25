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
    public int SkillCoolDown;

    public ItemPassiveType itemPassiveType;
    [TextArea(15, 20)]
    public string itemExplain;

    public ItemInfo info;
    public Player armedPlayer;

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
            case ItemPassiveType.Alway:


                break;
            default:
                Debug.LogError("������ Ÿ���� �������� �ʾҽ��ϴ�!");
                break;
        }
        armedPlayer = null;
        info.GetPlayer();
    }
}
