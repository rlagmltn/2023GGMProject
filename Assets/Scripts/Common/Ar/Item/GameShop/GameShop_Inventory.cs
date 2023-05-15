using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameShop_Inventory : MonoSingleton<GameShop_Inventory>
{
    [SerializeField] private List<Button> InventoryButtons;
    [SerializeField] private ItemDBSO InventorySO;
    [SerializeField] private ItemSO EmptySO;

    [SerializeField] private Button OutButton;

    private void Start()
    {
        Init();
    }

    void Init()
    {
        UpdateImage();

        if (OutButton == null) return;
        OutButton.onClick.RemoveAllListeners();
        OutButton.onClick.AddListener(StageClear);
    }

    /// <summary>
    /// 인벤토리 정보에 들어있는 정보로 버튼의 이미지를 업데이트 해주는 함수
    /// </summary>
    void UpdateImage()
    {
        for(int num = 0; num < InventoryButtons.Count; num++)
            InventoryButtons[num].GetComponent<InventoryButton>().SetItem(InventorySO.items[num], num);
    }

    internal void SetItem(ItemSO Item)
    {
        for(int num = 0; num < InventorySO.items.Count; num++)
        {
            if(InventorySO.items[num] == EmptySO)
            {
                InventorySO.items[num] = Item as ItemSO;
                break;
            }
        }
        UpdateImage();
    }

    internal void ItemChange(ItemSO Item, int num)
    {
        InventorySO.items[num] = Item;
        UpdateImage();
    }

    internal void InventoryClear(int num)
    {
        InventorySO.items[num] = EmptySO;
        UpdateImage();
    }

    internal bool CanAddItem()
    {
        for (int num = 0; num < InventorySO.items.Count; num++)
            if (InventorySO.items[num] == EmptySO) return true;
        return false;
    }

    internal void ChangeEachOther(int Item1, int Item2)
    {
        ItemSO Temp = InventorySO.items[Item1];
        InventorySO.items[Item1] = InventorySO.items[Item2];
        InventorySO.items[Item2] = Temp;
        UpdateImage();
    }

    //private void OnApplicationQuit() //없애야함
    //{
    //    for (int num = 0; num < InventorySO.items.Count; num++)
    //            InventorySO.items[num] = EmptySO;
    //}

    public void StageClear()
    {
        Global.EnterStage.IsCleared = true;
        SceneMgr.Instance.LoadScene(eSceneName.Map);
    }
}
