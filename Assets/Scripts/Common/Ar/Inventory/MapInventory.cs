using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class MapInventory : MonoSingleton<MapInventory>
{
    [SerializeField] private ArSOList ArList;
    [SerializeField] private ArSO emptyAr;
    [SerializeField] private List<Button> Buttons;
    [SerializeField] private Image ArImage;
    [SerializeField] private Transform InfoPannel;
    [SerializeField] private Image InfoImage;
    [SerializeField] private TextMeshProUGUI Ar_NameText;
    [SerializeField] private List<TextMeshProUGUI> InfoTexts;
    [SerializeField] private Transform InfoStatPannel;
    [SerializeField] private Transform InfoSkillPannel;
    [SerializeField] private Button InfoStatButton;
    [SerializeField] private Button InfoSkillButton;

    private bool isSkillPannel = false;
    private ArSO SelectedAR;
    private List<ArSO> sortedArSO;

    private void Start()
    {
        Init();
    }

    void Init()
    {
        SelectedAR = emptyAr;

        ClassfyArSO();
        ButtonInit();
        UpdateUI();
    }

    /// <summary>
    /// 알들을 정렬 하는 함수
    /// </summary>
    private void ClassfyArSO()
    {
        sortedArSO = new List<ArSO>();

        for (int num = 0; num < ArList.list.Count; num++)
            if (ArList.list[num].isTake) sortedArSO.Add(ArList.list[num]);

        for (int num = 0; num < ArList.list.Count; num++)
            if (!ArList.list[num].isTake) sortedArSO.Add(ArList.list[num]);

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
            num++;
        }
    }

    void UpdateUI()
    {
        InfoSkillPannel.gameObject.SetActive(isSkillPannel);
        InfoStatPannel.gameObject.SetActive(!isSkillPannel);

        if (SelectedAR == emptyAr)
        {
            InfoPannel.gameObject.SetActive(false);
            return;
        }

        InfoPannel.gameObject.SetActive(true);

        if (isSkillPannel)
        {
            InfoImage.sprite = SelectedAR.skill.SkillImage;
            Ar_NameText.text = SelectedAR.skill.SkillName;
            InfoTexts[6].text = SelectedAR.skill.SkillSummary;
            return;
        }
        InfoImage.sprite = SelectedAR.characterInfo.Image;
        Ar_NameText.text = SelectedAR.characterInfo.Name;
        InfoTexts[0].text = $" : {SelectedAR.surviveStats.BaseHP}";
        InfoTexts[1].text = $" : {SelectedAR.surviveStats.BaseShield}";
        InfoTexts[2].text = $" : {SelectedAR.attackStats.BaseAtk}";
        InfoTexts[3].text = $" : {SelectedAR.attackStats.BaseSkillAtk}";
        InfoTexts[4].text = $" : {SelectedAR.criticalStats.BaseCriticalPer}";
        InfoTexts[5].text = $" : {SelectedAR.surviveStats.BaseWeight}";
    }

    void ButtonInit()
    {
        foreach (Button btn in Buttons)
        {
            RemoveAllButtonEvents(btn);
            AddButtonEvent(btn, btn.GetComponent<ArSOHolder_Map>().SelectedButton);
        }

        AddButtonEvent(InfoStatButton, StatInfoClick);
        AddButtonEvent(InfoSkillButton, SkillInfoClick);
    }

    public void SelectArSO(ArSO Ar)
    {
        SelectedAR = Ar;
        isSkillPannel = false;
        UpdateUI();
    }

    void StatInfoClick()
    {
        isSkillPannel = false;
        UpdateUI();
    }
    void SkillInfoClick()
    {
        isSkillPannel = true;
        UpdateUI();
    }

    void RemoveAllButtonEvents(Button button)
    {
        button.onClick.RemoveAllListeners();
    }

    void AddButtonEvent(Button button, UnityAction action)
    {
        button.onClick.AddListener(action);
    }

    private void OnApplicationQuit()
    {
        foreach (ArSO ar in ArList.list)
        {
            ar.isUse = false;
        }
    }
}
