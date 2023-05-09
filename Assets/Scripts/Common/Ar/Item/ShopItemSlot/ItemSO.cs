using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "SO/Item/Item")]
public class ItemSO : ScriptableObject
{
    public Sprite itemIcon;
    public string itemName;
    public int itemPrice;
    public ItemRarity itemRarity;
    public Stat stat;
    public int SkillCoolDown;
    public int DamageDecrease;

    [TextArea(15, 20)]
    public string itemExplain;

    public List<TypeAndInfo> TAI;
}
[System.Serializable]
public class TypeAndInfo
{
    public ItemPassiveType itemPassiveType;
    public ItemInfo Info;
}
