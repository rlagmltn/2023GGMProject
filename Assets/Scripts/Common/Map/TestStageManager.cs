using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Road_2
{
    public Transform BeforeButton;
    public Transform NextButton;
    public Transform road;
}

public class TestStageManager : MonoSingleton<TestStageManager>
{
    [SerializeField] private Transform startStage; //그냥 아무것도 아닌 시작하는 스테이지 아마도 emptyStage가 아닐까?
    [SerializeField] private List<Transform> AllButtons;
    [SerializeField] private List<Road_2> AllRoads_Info;
    [SerializeField] private StageSOList stageList;

    [SerializeField] private Transform StageInfoPannel;
    [SerializeField] private Button BackPannel;
    [SerializeField] private Button StartButton;

    [SerializeField] private List<Image> EnemyImages;


    [SerializeField] private Image MapImage;

    [SerializeField] Button InventoryBtn;
    [SerializeField] Transform InventoryPopUpPannel;
    [SerializeField] Button InvnetoryQuitBtn;
    
    private List<Transform> ClearedStages;
    private Transform currentStage;
    [SerializeField] private StageSO SelectedStage;

    void Start()
    {
        Init();
    }

    void Init()
    {
        StageCheck();
        FindNextStage();
        StageDisable();
        ButtonInit();

        //if (GlobalIsEmpty()) SetStageState(); 

        //DebugSetName(); //디버그용
    }

    void StageCheck()
    {
        ClearedStages = new List<Transform>();

        if (GlobalIsEmpty()) //이게 전의 기록이 없을때 실행하는 코드
        {
            startStage.GetComponent<StageSOHolder>().GetStage().IsCleared = true;
            currentStage = startStage;
            SetStageState();
            Debug.Log("처음 실행함");
        }
        else //전에 다른 스테이지에 들어갔을때 실행해주는 코드
        {
            foreach (Transform trans in AllButtons)
            {
                if (trans != startStage)
                    if (trans.GetComponent<StageSOHolder>().GetStage().IsCleared)
                        ClearedStages.Add(trans);

                if (trans.GetComponent<StageSOHolder>().GetStage() == Global.EnterStage)
                    currentStage = trans;
            }
        }
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
        RemoveAllButtonListeners(InventoryBtn);
        AddButtonListener(InventoryBtn, PopUpInventoryUI);
        RemoveAllButtonListeners(InvnetoryQuitBtn);
        AddButtonListener(InvnetoryQuitBtn, InfoPannelActiveFalse);
    }

    public void InfoPannelActiveTrue(StageSO stage)
    {
        StageInfoPannel.gameObject.SetActive(true);
        BackPannel.gameObject.SetActive(true);
        SelectedStage = stage;
        MapImage.sprite = stage.stageInfo.stageImage;
        MapInventory.Instance.UpdateUI();
    }

    void InfoPannelActiveFalse()
    {
        StageInfoPannel.gameObject.SetActive(false);
        InventoryPopUpPannel.gameObject.SetActive(false);
        BackPannel.gameObject.SetActive(false);
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
        //StageClear();

        UnityAction Action = SelectedStage.stageKind switch
        {
            eStageState.Battle => () => SceneManager.LoadScene("TestScene"),
            eStageState.Shop => () => SceneManager.LoadScene("ShopScene"),
            eStageState.Event => () => SceneManager.LoadScene("EventScene"),
            _ => () => SceneManager.LoadScene("TestScene"),
        };

        Action();
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
        Color color = new Color(1, 1, 1);
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
            color = new Color(0.78f, 0.78f, 0.78f, 0.78f);
            if (AllRoads_Info[num].BeforeButton == ClearedStages[ClearedStages.Count - 1])
                color = new Color(1f, 1f, 1f, 1f);

            //길 활성화/비활성화
            RoadchangeColor(AllRoads_Info[num].road, color);
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

            if (stage.IsCleared) color.disabledColor = new Color(1f,1f, 1f, 1f);

            if(stage.IsCanEnter) btn.interactable = true;

            btn.colors = color;
        }
    }

    /// <summary>
    /// 스테이지의 종류를 정해주는 함수
    /// </summary>
    void SetStageState()
    {
        int eventNum = stageList.stageList.Count / 4;
        int shopNum = stageList.stageList.Count / 9;

        for (int num = 0; num < stageList.stageList.Count - 1; num++)
            stageList.stageList[num].stageKind = eStageState.Battle;

        for (int num = 0; num < eventNum;)
        {
            int temp = Random.Range(1, stageList.stageList.Count - 1);

            if (stageList.stageList[temp].stageKind == eStageState.Battle)
            {
                stageList.stageList[temp].stageKind = eStageState.Event;
                num++;
            }
        }

        for (int num = 0; num < shopNum;)
        {
            int temp = Random.Range(1, stageList.stageList.Count - 1);

            if (stageList.stageList[temp].stageKind == eStageState.Battle)
            {
                stageList.stageList[temp].stageKind = eStageState.Shop;
                num++;
            }
        }

        //여기서 스테이지so홀더의 이미지 바꿔주는 함수 실행
        for(int num = 0; num < AllButtons.Count; num++) AllButtons[num].GetComponent<StageSOHolder>().ChangeImage();
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

    void PopUpInventoryUI()
    {
        InventoryPopUpPannel.gameObject.SetActive(true);
        BackPannel.gameObject.SetActive(true);
    }

    void RemoveAllButtonListeners(Button button)
    {
        button.onClick.RemoveAllListeners();
    }

    void AddButtonListener(Button button, UnityAction action)
    {
        button.onClick.AddListener(action);
    }

    public StageSO GetStage()
    {
        return SelectedStage;
    }

    /// <summary>
    /// 테스트로 만들어놓은 거라 나중에 지워줘야할듯?
    /// </summary>
    private void OnApplicationQuit()
    {
        foreach (StageSO stage in stageList.stageList)
        {
            stage.IsCanEnter = false;
            stage.IsCleared = false;
        }
    }
}
