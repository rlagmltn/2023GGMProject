using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemDB", menuName = "SO/Item/ItemDBs")]
public class ItemDBSO : ScriptableObject
{
    public ItemSO[] items;

    public void AddItemsInItems(ItemSO[] _items)
    {
        int i = 0;
        for(; i<items.Length; i++)
        {
            if (items[i] == null) break;
        }

        foreach(ItemSO item in _items)
        {
            if(i>=items.Length)
            {

            }
            items[i] = item;
            i++;
        }
    }
    public void AddItemInItems(ItemSO _item)
    {
        int i = 0;
        for (; i < items.Length; i++)
        {
            if (items[i] == null) break;
        }

        if(i>=items.Length)
        {

        }
        items[i] = _item;
    }
}
