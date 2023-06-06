using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Map_Drag : MonoSingleton<Map_Drag>
{
    public Vector2 MousePosition;
    public ItemSO Item;
    [SerializeField] private Image GhostImage;
    [SerializeField] private ItemSO EmptySO;
    [SerializeField] private ItemUIInfo ItemUI;
    [SerializeField] private ScrollRect ScroolView;
    [SerializeField] private float DelayTime;

    private bool Isfind = false;
    private bool isClickDown = false;
    private bool isClickUp = false;
    private Transform TempObj;
    private int T_num;

    public bool CanClick = true;

    private float timer = 0f;

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
        //코루틴 만들어야할듯?
        timer += Time.deltaTime;
        if (!CanClick) return;
        if (timer <= DelayTime) return;

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
                T_num = Obj.GetComponent<InventoryButton>().GetNum();
                TempObj = Obj.transform;
                Isfind = true;
                break;
            }
        }

        if (!Isfind) return;
        if (ItemUI != null) ItemUI.UpdateUI(Item);
        if (Item == EmptySO) return;

        //고스트 이미지에 아이템의 이미지 넣어주고 활성화
        GhostImage.sprite = Item.itemIcon;
        GhostImage.gameObject.SetActive(true);
        ScroolView.vertical = false;
    }

    void OnMouseButtoneUp()
    {
        timer = 0f;
        isClickUp = false;
        ScroolView.vertical = true;
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
                    TempObj.GetComponent<InventoryButton>().Map_EquipItemToCharacter();
                }
                else
                {
                    int TempNum = TempObj.GetComponent<InventoryButton>().GetNum();
                    Map_Inventory.Instance.ItemChange(Obj.GetComponent<CanDrop>().GetCharacterSO().E_Item.itmeSO[num], TempNum);
                    Obj.GetComponent<CanDrop>().GetCharacterSO().E_Item.itmeSO[num] = Item;
                }
                Obj.GetComponent<CanDrop>().CharacterStatReset();
                Isfind = true;
                break;
            }

            if (Obj.GetComponent<InventoryButton>() != null)
            {
                int TempNum = Obj.GetComponent<InventoryButton>().GetNum();
                Map_Inventory.Instance.ChangeEachOther(T_num, TempNum);
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

        Vector3 vec = GetMousePos();
        vec.z = 1f;
        GhostImage.transform.position = vec;
    }

    Vector2 GetMousePos()
    {
        Vector2 point = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        return point;
    }
}
