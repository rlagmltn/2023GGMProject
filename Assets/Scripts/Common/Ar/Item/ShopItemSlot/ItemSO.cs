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

    [TextArea(15, 20)]
    public string itemExplain;

    public List<TypeAndInfo> TAI;

    public Player armedPlayer;

    public void UnArmed()
    {
        armedPlayer.stat -= stat;
        armedPlayer.skillCooltime += SkillCoolDown; //�̰� ��ϵ忡�� �������

        for (int num = 0; num < TAI.Count; num++)
        {
            TAI[num].Info.GetPlayer(armedPlayer);
            switch (TAI[num].itemPassiveType)
            {
                case ItemPassiveType.StartTurn:
                    armedPlayer.StartTurn.RemoveListener(TAI[num].Info.Passive);
                    break;
                case ItemPassiveType.EndTurn:
                    armedPlayer.EndTurn.RemoveListener(TAI[num].Info.Passive);
                    break;
                case ItemPassiveType.BeforeCrash:
                    armedPlayer.BeforeCrash.RemoveListener(TAI[num].Info.Passive);
                    break;
                case ItemPassiveType.AfterCrash:
                    armedPlayer.AfterCrash.RemoveListener(TAI[num].Info.Passive);
                    break;
                case ItemPassiveType.BeforeAttack:
                    armedPlayer.BeforeAttack.RemoveListener(TAI[num].Info.Passive);
                    break;
                case ItemPassiveType.AfterAttack:
                    armedPlayer.AfterAttack.RemoveListener(TAI[num].Info.Passive);
                    break;
                case ItemPassiveType.BeforeDefence:
                    armedPlayer.BeforeDefence.RemoveListener(TAI[num].Info.Passive);
                    break;
                case ItemPassiveType.AfterDefence:
                    armedPlayer.AfterDefence.RemoveListener(TAI[num].Info.Passive);
                    break;
                case ItemPassiveType.AfterMove:
                    armedPlayer.AfterMove.RemoveListener(TAI[num].Info.Passive);
                    break;
                case ItemPassiveType.OnOutDie:
                    armedPlayer.OnOutDie.RemoveListener(TAI[num].Info.Passive);
                    break;
                case ItemPassiveType.OnBattleDie:
                    armedPlayer.OnBattleDie.RemoveListener(TAI[num].Info.Passive);
                    break;
                case ItemPassiveType.MouseUp:
                    armedPlayer.MouseUp.RemoveListener(TAI[num].Info.Passive);
                    break;
                case ItemPassiveType.OnUsedSkill:
                    armedPlayer.OnUsedSkill.RemoveListener(TAI[num].Info.Passive);
                    break;
                case ItemPassiveType.OnCrashed:
                    armedPlayer.OnCrashed.RemoveListener(TAI[num].Info.Passive);
                    break;
                case ItemPassiveType.Alway:
                    //�̰� �߰��������� �̰� �׳� update�� ��������

                    break;
                case ItemPassiveType.Once:
                    TAI[num].Info.Passive();
                    break;
                default:
                    Debug.LogError("������ Ÿ���� �������� �ʾҽ��ϴ�!");
                    break;
            }
        }
        armedPlayer = null;
    }
}
[System.Serializable]
public class TypeAndInfo
{
    public ItemPassiveType itemPassiveType;
    public ItemInfo Info;
}
