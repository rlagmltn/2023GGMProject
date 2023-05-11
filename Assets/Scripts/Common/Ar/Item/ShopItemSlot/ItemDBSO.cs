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
            items.Add(item);
        }
    }
    public void AddItemInItems(ItemSO _item)
    {
        items.Add(_item);
    }
}
