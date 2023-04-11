using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameShop_Character : MonoBehaviour
{
    [SerializeField] private ArSOList ArSOList;
    [SerializeField] private Transform CharacterSlot;
    [SerializeField] private Transform CharacterContent;

    private List<ArSO> TakeArList;

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        TakeArList = new List<ArSO>();
        TrashPreviewSlot();
        TakeArToList();
        InstantiateCharacterSlot();
    }

    /// <summary>
    /// �ʵ忡 �����մ� ���Ե��� �����ϴ� �Լ�
    /// </summary>
    void TrashPreviewSlot()
    {
        ShopCharacterSlot[] Trash = FindObjectsOfType<ShopCharacterSlot>();

        for(int num = 0; num < Trash.Length; num++) Destroy(Trash[num]);
    }

    /// <summary>
    /// ������ �ִ� �˵��� List�� ����ִ� �Լ�
    /// </summary>
    void TakeArToList()
    {
        for(int num = 0; num < ArSOList.list.Count; num++)
            if(ArSOList.list[num].isInGameTake) TakeArList.Add(ArSOList.list[num]);
    }

    /// <summary>
    /// �������ִ� ĳ������ ������ŭ ������ ������ִ� �Լ�
    /// </summary>
    void InstantiateCharacterSlot()
    {
        for(int num = 0; num < TakeArList.Count; num++)
        {
            Transform tempObj = Instantiate(CharacterSlot, CharacterContent);
            tempObj.GetComponent<ShopCharacterSlot>().SetArSO(TakeArList[num]);
        }
    }
}
