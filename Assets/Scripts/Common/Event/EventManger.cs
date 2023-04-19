using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManger : MonoSingleton<EventManger>
{
    [SerializeField] private Button ClearButton;
    [SerializeField] private Transform BackGroudnPannel;
    [SerializeField] private Transform ClearPannel;

    [SerializeField] private List<_Event> EventList;
    public Events S_event;

    private int RandomNum;

    private void Start()
    {
        Init();
    }

    void Init()
    {
        ButtonInit();
        EventPlay();
    }

    void ButtonInit()
    {
        ClearButton.onClick.RemoveAllListeners();
        ClearButton.onClick.AddListener(StageClear);
    }

    void EventPlay()
    {
        EventSelect();
        
        switch(S_event)
        {
            case Events.HealAll: { EventList[(int)Events.HealAll].EventStart();} break;
            default: { EventList[(int)S_event].EventStart();} break;
        }
    }

    void EventSelect()
    {
        RandomNum = Random.Range(0, EventList.Count);

        foreach(Events num in Events.GetValues(typeof(Events)))
        {
            if(RandomNum == (int)num)
            {
                string EventName = Events.GetName(typeof(Events), RandomNum);
                S_event = (Events)Events.Parse(typeof(Events), EventName);
                break;
            }
        }
    }

    internal void EventClear() //�̺�Ʈ�� Ŭ��������� ����� â
    {
        BackGroudnPannel.gameObject.SetActive(true);
        ClearPannel.gameObject.SetActive(true);
    }

    void StageClear()
    {
        Global.EnterStage.IsCleared = true;
        SceneMgr.Instance.LoadScene(eSceneName.Map);
    }
}
