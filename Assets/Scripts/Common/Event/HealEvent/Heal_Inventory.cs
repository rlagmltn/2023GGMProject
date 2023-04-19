using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heal_Inventory : MonoSingleton<Heal_Inventory>
{
    [SerializeField] private ArSO emptyAr;
    [SerializeField] private List<Button> Buttons;
    [SerializeField] private ArSOList ArList;

    private List<ArSO> sortedArSO;

    private void Start()
    {
        ClassfyArSO();
    }

    /// <summary>
    /// 알들을 정렬 하는 함수
    /// </summary>
    private void ClassfyArSO()
    {
        sortedArSO = new List<ArSO>();

        for (int num = 0; num < ArList.list.Count; num++)
            if (ArList.list[num].isInGameTake) sortedArSO.Add(ArList.list[num]);

        for (int num = 0; num < ArList.list.Count; num++)
            if (!ArList.list[num].isInGameTake) sortedArSO.Add(ArList.list[num]);

        AddButtonInstance();
    }

    /// <summary>
    /// 버튼에 정보를 전해주는 함수
    /// </summary>
    void AddButtonInstance()
    {
        int num = 0;
        ArSO _Ar = null;
        foreach (Button btn in Buttons)
        {
            _Ar = null;
            if (sortedArSO.Count > num) _Ar = sortedArSO[num];

            btn.GetComponent<ArSOHolder_Map>().SetArSO(_Ar);
            btn.onClick.AddListener(btn.GetComponent<ArSOHolder_Map>().SelectButton_Heal);
            num++;
        }
    }
}
