using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameShop_Drag : MonoBehaviour
{
    public Vector2 MousePosition;
    public ItemSO Item;
    [SerializeField] private Image GhostImage;
    [SerializeField] private ItemSO EmptySO;

    private bool Isfind = false;

    private void Start()
    {
        Isfind = false;
    }

    private void Update()
    {
        OnMouseButtonDown();
        OnMousButtoneUp();
        GhostImageMove();
    }

    void OnMouseButtonDown()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Isfind = false;
            MousePosition = GetMousePos();
            Collider2D[] hit = Physics2D.OverlapBoxAll(MousePosition, new Vector2(1, 1), 0);

            if (hit == null) return;

            foreach (var Obj in hit)
            {
                if (Obj.GetComponent<InventoryButton>() != null)
                {
                    Item = Obj.GetComponent<InventoryButton>().GetItem();
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
    }

    void OnMousButtoneUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
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
                    Obj.GetComponent<CanDrop>().GetCharacterSO().E_Item.itmeSO[num] = Item;
                    Isfind = true;
                    break;
                }
            }

            if (!Isfind) return;
            if (Item == null) return;
            if (Item == EmptySO) return;
            Isfind = false;
            GameShop_Character.Instance.AllArUpdateUI();
            Item = null;
        }
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
