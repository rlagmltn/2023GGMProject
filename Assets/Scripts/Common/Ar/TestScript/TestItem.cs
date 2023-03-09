using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : MonoBehaviour
{
    public InventoryObj inventoryObject;
    public ItemDBObj databaseObject;

    public void AddNewItem()
    {
        if( databaseObject.itemObjects.Length > 0)
        {
            ItemObj newItemObject = databaseObject.itemObjects[Random.Range(0, databaseObject.itemObjects.Length)];

            inventoryObject.AddItem(newItemObject.itemData, 1);
        }
    }

    public void ClearInventory()
    {
        inventoryObject?.Clear();
    }
}
