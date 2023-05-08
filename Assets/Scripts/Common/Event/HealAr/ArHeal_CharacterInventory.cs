using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArHeal_CharacterInventory : MonoSingleton<ArHeal_CharacterInventory>
{
    [SerializeField] private Transform Content;

    [SerializeField] private Transform CharacterButton;

    [SerializeField] private ArSOList ArList;

    private ArSO SelectedAR;

    public List<Transform> CharacterButtons;

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
            Transform Btn = Instantiate(CharacterButton, Content);
            Btn.GetComponent<ArHeal_Button>().SetAr(Ars[num]);
            Btn.GetComponent<Button>().onClick.AddListener(Btn.GetComponent<ArHeal_Button>().ClickButton);
            CharacterButtons.Add(Btn);
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

    public void ClickButton()
    {
        for(int num = 0; num < CharacterButtons.Count; num++)
        {
            CharacterButtons[num].GetComponent<Outline>().effectColor = new Color(0, 0, 0, 0);
        }
    }

    public void GetAR(ArSO Ar)
    {
        SelectedAR = Ar;
    }
}
