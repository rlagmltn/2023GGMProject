using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item Database", menuName ="Inventory System/Items/Database")]
public class ItemDBObj : ScriptableObject
{
    public ItemObj[] itemObjects;

    private void OnValidate()
    {
        for(int i = 0; i < itemObjects.Length; ++i)
        {
            itemObjects[i].itemData.ar_id = i;
        }
    }
}
