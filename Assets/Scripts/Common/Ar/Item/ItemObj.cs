using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType : int
{
    Hair,
    Head,
    Body,
    Pants,
    LeftHand,
    RightHand,
    Default,
}

[CreateAssetMenu(fileName ="New Item", menuName ="Inventory System/Items/New Item")]
public class ItemObj : ScriptableObject
{
    // 장비 타입
    public ItemType itemType;
    // 인벤토리에서 겹쳐지는지 여부
    public bool flagStackable;

    public Sprite itemIcon;
    public GameObject objModelPrefab;

    public List<string> boneNameLists = new List<string>();

    public Player itemData = new Player();

    [TextArea(15, 20)]
    public string itemSummery;

    private void OnValidate()
    {
        boneNameLists.Clear();

        if(objModelPrefab == null || objModelPrefab.GetComponentInChildren<SkinnedMeshRenderer>() == null )
        {
            return;
        }

        SkinnedMeshRenderer renderer = objModelPrefab.GetComponentInChildren<SkinnedMeshRenderer>();
        var bones = renderer.bones;
        foreach (var t in bones)
        {
            boneNameLists.Add(t.name);
        }
    }

    public Player CreateItemObj()
    {
        Player new_Item = new Player(this);
        return new_Item;
    }

}
