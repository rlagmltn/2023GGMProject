using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameShop_Inventory : MonoSingleton<GameShop_Inventory>
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
    /// �κ��丮 ������ ����ִ� ������ ��ư�� �̹����� ������Ʈ ���ִ� �Լ�
    /// </summary>
    void UpdateImage()
    {
        for(int num = 0; num < InventoryButtons.Count; num++)
            InventoryButtons[num].GetComponent<InventoryButton>().SetItem(InventorySO.items[num]);
    }

    internal void SetItem(ItemSO Item)
    {
        for(int num = 0; num < InventorySO.items.Length; num++)
        {
            if(InventorySO.items[num] == EmptySO)
            {
                InventorySO.items[num] = Item as ItemSO;
                break;
            }
        }
        UpdateImage();
    }

    internal bool CanAddItem()
    {
        for (int num = 0; num < InventorySO.items.Length; num++)
            if (InventorySO.items[num] == EmptySO) return true;
        return false;
    }

    private void OnApplicationQuit() //���־���
    {
        for (int num = 0; num < InventorySO.items.Length; num++)
                InventorySO.items[num] = EmptySO;
    }
}
