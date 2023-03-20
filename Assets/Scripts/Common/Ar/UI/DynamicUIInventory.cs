using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicUIInventory : UIInventory
{
    [SerializeField]
    protected GameObject prefabSlot;

    [SerializeField]
    protected Vector2 start;

    [SerializeField]
    protected Vector2 size;

    [SerializeField]
    protected Vector2 space;

    protected int numCols = 4;

    public override void CreateUISlots()
    {
        uiSlotLists = new Dictionary<GameObject, ArInventorySlot>();

        for(int i =0; i < inventoryObj.inventorySlots.Length; ++i)
        {
            GameObject gameObj = Instantiate(prefabSlot, Vector3.zero, Quaternion.identity, transform);

            gameObj.GetComponent<RectTransform>().anchoredPosition = CalculatePosition(i);

            AddEventAction(gameObj, EventTriggerType.PointerEnter, delegate { OnEnterSlots(gameObj); });
            AddEventAction(gameObj, EventTriggerType.PointerExit, delegate { OnExitSlots(gameObj); });
            AddEventAction(gameObj, EventTriggerType.BeginDrag, delegate { OnStartDrag(gameObj); });
            AddEventAction(gameObj, EventTriggerType.EndDrag, delegate { OnEndDrag(gameObj); });
            AddEventAction(gameObj, EventTriggerType.Drag, delegate { OnMovingDrag(gameObj); });

            inventoryObj.inventorySlots[i].slotUI = gameObj;
            uiSlotLists.Add(gameObj, inventoryObj.inventorySlots[i]);
            gameObj.name += ": " + i;
        }
    }

    public Vector3 CalculatePosition(int i)
    {
        float x = start.x + ((space.x + size.x) * (i % numCols));
        float y = start.y + (-(space.y + size.y) * (i / numCols));

        return new Vector3(x, y, 0f);
    }

}
