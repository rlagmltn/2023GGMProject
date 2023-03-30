using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TestStageManager : MonoBehaviour
{
    [SerializeField] private Transform startStage; //�׳� �ƹ��͵� �ƴ� �����ϴ� �������� �Ƹ��� emptyStage�� �ƴұ�?
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
    /// ���ӿ� �� �� ������ �Լ�
    /// </summary>
    /// <param name="stageSO"></param>
    void StageEnter(StageSO stageSO)
    {
        Global.EnterStage = stageSO;
        //�� �ε� �������

        //������ �ӽ÷� �������� Ŭ������
        StageClear();
    }

    /// <summary>
    /// ���������� Ŭ���� �ž����� ������ �Լ�
    /// </summary>
    public void StageClear()
    {
        Global.EnterStage.IsCleared = true;

        foreach(Transform trans in AllButtons)
        {
            if(trans.GetComponent<StageSOHolder>().GetStage() == Global.EnterStage)
            {
                currentStage = trans;
                Debug.Log("���� �������� ü����");
                break;
            }
        }

        FindNextStage();
        StageDisable();
    }

    /// <summary>
    /// �� ���������� Ŭ���������� �������������� ã���ִ� �Լ�
    /// </summary>
    void FindNextStage()
    {
        StageSOHolder CurrentStage = currentStage.GetComponent<StageSOHolder>();

        //������ ���� ������ �������� ���� �ʱ�ȭ
        for(int num = 0; num < AllButtons.Count; num++)
            AllButtons[num].GetComponent<StageSOHolder>().GetStage().IsCanEnter = false;

        //���� ������ �������� ����
        for (int num = 0; num < CurrentStage.NextStageList.Count; num++)
            CurrentStage.NextStageList[num].GetComponent<StageSOHolder>().GetStage().IsCanEnter = true;
    }

    /// <summary>
    /// ���������� ��Ȱ��ȭ�� �ٽ� �ε��ϴ� �Լ�
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
    /// �׽�Ʈ�� �������� �Ŷ� ���߿� ��������ҵ�?
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
