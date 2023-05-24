using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemDB", menuName = "SO/Item/ItemDBs")]
public class ItemDBSO : ScriptableObject
{
    public List<ItemSO> items;

    public void AddItemsInItems(ItemSO[] _items)
    {
        foreach(ItemSO item in _items)
        {
            for(int i=0; i<items.Count; i++)
            {
                if(items[i].itemName=="EMPTY")
                {
                    items[i] = item;
                    break;
                }
            }
        }
    }

    public void AddItemInItems(ItemSO _item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == "EMPTY")
            {
                items[i] = _item;
                break;
            }
        }
    }

    public void ResetInven(ItemSO item)
    {
        items.Add(item);
    }
}
