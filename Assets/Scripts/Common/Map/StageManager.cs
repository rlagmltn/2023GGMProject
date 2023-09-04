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
    [SerializeField] private Transform startStage; //�׳� �ƹ��͵� �ƴ� �����ϴ� �������� �Ƹ��� emptyStage�� �ƴұ�?

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

        if (GlobalIsEmpty()) //�̰� ���� ����� ������ �����ϴ� �ڵ�
        {
            startStage.GetComponent<StageSOHolder>().GetStage().IsCleared = true;
            currentStage = startStage;
            ClearedStages.Add(startStage);
            SetStageState(stageList);
            Debug.Log("ó�� ������");
        }
        else //���� �ٸ� ���������� ������ �������ִ� �ڵ�
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

                //�߰����� ���������� Ŭ���������� �� �ٲٱ⸦ ������ ��ġ
                if(currentStage.GetComponent<StageSOHolder>().GetStage().stageInfo.stageName == "BOSS STAGE")
                {
                    Global.is2Stage = true;
                    Content.transform.localPosition = new Vector3(0, 730, 0);
                    Map1.transform.localPosition = new Vector3(2100, 4000f, 0);
                    Map2.transform.localPosition = new Vector3(2100, 0f, 0);
                }
            }

            for (int num = 0; num < AllButtons.Count; num++) AllButtons[num].GetComponent<StageSOHolder>().ChangeImage(); //�������� ������ �ٲ��ִ� �Լ�
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

        //���⼭ ��������soȦ���� �̹��� �ٲ��ִ� �Լ� ����
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

        //������ ���� ������ �������� ���� �ʱ�ȭ
        for (int num = 0; num < AllButtons.Count; num++)
            AllButtons[num].GetComponent<StageSOHolder>().GetStage().IsCanEnter = false;

        //���� ������ �������� ����
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
            //�� �� �ִ±� Ȱ��ȭ ���ֱ�
            color = new Color(0.56f, 0.56f, 0.56f, 0.78f);

            if (AllRoads_Info[num].BeforeButton == ClearedStages[ClearedStages.Count - 1])
                color = new Color(1f, 1f, 1f, 1f);

            //�� Ȱ��ȭ/��Ȱ��ȭ
            ChangeRoadColor(AllRoads_Info[num].Roads, color);
        }

        //Ŭ�����ߴ� �� Ȱ��ȭ ���ֱ�
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
    /// ���������� ��Ȱ��ȭ�� �ٽ� �ε��ϴ� �Լ�
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
    /// ���ӿ� �� �� ������ �Լ�
    /// </summary>
    /// <param name="stageSO"></param>
    public void StageEnter()
    {
        Global.EnterStage = Selected_Stage;
        //�� �ε� �������
        Global.Map = Selected_Stage.map;
        //������ �ӽ÷� �������� ���ص�
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
