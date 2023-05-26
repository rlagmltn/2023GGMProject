using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUIManager : MonoSingleton<StageUIManager>
{
    [SerializeField] private Transform StageInfoPannel;
    [SerializeField] private List<Transform> AllButtons;
    [SerializeField] private List<Transform> EnemyButtons;
    [SerializeField] private List<Transform> RewardButtons;
        
    [SerializeField] private Button CancelButton;
    [SerializeField] private Button StartButton;

    [SerializeField] private List<Transform> CharacterButtons;
    [SerializeField] private ArSOList ArList;

    [SerializeField] private Transform BackgroundPannel;
    [SerializeField] private Transform _EnemyInfoPannel;
    [SerializeField] private Transform _RewardInfoPannel;

    [SerializeField] private List<Button> EnemyInfoButtons;
    [SerializeField] private Button RightArrowButton;
    [SerializeField] private Button LeftArrowButton;
    [SerializeField] private Button RewardPannelCancelButton;

    [SerializeField] private Transform BackGroundPannel_2;

     
    private StageSO Selected_Stage;
    private List<ArSO> sortedArSO;
    private List<EnemyInfoSO> EnemyInfos;
    private List<StageInfo_RewardSO> RewardInfos;
    private int CurrentEnemyInfo = 0;

    private void Start()
    {
        Init();
    }

    void Init()
    {
        ButtonInit();
        ClassfyArSO();
    }

    void ButtonInit()
    {
        for (int num = 0; num < AllButtons.Count; num++)
        {
            AllButtons[num].GetComponent<Button>().onClick.RemoveAllListeners();
            AllButtons[num].GetComponent<Button>().onClick.AddListener(AllButtons[num].GetComponent<StageSOHolder>().StageSelect);
        }

        for (int num = 0; num < EnemyButtons.Count; num++)
        {
            EnemyButtons[num].GetComponent<Button>().onClick.RemoveAllListeners();
            EnemyButtons[num].GetComponent<StageInfo_EnemyButton>().SetEnemyNum(num);
            var EnemyBtn = EnemyButtons[num].GetComponent<StageInfo_EnemyButton>().GetEnemyNum();
            EnemyButtons[num].GetComponent<Button>().onClick.AddListener(() => EnemyInfoActiveTrue(EnemyBtn));

            EnemyInfoButtons[num].GetComponent<Button>().onClick.RemoveAllListeners();
            EnemyInfoButtons[num].GetComponent<StageInfo_EnemyButton>().SetEnemyNum(num);
            var EnemyInfoBtn = EnemyButtons[num].GetComponent<StageInfo_EnemyButton>().GetEnemyNum();
            EnemyInfoButtons[num].GetComponent<Button>().onClick.AddListener(() => EnemyInfoActiveTrue(EnemyInfoBtn));
        }

        for(int num = 0; num < RewardButtons.Count; num++)
        {
            RewardButtons[num].GetComponent<Button>().onClick.RemoveAllListeners();
            RewardButtons[num].GetComponent<StageInfo_EnemyButton>().SetEnemyNum(num);
            var RewardBtn = RewardButtons[num].GetComponent<StageInfo_EnemyButton>().GetEnemyNum();
            RewardButtons[num].GetComponent<Button>().onClick.AddListener(() => RewardInfoActiveTrue(RewardBtn));
        } //여기 리워드


        CancelButton.onClick.RemoveAllListeners();
        StartButton.onClick.RemoveAllListeners();
        BackgroundPannel.GetComponent<Button>().onClick.RemoveAllListeners();
        RightArrowButton.onClick.RemoveAllListeners();
        LeftArrowButton.onClick.RemoveAllListeners();
        RewardPannelCancelButton.onClick.RemoveAllListeners();

        CancelButton.onClick.AddListener(() => StageInfoPannelActive(false));
        StartButton.onClick.AddListener(StageManager.Instance.StageEnter);
        BackgroundPannel.GetComponent<Button>().onClick.AddListener(EnemyInfoActiveFalse);
        RightArrowButton.onClick.AddListener(EnemyInfoPlus);
        LeftArrowButton.onClick.AddListener(EnemyInfoMinus);
        RewardPannelCancelButton.onClick.AddListener(RewardInfoActiveFalse);
    }

    public void SetSelectedStage(StageSO stage)
    {
        Selected_Stage = stage;
        StageInfoPannelActive(true);
        EnemyUI();
        RewardUI();
    }

    void StageInfoPannelActive(bool isActive)
    {
        StageInfoPannel.gameObject.SetActive(isActive);
    }

    private void ClassfyArSO()
    {
        sortedArSO = new List<ArSO>();

        for (int num = 0; num < ArList.list.Count; num++)
            if (ArList.list[num].isInGameTake && !ArList.list[num].isDead) sortedArSO.Add(ArList.list[num]);

        UpdateStageInfoPannel_Scrollview();
    }

    void UpdateStageInfoPannel_Scrollview()
    {
        for(int num = 0; num < CharacterButtons.Count; num++)
        {
            if(num > sortedArSO.Count - 1)
            {
                CharacterButtons[num].GetComponent<ArSOHolder_Map>().SetArSO(null);
                continue;
            }
            CharacterButtons[num].GetComponent<ArSOHolder_Map>().SetArSO(sortedArSO[num]);
        }
    }

    void EnemyUI()
    {
        EnemyInfos = new List<EnemyInfoSO>();
        for (int num = 0; num < EnemyButtons.Count; num++)
        {
            EnemyButtons[num].gameObject.SetActive(true);

            if (num <= Selected_Stage.StageEnemy.Count - 1)
            {
                EnemyButtons[num].GetComponent<StageInfo_EnemyButton>().SetEnemyNum(num);
                EnemyButtons[num].GetChild(0).GetComponent<Image>().sprite = Selected_Stage.StageEnemy[num].EnemyImage;
                EnemyInfos.Add(Selected_Stage.StageEnemy[num]);

                EnemyInfoButtons[num].GetComponent<StageInfo_EnemyButton>().SetEnemyNum(num);
                EnemyInfoButtons[num].transform.GetChild(0).GetComponent<Image>().sprite = Selected_Stage.StageEnemy[num].EnemyImage;
                continue;
            }

            EnemyButtons[num].gameObject.SetActive(false);
        }
    }

    void RewardUI()
    {
        RewardInfos = new List<StageInfo_RewardSO>();
        for (int num = 0; num < RewardButtons.Count; num++)
        {
            RewardButtons[num].gameObject.SetActive(true);

            if (num <= Selected_Stage.StageReward.Count - 1)
            {
                RewardButtons[num].GetChild(0).GetComponent<Image>().sprite = Selected_Stage.StageReward[num].RewardImage;
                RewardInfos.Add(Selected_Stage.StageReward[num]);
                continue;
            }

            RewardButtons[num].gameObject.SetActive(false);
        }
    }

    void EnemyInfoActiveTrue(int num)
    {
        CurrentEnemyInfo = num;
        BackgroundPannel.gameObject.SetActive(true);
        _EnemyInfoPannel.gameObject.SetActive(true);

        EnemyInfoPannel.Instance.EnemyInfoPannelUpdate(EnemyInfos[num]);
        EnemyButtonOutline();

        for (int i = 0; i < EnemyInfoButtons.Count; i++)
        {
            EnemyInfoButtons[i].gameObject.SetActive(false);
            if (i <= EnemyInfos.Count - 1) EnemyInfoButtons[i].gameObject.SetActive(true);
        }
    }

    void RewardInfoActiveTrue(int num)
    {
        int CurrentRewardInfo = num;
        BackGroundPannel_2.gameObject.SetActive(true);
        _RewardInfoPannel.gameObject.SetActive(true);

        RewardInfoPannel.Instance.EnemyInfoPannelUpdate(RewardInfos[num]);
    }

    void RewardInfoActiveFalse()
    {
        BackGroundPannel_2.gameObject.SetActive(false);
        _RewardInfoPannel.gameObject.SetActive(false);
    }

    void EnemyInfoActiveFalse()
    {
        BackgroundPannel.gameObject.SetActive(false);
        _EnemyInfoPannel.gameObject.SetActive(false);
    }

    void EnemyInfoPlus()
    {
        CurrentEnemyInfo++;
        CurrentEnemyInfo = Mathf.Clamp(CurrentEnemyInfo, 0, EnemyInfos.Count - 1);
        EnemyButtonOutline();
        EnemyInfoPannel.Instance.EnemyInfoPannelUpdate(EnemyInfos[CurrentEnemyInfo]);
    }

    void EnemyInfoMinus()
    {
        CurrentEnemyInfo--;
        CurrentEnemyInfo = Mathf.Clamp(CurrentEnemyInfo, 0, EnemyInfos.Count - 1);
        EnemyButtonOutline();
        EnemyInfoPannel.Instance.EnemyInfoPannelUpdate(EnemyInfos[CurrentEnemyInfo]);
    }

    void EnemyButtonOutline()
    {
        for(int num = 0; num < EnemyInfoButtons.Count; num++)
            EnemyInfoButtons[num].GetComponent<Outline>().effectColor = new Color(0, 0, 0, 0);

        EnemyInfoButtons[CurrentEnemyInfo].GetComponent<Outline>().effectColor = new Color(1, 0.83f, 0, 0.57f);
    }

    private void OnApplicationQuit()
    {
        foreach (ArSO Ar in ArList.list)
        {
            Ar.isInGameTake = false;
            Ar.isDead = false;
        }
    }
}
;