using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType : int
{
    DEFAULT,
    WEAPON,
    ACCESSORY
}

[CreateAssetMenu(fileName ="New Item", menuName ="Ars/New Ar")]
public class ArObj : ScriptableObject
{
    // 장비 타입
    public ItemType itemType;
    // 인벤토리에서 겹쳐지는지 여부
    public bool flagStackable;

    public string arName;
    public Sprite arIcon;
    public GameObject objModelPrefab;

    public List<string> boneNameLists = new List<string>();

    public Player arData = new Player();

    [TextArea(15, 20)]
    public string arSummery;

    private void OnValidate()
    {
        boneNameLists.Clear();

        if(objModelPrefab == null || objModelPrefab.GetComponentInChildren<SkinnedMeshRenderer>() == null)
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
