using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TestStageManager : MonoBehaviour
{
    [SerializeField] private Transform startStage; //그냥 아무것도 아닌 시작하는 스테이지 아마도 emptyStage가 아닐까?
    [SerializeField] private List<Transform> AllButtons;
    [SerializeField] private StageSOList stageList;

    [SerializeField] private Transform StageInfoPannel;

    private Transform currentStage;

    void Start()
    {
        Init();
    }

    void Init()
    {
        startStage.GetComponent<StageSOHolder>().GetStage().IsCleared = true;
        currentStage = startStage;
        FindNextStage();
        StageDisable();
    }

    /// <summary>
    /// 게임에 들어갈 때 실행할 함수
    /// </summary>
    /// <param name="stageSO"></param>
    void StageEnter(StageSO stageSO)
    {
        Global.EnterStage = stageSO;
        //맵 로딩 해줘야함

        //지금은 임시로 스테이지 클리어임
        StageClear();
    }

    /// <summary>
    /// 스테이지가 클리어 돼었을때 실행할 함수
    /// </summary>
    public void StageClear()
    {
        Global.EnterStage.IsCleared = true;

        foreach(Transform trans in AllButtons)
        {
            if(trans.GetComponent<StageSOHolder>().GetStage() == Global.EnterStage)
            {
                currentStage = trans;
                Debug.Log("현재 스테이지 체인지");
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
    }

    /// <summary>
    /// 스테이지를 비활성화를 다시 로드하는 함수
    /// </summary>
    void StageDisable()
    {
        for(int num = 0; num < AllButtons.Count; num++)
        {
            Button btn = AllButtons[num].GetComponent<Button>();
            StageSO stage = AllButtons[num].GetComponent<StageSOHolder>().GetStage();
            btn.interactable = false;

            if(stage.IsCleared || stage.IsCanEnter)
                btn.interactable = true;
        }
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
