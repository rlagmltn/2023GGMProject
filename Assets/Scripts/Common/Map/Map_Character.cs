using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Character : MonoSingleton<Map_Character>
{
    [SerializeField] private ArSOList ArSOList;
    [SerializeField] private Transform CharacterSlot;
    [SerializeField] private Transform CharacterContent;

    private List<ArSO> TakeArList;
    private List<Transform> Pannels;

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        TakeArList = new List<ArSO>();
        Pannels = new List<Transform>();

        TrashPreviewSlot();
        TakeArToList();
        InstantiateCharacterSlot();
    }

    /// <summary>
    /// 필드에 남아잇는 슬롯들을 제거하는 함수
    /// </summary>
    void TrashPreviewSlot()
    {
        ShopCharacterSlot[] Trash = FindObjectsOfType<ShopCharacterSlot>();

        for (int num = 0; num < Trash.Length; num++) Destroy(Trash[num]);
    }

    /// <summary>
    /// 가지고 있는 알들을 List에 담아주는 함수
    /// </summary>
    void TakeArToList()
    {
        for (int num = 0; num < ArSOList.list.Count; num++)
            if (ArSOList.list[num].isInGameTake) TakeArList.Add(ArSOList.list[num]);
    }

    /// <summary>
    /// 가지고있는 캐릭터의 갯수만큼 슬롯을 만들어주는 함수
    /// </summary>
    void InstantiateCharacterSlot()
    {
        for (int num = 0; num < TakeArList.Count; num++)
        {
            Transform tempObj = Instantiate(CharacterSlot, CharacterContent);
            tempObj.GetComponent<ShopCharacterSlot>().SetArSO(TakeArList[num]);
            Pannels.Add(tempObj);
        }
    }

    internal void AllArUpdateUI()
    {
        for (int num = 0; num < Pannels.Count; num++)
        {
            Pannels[num].GetComponent<ShopCharacterSlot>().UpdateUI();
        }
    }
}
