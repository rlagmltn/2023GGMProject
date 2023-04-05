using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Road
{
    public Transform BeforeButton;
    public Transform NextButton;
    public Transform road;
}

public class TestStageManager : MonoSingleton<TestStageManager>
{
    [SerializeField] private Transform startStage; //그냥 아무것도 아닌 시작하는 스테이지 아마도 emptyStage가 아닐까?
    [SerializeField] private List<Transform> AllButtons;
    [SerializeField] private List<Road> AllRoads_Info;
    [SerializeField] private StageSOList stageList;

    [SerializeField] private Transform StageInfoPannel;
    [SerializeField] private Button BackPannel;
    [SerializeField] private Button StartButton;

    [SerializeField] private List<Image> EnemyImages;
    [SerializeField] private List<TextMeshProUGUI> EnemyTexts;

    [SerializeField] private Image MapImage;
    
    public List<Transform> ClearedStages;
    private Transform currentStage;
    private StageSO SelectedStage;

    void Start()
    {
        Init();
    }

    void Init()
    {
        if(GlobalIsEmpty())
        {
            startStage.GetComponent<StageSOHolder>().GetStage().IsCleared = true;
            currentStage = startStage;
            SetStageState();
            Debug.Log("빔");
        }
        else
        {
            foreach (Transform trans in AllButtons)
            {
                if (trans.GetComponent<StageSOHolder>().GetStage() == Global.EnterStage)
                {
                    currentStage = trans;
                    break;
                }
            }
        }

        FindNextStage();
        StageDisable();
        ButtonInit();

        //if (GlobalIsEmpty()) SetStageState(); 

        //DebugSetName(); //디버그용
    }

    void ButtonInit()
    {
        for(int num = 0; num < AllButtons.Count; num++)
        {
            Button btn = AllButtons[num].GetComponent<Button>();
            RemoveAllButtonListeners(btn);
            AddButtonListener(btn, btn.GetComponent<StageSOHolder>().StageSelect);
        }

        RemoveAllButtonListeners(BackPannel);
        AddButtonListener(BackPannel, InfoPannelActiveFalse);
        RemoveAllButtonListeners(StartButton);
        AddButtonListener(StartButton, StageEnter);
    }

    void DebugSetName()
    { 
        for(int i = 0; i <stageList.stageList.Count; i++)
        {
            var stage = stageList.stageList[i].stageInfo;
            stage.stageName = new string($"스테이지{i + 1}");
            stage.explanationText = new string($"스테이지{i + 1} 이다.");
        }
    }

    public void InfoPannelActiveTrue(StageSO stage)
    {
        StageInfoPannel.gameObject.SetActive(true);
        BackPannel.gameObject.SetActive(true);
        SelectedStage = stage;
        MapImage.sprite = stage.stageInfo.stageImage;
        StageInfoPannelUpdate();
    }

    void InfoPannelActiveFalse()
    {
        StageInfoPannel.gameObject.SetActive(false);
        BackPannel.gameObject.SetActive(false);
    }

    void StageInfoPannelUpdate()
    {
        for(int num = 0; num < EnemyTexts.Count; num++)
        {
            EnemyTexts[num].text = SelectedStage.stageEnmey[num].Name;
        }
        //스테이지의 정보로 ui업데이트
    }

    /// <summary>
    /// 게임에 들어갈 때 실행할 함수
    /// </summary>
    /// <param name="stageSO"></param>
    public void StageEnter()
    {
        InfoPannelActiveFalse();
        Global.EnterStage = SelectedStage;
        //맵 로딩 해줘야함
        Global.Map = SelectedStage.map;
        //지금은 임시로 스테이지 클리어임
        StageClear();

        //SceneManager.LoadScene("TestScene");
    }

    /// <summary>
    /// 스테이지가 클리어 돼었을때 실행할 함수
    /// </summary>
    public void StageClear()
    {
        Global.EnterStage.IsCleared = true; //이거를 다른 거에서 해줘야할듯? 이거만 하면 됨 ㄹㅇ

        foreach(Transform trans in AllButtons)
        {
            if(trans.GetComponent<StageSOHolder>().GetStage() == Global.EnterStage)
            {
                currentStage = trans;
                ClearedStages.Add(currentStage);
                break;
            }
        }

        FindNextStage();
        StageDisable();
    }

    /// <summary>
    /// 전 스테이지를 클리어했을때 다음스테이지를 찾아주는 함수
    /// </summary>
    void FindNextStage()
    {
        StageSOHolder CurrentStage = currentStage.GetComponent<StageSOHolder>();

        //현재의 입장 가능한 스테이지 전부 초기화
        for(int num = 0; num < AllButtons.Count; num++)
            AllButtons[num].GetComponent<StageSOHolder>().GetStage().IsCanEnter = false;

        //입장 가능한 스테이지 설정
        for (int num = 0; num < CurrentStage.NextStageList.Count; num++)
            CurrentStage.NextStageList[num].GetComponent<StageSOHolder>().GetStage().IsCanEnter = true;

        if (ClearedStages.Count <= 0)
        {
            for (int num = 0; num < AllRoads_Info.Count; num++)
                RoadchangeColor(AllRoads_Info[num].road, new Color(0.78f, 0.78f, 0.78f, 0.78f));
            return;
        }

        for (int num = 0; num < AllRoads_Info.Count; num++)
        {
            //들어갈 수 있는길 활성화 해주기
            if (AllRoads_Info[num].BeforeButton == ClearedStages[ClearedStages.Count - 1])
            {
                RoadchangeColor(AllRoads_Info[num].road, new Color(1, 1, 1));
                continue;
            }

            //길 비활성화
            RoadchangeColor(AllRoads_Info[num].road, new Color(0.78f, 0.78f, 0.78f, 0.78f));
        }


        //클리어했던 길 활성화 해주기
        if (ClearedStages.Count < 2) return;

        for (int num = 0; num < AllRoads_Info.Count; num++)
        {
            for (int i = 0; i < ClearedStages.Count - 1; i++)
            {
                if (ClearedStages[i] != AllRoads_Info[num].BeforeButton) continue;
                if (ClearedStages[i + 1] != AllRoads_Info[num].NextButton) continue;

                RoadchangeColor(AllRoads_Info[num].road, new Color(1, 1, 1));
            }
        }
    }

    /// <summary>
    /// 스테이지를 비활성화를 다시 로드하는 함수
    /// </summary>
    void StageDisable()
    {
        ColorBlock color = new();
        color = AllButtons[1].GetComponent<Button>().colors;

        for (int num = 0; num < AllButtons.Count; num++)
        {
            Button btn = AllButtons[num].GetComponent<Button>();
            StageSO stage = AllButtons[num].GetComponent<StageSOHolder>().GetStage();

            btn.interactable = false;
            color.disabledColor = new Color(0.78f, 0.78f, 0.78f, 0.5f);
            btn.colors = color;

            if (stage.IsCleared)
            {
                color.disabledColor = new Color(1f,1f, 1f, 1f);
                btn.colors = color;
                continue;
            }

            if(stage.IsCanEnter)
            {
                btn.interactable = true;
                color.disabledColor = new Color(1f, 1f, 1f, 1f);
                btn.colors = color;
            }

        }
    }

    void SetStageState()
    {
        int eventNum = stageList.stageList.Count / 4;
        int shopNum = stageList.stageList.Count / 9;

        for (int num = 0; num < stageList.stageList.Count - 1; num++)
            stageList.stageList[num].stageKind = eStageState.Battle;

        for (int num = 0; num < eventNum;)
        {
            int temp = Random.Range(0, stageList.stageList.Count - 1);

            if (stageList.stageList[temp].stageKind == eStageState.Battle)
            {
                stageList.stageList[temp].stageKind = eStageState.Event;
                num++;
            }
        }

        for (int num = 0; num < shopNum;)
        {
            int temp = Random.Range(0, stageList.stageList.Count - 1);

            if (stageList.stageList[temp].stageKind == eStageState.Battle)
            {
                stageList.stageList[temp].stageKind = eStageState.Shop;
                num++;
            }
        }
    }

    bool GlobalIsEmpty()
    {
        foreach(Transform trans in AllButtons)
        {
            if (Global.EnterStage == trans.GetComponent<StageSOHolder>().GetStage())
                return false;
        }
        return true;
    }

    void RoadchangeColor(Transform obj, Color color)
    {
        obj.GetComponent<Image>().color = color;
    }

    void RemoveAllButtonListeners(Button button)
    {
        button.onClick.RemoveAllListeners();
    }

    void AddButtonListener(Button button, UnityAction action)
    {
        button.onClick.AddListener(action);
    }

    /// <summary>
    /// 테스트로 만들어놓은 거라 나중에 지워줘야할듯?
    /// </summary>
    private void OnApplicationQuit()
    {
        foreach(StageSO stage in stageList.stageList)
        {
            stage.IsCanEnter = false;
            stage.IsCleared = false;
        }
    }
}
