using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealAr : _Event
{
    [SerializeField] private Transform HealArPannel;

    [SerializeField] private Button OptionButton_1;
    [SerializeField] private Button OptionButton_2;
    [SerializeField] private Button OptionButton_3;

    [SerializeField] private Transform OptionPopUp_1;
    [SerializeField] private Transform OptionPopUp_2;
    [SerializeField] private Transform OptionPopUp_3;

    [SerializeField] private TextMeshProUGUI OptionText_1;
    [SerializeField] private TextMeshProUGUI OptionText_2;
    [SerializeField] private TextMeshProUGUI OptionText_3;

    [SerializeField] private Transform BackGroundPannel;

    [SerializeField] private ArSOList ArList;
    private List<ArSO> Ars;

    [SerializeField] private List<Button> ClearButtons;

    public override void EventStart()
    {
        GetIsTakeArs();
        EventInit();
    }

    void EventInit()
    {
        HealArPannel.gameObject.SetActive(true);
        ButtonInit();
        TwinManager_Event.Instance.SetDAP(0.7f, OptionButton_1.transform);
        TwinManager_Event.Instance.SetDAP(0.2f, OptionButton_2.transform);
        TwinManager_Event.Instance.SetDAP(0.2f, OptionButton_3.transform);
        TwinManager_Event.Instance.EventStart();
    }

    void ButtonInit()
    {
        OptionButton_1.onClick.RemoveAllListeners();
        OptionButton_2.onClick.RemoveAllListeners();
        OptionButton_3.onClick.RemoveAllListeners();

        OptionButton_1.onClick.AddListener(OptionButton1);
        OptionButton_2.onClick.AddListener(OptionButton2);
        OptionButton_3.onClick.AddListener(OptionButton3);

        for(int num = 0; num < ClearButtons.Count; num++)
        {
            ClearButtons[num].onClick.RemoveAllListeners();
            ClearButtons[num].onClick.AddListener(EventManger.Instance.StageClear);
        }
    }

    void OptionButton1()
    {
        BackGroundPannel.gameObject.SetActive(true);
        OptionPopUp_1.gameObject.SetActive(true);
    }

    void OptionButton2()
    {
        int Rnum = Random.Range(0, Ars.Count);
        DecreaseArHP(Ars[Rnum], 30);
        HealAll(Rnum, 50);
        OptionText_2.text = $"제단에 손을 올리고 체력을 바쳤다.\n{Ars[Rnum].characterInfo.Name} 체력{(int)((float)Ars[Rnum].surviveStats.MaxHP * 30f / 100f)}만큼 감소, 남은체력 : {(int)Ars[Rnum].surviveStats.currentHP}\n모든 아군 체력 절반 회복";
        BackGroundPannel.gameObject.SetActive(true);
        OptionPopUp_2.gameObject.SetActive(true);
    }

    void OptionButton3()
    {
        int Rnum = Random.Range(0, Ars.Count);
        DecreaseArHP(Ars[Rnum], 10);
        OptionText_3.text = $"건방진 자식! 변장했던 악마가 저주를 걸고 사라졌다.\n{Ars[Rnum].characterInfo.Name} 체력{(int)((float)Ars[Rnum].surviveStats.MaxHP * 10f / 100f)}만큼 감소, 남은체력 : {(int)Ars[Rnum].surviveStats.currentHP}";
        BackGroundPannel.gameObject.SetActive(true);
        OptionPopUp_3.gameObject.SetActive(true);
    }

    void DecreaseArHP(ArSO Ar, int percent)
    {
        int MaxHP = (int)Ar.surviveStats.MaxHP;
        Ar.surviveStats.currentHP -= (int)((float)MaxHP * (float)percent / 100f);
        if (Ar.surviveStats.currentHP <= 0)
        {
            Ar.surviveStats.currentHP = 0;
            Ar.isDead = true;
            Ar.isInGameTake = false;
        }
    }

    void HealAll(int ExceptNum, float percent)
    {
        for(int i = 0; i < Ars.Count; i++)
        {
            if (i == ExceptNum) continue;
            if (Ars[i].isDead) continue;
            if (!Ars[i].isInGameTake) continue;
            Ars[i].surviveStats.currentHP += (int)(Ars[i].surviveStats.MaxHP * (percent / 100f));
            if (Ars[i].surviveStats.currentHP >= Ars[i].surviveStats.MaxHP) Ars[i].surviveStats.currentHP = Ars[i].surviveStats.MaxHP;
        }
    }

    void GetIsTakeArs()
    {
        Ars = new List<ArSO>();
        foreach (ArSO ar in ArList.list)
        {
            if (!ar.isInGameTake) continue;

            if(ar.surviveStats.currentHP <= 0)
            {
                ar.isInGameTake = false;
                continue;
            }

            Ars.Add(ar);
        }
    }
}
