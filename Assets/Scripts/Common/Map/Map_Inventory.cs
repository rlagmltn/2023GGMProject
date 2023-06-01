using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map_Inventory : MonoSingleton<Map_Inventory>
{
    [SerializeField] private List<Button> InventoryButtons;
    [SerializeField] private ItemDBSO InventorySO;
    [SerializeField] private ItemSO EmptySO;

    private void Start()
    {
        Init();
    }

    void Init()
    {
        UpdateImage();
    }

    /// <summary>
    /// 인벤토리 정보에 들어있는 정보로 버튼의 이미지를 업데이트 해주는 함수
    /// </summary>
    void UpdateImage()
    {
        for (int num = 0; num < InventoryButtons.Count; num++)
            InventoryButtons[num].GetComponent<InventoryButton>().SetItem(InventorySO.items[num], num);
    }

    internal void SetItem(ItemSO Item)
    {
        for (int num = 0; num < InventorySO.items.Count; num++)
        {
            if (InventorySO.items[num] == EmptySO)
            {
                InventorySO.items[num] = Item as ItemSO;
                break;
            }
        }
        UpdateImage();
    }

    public void ItemChange(ItemSO Item, int num)
    {
        InventorySO.items[num] = Item;
        UpdateImage();
    }

    public void InventoryClear(int num)
    {
        InventorySO.items[num] = EmptySO;
        UpdateImage();
    }

    public bool CanAddItem()
    {
        for (int num = 0; num < InventorySO.items.Count; num++)
            if (InventorySO.items[num] == EmptySO) return true;
        return false;
    }

    public void ChangeEachOther(int Item1, int Item2)
    {
        ItemSO Temp = InventorySO.items[Item1];
        InventorySO.items[Item1] = InventorySO.items[Item2];
        InventorySO.items[Item2] = Temp;
        UpdateImage();
    }
}
