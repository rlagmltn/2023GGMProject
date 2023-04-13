using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameShop_Drag : MonoBehaviour
{
    public Vector2 MousePosition;
    public ItemSO Item;
    [SerializeField] private Image GhostImage;
    [SerializeField] private ItemSO EmptySO;

    private bool Isfind = false;
    private bool isClickDown = false;
    private bool isClickUp = false;
    private Transform TempObj;

    private void Start()
    {
        Isfind = false;
    }

    private void Update()
    {
        InputState();
        SwitchState();
        GhostImageMove();
    }

    void InputState()
    {
        if (Input.GetMouseButtonDown(0)) isClickDown = true;
        if (Input.GetMouseButtonUp(0)) isClickUp = true;
    }

    void SwitchState()
    {
        UnityAction Action = isClickDown switch
        {
            true => OnMouseButtonDown,
            _ => null,
        };

        if (Action == null)
        {
            Action = isClickUp switch
            {
                true => OnMouseButtoneUp,
                _ => null,
            };
        }

        if (Action == null) return;

        Action();
    }

    void OnMouseButtonDown()
    {
        isClickDown = false;
        Isfind = false;
        MousePosition = GetMousePos();
        Collider2D[] hit = Physics2D.OverlapBoxAll(MousePosition, new Vector2(1, 1), 0);

        if (hit == null) return;

        foreach (var Obj in hit)
        {
            if (Obj.GetComponent<InventoryButton>() != null)
            {
                Item = Obj.GetComponent<InventoryButton>().GetItem();
                TempObj = Obj.transform;
                Isfind = true;
                break;
            }
        }

        if (!Isfind) return;
        if(Item == EmptySO) return;

        //고스트 이미지에 아이템의 이미지 넣어주고 활성화
        GhostImage.sprite = Item.itemIcon;
        GhostImage.gameObject.SetActive(true);
    }

    void OnMouseButtoneUp()
    {
        isClickUp = false;
        if (Item == EmptySO) return;
        if (Item == null) return;

        Isfind = false;
        GhostImage.gameObject.SetActive(false);

        MousePosition = GetMousePos();
        Collider2D[] hit = Physics2D.OverlapBoxAll(MousePosition, new Vector2(1, 1), 0);

        if (hit == null) return;

        int num = 0;

        foreach (var Obj in hit)
        {
            if (Obj.GetComponent<CanDrop>() != null)
            {
                num = Obj.GetComponent<CanDrop>().GetNum();
                var C_Inventory = Obj.GetComponent<CanDrop>().GetCharacterSO().E_Item.itmeSO[num];
                if (C_Inventory == EmptySO || C_Inventory is null)
                {
                    Obj.GetComponent<CanDrop>().GetCharacterSO().E_Item.itmeSO[num] = Item;
                    TempObj.GetComponent<InventoryButton>().EquipItemToCharacter();
                }
                else
                {
                    int TempNum = TempObj.GetComponent<InventoryButton>().GetNum();
                    GameShop_Inventory.Instance.ItemChange(Obj.GetComponent<CanDrop>().GetCharacterSO().E_Item.itmeSO[num], TempNum);
                    Obj.GetComponent<CanDrop>().GetCharacterSO().E_Item.itmeSO[num] = Item;
                }
                Obj.GetComponent<CanDrop>().CharacterStatReset();
                Isfind = true;
                break;
            }
        }

        if (!Isfind) return;

        Isfind = false;
        GameShop_Character.Instance.AllArUpdateUI();
        Item = null;
    }

    void GhostImageMove()
    {
        if (!Isfind) return;
        GhostImage.transform.position = GetMousePos();
    }

    Vector2 GetMousePos()
    {
        Vector2 point = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        return point;
    }
}
