using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticUIInventory : UIInventory
{
    public GameObject[] staticSlot = null;
    public override void CreateUISlots()
    {
        uiSlotLists = new Dictionary<GameObject, InventorySlot>();

        for(int i = 0; i < inventoryObj.inventorySlots.Length; i++)
        {
            GameObject gameObj = staticSlot[i];

            AddEventAction(gameObj, EventTriggerType.PointerEnter, delegate { OnEnterSlots(gameObj); });
            AddEventAction(gameObj, EventTriggerType.PointerExit, delegate { OnExitSlots(gameObj); });
            AddEventAction(gameObj, EventTriggerType.BeginDrag, delegate { OnStartDrag(gameObj); });
            AddEventAction(gameObj, EventTriggerType.EndDrag, delegate { OnEndDrag(gameObj); });
            AddEventAction(gameObj, EventTriggerType.Drag, delegate { OnMovingDrag(gameObj); });

            inventoryObj.inventorySlots[i].slotUI = gameObj;
            uiSlotLists.Add(gameObj, inventoryObj.inventorySlots[i]);
        }
    }
}
