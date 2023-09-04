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
    public List<Transform> Roads;
}

public class StageManager : MonoSingleton<StageManager>
{
    [SerializeField] private List<Road> AllRoads_Info;

    [SerializeField] private List<Transform> AllButtons;

    [SerializeField] private Button CancelButton;
    [SerializeField] private StageSOList stageList;
    [SerializeField] private Transform startStage; //그냥 아무것도 아닌 시작하는 스테이지 아마도 emptyStage가 아닐까?

    [SerializeField] private BattleMapHolder battleMapHolder;
    [SerializeField] private BattleMapList battleMapList;

    [SerializeField] private Transform Map1;
    [SerializeField] private Transform Map2;
    [SerializeField] private Transform Content;

    public List<Transform> ClearedStages;

    public Transform currentStage;
    private StageSO Selected_Stage;

    private void Start()
    {
        Init();
    }

    void Init()
    {
        StageCheck();
        FindNextStage();
        StageDisable();
        PlayBGM();
    }

    void StageCheck()
    {
        ClearedStages = new List<Transform>();

        if (GlobalIsEmpty()) //이게 전의 기록이 없을때 실행하는 코드
        {
            startStage.GetComponent<StageSOHolder>().GetStage().IsCleared = true;
            currentStage = startStage;
            ClearedStages.Add(startStage);
            SetStageState(stageList);
            Debug.Log("처음 실행함");
        }
        else //전에 다른 스테이지에 들어갔을때 실행해주는 코드
        {
            ClearedStages.Add(startStage);
            foreach (Transform trans in AllButtons)
            {
                if (trans.GetComponent<StageSOHolder>().GetStage().IsCleared)
                {
                    ClearedStages.Add(trans);
                    Debug.Log(trans.GetComponent<StageSOHolder>().GetStage().stageInfo.stageName);
                }

                if (trans.GetComponent<StageSOHolder>().GetStage().IsCleared)
                    currentStage = trans;

                //중간보스 스테이지를 클리어했을때 맵 바꾸기를 시전할 위치
                if(currentStage.GetComponent<StageSOHolder>().GetStage().stageInfo.stageName == "BOSS STAGE")
                {
                    Global.is2Stage = true;
                    Content.transform.localPosition = new Vector3(0, 730, 0);
                    Map1.transform.localPosition = new Vector3(2100, 4000f, 0);
                    Map2.transform.localPosition = new Vector3(2100, 0f, 0);
                }
            }

            for (int num = 0; num < AllButtons.Count; num++) AllButtons[num].GetComponent<StageSOHolder>().ChangeImage(); //스테이지 아이콘 바꿔주는 함수
        }
    }

    void SetStageState(StageSOList StageList)
    {
        int eventNum = StageList.stageList.Count / 4;
        int shopNum = StageList.stageList.Count / 9;

        for (int num = 0; num < StageList.stageList.Count - 1; num++)
        {
            StageList.stageList[num].stageKind = eStageState.Battle;
            StageList.stageList[num].battleMapSO = battleMapList.stages[0].maps[Random.Range(0, battleMapList.stages[0].maps.Length-1)];
        }

        StageKindChange(eStageState.Event, eventNum, StageList);
        StageKindChange(eStageState.Shop, shopNum, StageList);
        //
        //
        Debug.Log(shopNum);

        StageList.stageList[StageList.stageList.Count - 1].battleMapSO = battleMapList.stages[0].maps[battleMapList.stages[0].maps.Length - 1];

        //여기서 스테이지so홀더의 이미지 바꿔주는 함수 실행
        for (int num = 0; num < AllButtons.Count; num++) AllButtons[num].GetComponent<StageSOHolder>().ChangeImage();
    }

    void PlayBGM()
    {
        SoundManager.Instance.Play(SoundManager.Instance.GetOrAddAudioClips("BackGroundMusic/EventStageBGM_1", Sound.BGM), Sound.BGM);
    }

    void StageKindChange(eStageState State, int StateNum, StageSOList StageList)
    {
        for (int num = 0; num < StateNum;)
        {
            int temp = Random.Range(1, StageList.stageList.Count - 1);

            if (temp == 10) continue;
            if (StageList.stageList[temp].stageKind != eStageState.Battle) continue;

            StageList.stageList[temp].stageKind = State;
            num++;
        }
    }

    bool GlobalIsEmpty()
    {
        int num = 0;
        foreach(StageSO stage in stageList.stageList)
        {
            if (stage.IsCleared) num++;
        }

        if (num > 0) return false;

        foreach (Transform trans in AllButtons)
            if (Global.EnterStage == trans.GetComponent<StageSOHolder>().GetStage())
                return false;
        return true;
    }

    void FindNextStage()
    {
        Color color = new Color(1, 1, 1);
        StageSOHolder CurrentStage = currentStage.GetComponent<StageSOHolder>();

        //현재의 입장 가능한 스테이지 전부 초기화
        for (int num = 0; num < AllButtons.Count; num++)
            AllButtons[num].GetComponent<StageSOHolder>().GetStage().IsCanEnter = false;

        //입장 가능한 스테이지 설정
        for (int num = 0; num < CurrentStage.NextStageList.Count; num++)
            CurrentStage.NextStageList[num].GetComponent<StageSOHolder>().GetStage().IsCanEnter = true;

        if (ClearedStages.Count <= 0)
        {
            color = new Color(0.56f, 0.56f, 0.56f, 0.78f);
            for (int num = 0; num < AllRoads_Info.Count; num++)
                ChangeRoadColor(AllRoads_Info[num].Roads, color);
            return;
        }

        for (int num = 0; num < AllRoads_Info.Count; num++)
        {
            //들어갈 수 있는길 활성화 해주기
            color = new Color(0.56f, 0.56f, 0.56f, 0.78f);

            if (AllRoads_Info[num].BeforeButton == ClearedStages[ClearedStages.Count - 1])
                color = new Color(1f, 1f, 1f, 1f);

            //길 활성화/비활성화
            ChangeRoadColor(AllRoads_Info[num].Roads, color);
        }

        //클리어했던 길 활성화 해주기
        if (ClearedStages.Count < 2) return;

        color = new Color(1, 1, 1, 1);

        for (int num = 0; num < AllRoads_Info.Count; num++)
        {
            for (int i = 0; i < ClearedStages.Count - 1; i++)
            {
                if (ClearedStages[i] != AllRoads_Info[num].BeforeButton) continue;
                if (ClearedStages[i + 1] != AllRoads_Info[num].NextButton) continue;

                ChangeRoadColor(AllRoads_Info[num].Roads, color);
            }
        }
    }

    void ChangeRoadColor(List<Transform> roads, Color color)
    {
        for(int num = 0; num < roads.Count; num++)
        {
            roads[num].GetComponent<Image>().color = color;
        }
    }

    /// <summary>
    /// 스테이지를 비활성화를 다시 로드하는 함수
    /// </summary>
    void StageDisable()
    {
        ColorBlock color = new();
        color = AllButtons[0].GetComponent<Button>().colors;

        for (int num = 0; num < AllButtons.Count; num++)
        {
            Button btn = AllButtons[num].GetComponent<Button>();
            StageSO stage = AllButtons[num].GetComponent<StageSOHolder>().GetStage();

            btn.interactable = false;
            color.disabledColor = new Color(0.78f, 0.78f, 0.78f, 0.5f);

            if (stage.IsCleared) color.disabledColor = new Color(1f, 1f, 1f, 1f);

            if (stage.IsCanEnter) btn.interactable = true;

            btn.colors = color;
        }
    }

    public void SetSelectedStage(StageSO stage)
    {
        Selected_Stage = stage;
    }

    /// <summary>
    /// 게임에 들어갈 때 실행할 함수
    /// </summary>
    /// <param name="stageSO"></param>
    public void StageEnter()
    {
        Global.EnterStage = Selected_Stage;
        //맵 로딩 해줘야함
        Global.Map = Selected_Stage.map;
        //지금은 임시로 스테이지 정해둠
        battleMapHolder.map = Selected_Stage.battleMapSO;

        UnityAction Action = Selected_Stage.stageKind switch
        {
            eStageState.Battle => () => { SceneManager.LoadScene("TestScene"); SaveManager.Instance.GameData.PointPoint += 50; },
            eStageState.Shop => () => { SceneManager.LoadScene("ShopScene"); SaveManager.Instance.GameData.PointPoint += 20; },
            eStageState.Event => () => { SceneManager.LoadScene("EventScene"); SaveManager.Instance.GameData.PointPoint += 30; },
            _ => () => { SceneManager.LoadScene("TestScene"); SaveManager.Instance.GameData.PointPoint += 100; },
        };
        SaveManager.Instance.GameDataSave();
        Action();
    }

    private void OnApplicationQuit()
    {
        //foreach (StageSO stage in stageList.stageList)
        //{
        //    stage.IsCanEnter = false;
        //    stage.IsCleared = false;
        //}
    }
}
