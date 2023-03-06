using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class MouseTransformData
{
    // UI 인벤토리에 위치해 있는지?
    public static UIInventory mouseInventory;
    // 마우스가 드래그앤드랍 중인가?
    public static GameObject mouseDragging;
    // Slot UI에 위치해 있는지?
    public static GameObject mouseSlot;
}

[RequireComponent(typeof(EventTrigger))]
public abstract class UIInventory : MonoBehaviour
{
    public InventoryObj inventoryObj;
    private InventoryObj beforeInventoryObj;

    public Dictionary<GameObject, InventorySlot> uiSlotLists = new Dictionary<GameObject, InventorySlot>();

    private void Awake()
    {
        CreateUISlots();

        for(int i = 0; i< inventoryObj.inventorySlots.Length; ++i)
        {
            inventoryObj.inventorySlots[i].inventoryObj = inventoryObj;
            inventoryObj.inventorySlots[i].OnPostUpload += OnEquipUpdate;
        }

        AddEventAction(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInventory(gameObject); });
        AddEventAction(gameObject, EventTriggerType.PointerExit, delegate { OnExitInventory(gameObject); });
    }

    protected virtual void Start()
    {
        for(int i = 0; i < inventoryObj.inventorySlots.Length; ++i)
        {
            inventoryObj.inventorySlots[i].UploadSlot(inventoryObj.inventorySlots[i].item, inventoryObj.inventorySlots[i].itemCnt);
        }
    }

    public abstract void CreateUISlots();

    protected void AddEventAction(GameObject gameObj, EventTriggerType eventTriggerType, UnityAction<BaseEventData> baseEventDataAction)
    {
        EventTrigger eventTrigger = gameObj.GetComponent<EventTrigger>();
        if (!eventTrigger)
        {
            Debug.LogWarning("Nothing Events");
            return;
        }

        EventTrigger.Entry eventTriggerEntry = new EventTrigger.Entry { eventID = eventTriggerType };
        eventTriggerEntry.callback.AddListener(baseEventDataAction);

        eventTrigger.triggers.Add(eventTriggerEntry);
    }

    public void OnEquipUpdate(InventorySlot inventorySlot)
    {
        inventorySlot.slotUI.transform.GetChild(0).GetComponent<Image>().sprite = inventorySlot.item.ar_id < 0 ? null : inventorySlot.ItemObject.itemIcon;

        inventorySlot.slotUI.transform.GetChild(0).GetComponent<Image>().color = inventorySlot.item.ar_id < 0 ? new Color(1, 1, 1, 0) : new Color(1, 1, 1, 1);

        inventorySlot.slotUI.GetComponentInChildren<TextMeshProUGUI>().text = inventorySlot.item.ar_id < 0 ? string.Empty : (inventorySlot.itemCnt == 1 ? string.Empty : inventorySlot.itemCnt.ToString("n0"));
    }

    public void OnEnterInventory(GameObject gameObj)
    {
        MouseTransformData.mouseInventory = gameObj.GetComponent<UIInventory>();
    }

    public void OnExitInventory(GameObject gameObj)
    {
        MouseTransformData.mouseInventory = null;
    }

    public void OnEnterSlots(GameObject gameObj)
    {
        MouseTransformData.mouseSlot = gameObj;
        MouseTransformData.mouseInventory = gameObj.GetComponentInParent<UIInventory>();
    }

    public void OnExitSlots(GameObject gameObj)
    {
        MouseTransformData.mouseSlot = null;
    }

    public void OnStartDrag(GameObject gameObj)
    {
        MouseTransformData.mouseDragging = AddEventDragImage(gameObj);
    }

    public void OnMovingDrag(GameObject gameObj)
    {
        if (MouseTransformData.mouseDragging == null)
            return;

        MouseTransformData.mouseDragging.GetComponent<RectTransform>().position = Input.mousePosition;
    }

    public void OnEndDrag(GameObject gameObj)
    {
        Destroy(MouseTransformData.mouseDragging);

        if(MouseTransformData.mouseInventory == null)
        {
            uiSlotLists[gameObj].DestoryItem();
        }
        else if( MouseTransformData.mouseSlot )
        {
            InventorySlot mouseHoverSlotData = MouseTransformData.mouseInventory.uiSlotLists[MouseTransformData.mouseSlot];

            inventoryObj.SwapItems(uiSlotLists[gameObj], mouseHoverSlotData);
        }
    }

    private GameObject AddEventDragImage(GameObject gameObj)
    {
        if( uiSlotLists.ContainsKey(gameObj)==false || uiSlotLists[gameObj].item.ar_id < 0 )
        {
            return null;
        }

        GameObject imgDrags = new GameObject();

        RectTransform rectTransform = imgDrags.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(50, 50);
        imgDrags.transform.SetParent(transform.parent);

        Image image = imgDrags.AddComponent<Image>();
        image.sprite = uiSlotLists[gameObj].ItemObject.itemIcon;
        image.raycastTarget = false;

        imgDrags.name = "Drag Image";

        return imgDrags;
    }


    
}
