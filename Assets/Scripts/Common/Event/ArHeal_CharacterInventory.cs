using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArHeal_CharacterInventory : MonoBehaviour
{
    [SerializeField] private Transform Content;

    [SerializeField] private Transform CharacterButton;

    [SerializeField] private ArSOList ArList;
    private List<ArSO> Ars;

    private void Start()
    {
        Init();
    }

    void Init()
    {
        GetIsTakeArs();
        Instantiate_ChracterButton();
    }

    /// <summary>
    /// 캐릭터 버튼들을 만들어주는 함수
    /// </summary>
    void Instantiate_ChracterButton()
    { 
        for(int num = 0; num < Ars.Count; num++)
        {
            Instantiate(Ars[num], Content);
        }
    }

    void GetIsTakeArs()
    {
        Ars = new List<ArSO>();
        foreach (ArSO ar in ArList.list)
        {
            if (!ar.isInGameTake) continue;

            if (ar.surviveStats.currentHP <= 0)
            {
                ar.isInGameTake = false;
                continue;
            }
            Ars.Add(ar);
        }
    }
}
