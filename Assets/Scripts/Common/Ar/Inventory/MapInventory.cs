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
    [SerializeField] private TextMeshProUGUI StageSumarryText;

    [SerializeField] private List<Image> ItemImage;

    private StageSO Selected_Stage;

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
            num++;
        }
    }

    public void UpdateUI()
    {
        if (TestStageManager.Instance.GetStage() is null) return;
        string TempText = TestStageManager.Instance.GetStage().stageKind switch
        {
            eStageState.Battle => "전투 스테이지",
            eStageState.Boss => "보스 스테이지",
            eStageState.Event => "이벤트 스테이지",
            eStageState.Shop => "상점 스테이지",
            _ => "정체를 알 수 없는 스테이지",
        };
        StageSumarryText.text = TempText;

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

        for(int num = 0; num < 3; num++)
        {
            if(SelectedAR.E_Item.itmeSO[num] == null)
            {
                ItemImage[num].sprite = null;
                ItemImage[num].color = new Color(0.78f, 0.78f, 0.78f, 0.78f);
                continue;
            }

            ItemImage[num].sprite = SelectedAR.E_Item.itmeSO[num].itemIcon;
            ItemImage[num].color = new Color(1, 1, 1, 1);
        }

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
            ar.isInGameTake = false;
        }
    }
    
    private void SetStage()
    {
        Selected_Stage = TestStageManager.Instance.GetStage();
    }
}
